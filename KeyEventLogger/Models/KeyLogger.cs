namespace KeyEventLogger.Models;

/// <summary>
/// Kayıt durumunu (açık/kapalı), kayıt listesini ve tuş istatistiklerini yönetir.
/// Arayüzden bağımsızdır; yeni kayıt eklendiğinde EntryAdded olayını tetikler.
/// </summary>
public class KeyLogger
{
    private readonly List<KeyLogEntry> _entries = new();
    private readonly Dictionary<string, int> _keyCounts = new(); // tuş adı → basılma sayısı

    public bool IsRecording { get; private set; }
    public IReadOnlyList<KeyLogEntry> Entries => _entries;

    /// <summary>Toplam tuş basımı (yalnızca KeyDown olayları sayılır).</summary>
    public int TotalKeyPresses { get; private set; }

    /// <summary>Yeni bir kayıt eklendiğinde tetiklenir.</summary>
    public event EventHandler<KeyLogEntry>? EntryAdded;

    public void Start() => IsRecording = true;

    public void Stop() => IsRecording = false;

    /// <summary>Tüm kayıtları ve istatistikleri siler.</summary>
    public void Clear()
    {
        _entries.Clear();
        _keyCounts.Clear();
        TotalKeyPresses = 0;
    }

    /// <summary>Kayıt açıksa olayı listeye ekler. Kayıt kapalıysa hiçbir şey yapmaz.</summary>
    public bool Record(KeyLogEntry entry)
    {
        if (!IsRecording) return false;

        _entries.Add(entry);

        if (entry.EventType == KeyEventType.KeyDown)
        {
            TotalKeyPresses++;
            _keyCounts[entry.KeyName] = _keyCounts.GetValueOrDefault(entry.KeyName) + 1;
        }

        EntryAdded?.Invoke(this, entry);
        return true;
    }

    /// <summary>En sık basılan tuşlar (çoktan aza; eşitlikte alfabetik).</summary>
    public IEnumerable<KeyValuePair<string, int>> GetTopKeys(int count) =>
        _keyCounts.OrderByDescending(k => k.Value)
                  .ThenBy(k => k.Key)
                  .Take(count);

    /// <summary>Verilen tarih/saat aralığındaki kayıtlar (sınırlar dahil).</summary>
    public IEnumerable<KeyLogEntry> Filter(DateTime from, DateTime to) =>
        _entries.Where(e => e.Time >= from && e.Time <= to);
}
