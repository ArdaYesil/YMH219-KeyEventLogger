using KeyEventLogger.Models;
using KeyEventLogger.Services;

namespace KeyEventLogger;

/// <summary>
/// Ana pencere. Klavye olaylarını yalnızca KENDİ penceresi aktifken yakalar
/// (Form.KeyPreview), KeyLogger nesnesine iletir ve sonuçları arayüzde gösterir.
/// Global hook, arka plan çalışması veya ağ gönderimi YOKTUR.
/// </summary>
public partial class MainForm : Form
{
    private const int TopKeyCount = 10;

    private readonly KeyLogger _logger = new();
    private readonly LogFileService _fileService = new();

    // Aktif filtre (null = filtre yok, tüm kayıtlar gösterilir)
    private (DateTime From, DateTime To)? _filter;

    public MainForm()
    {
        InitializeComponent();

        _logger.EntryAdded += Logger_EntryAdded;

        // Filtre için mantıklı varsayılanlar: bugünün başı ve sonu
        dtpFrom.Value = DateTime.Today;
        dtpTo.Value = DateTime.Today.AddDays(1).AddSeconds(-1);

        // Ok tuşları ve Tab normalde form içinde odak değiştirmek için kullanılır ve
        // KeyDown olayına ulaşmaz. Kayıt sırasında bunları da "giriş tuşu" sayıyoruz.
        AttachPreviewKeyDown(this);

        UpdateRecordingUi();
        UpdateStatistics();
        UpdateCountLabel();
    }

    // ------------------------------------------------------------------
    // Klavye olayları (yalnızca bu form aktifken gelir)
    // ------------------------------------------------------------------

    private void MainForm_KeyDown(object? sender, KeyEventArgs e)
    {
        if (chkKeyDown.Checked)
            _logger.Record(KeyLogEntry.FromKeyEvent(KeyEventType.KeyDown, e));
    }

    private void MainForm_KeyUp(object? sender, KeyEventArgs e)
    {
        if (chkKeyUp.Checked)
            _logger.Record(KeyLogEntry.FromKeyEvent(KeyEventType.KeyUp, e));
    }

    private void MainForm_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (chkKeyPress.Checked)
            _logger.Record(KeyLogEntry.FromKeyPress(e));
    }

    /// <summary>KeyLogger yeni bir kayıt eklediğinde çalışır (olay aboneliği).</summary>
    private void Logger_EntryAdded(object? sender, KeyLogEntry entry)
    {
        if (MatchesFilter(entry))
        {
            lstLog.Items.Add(entry);
            lstLog.TopIndex = lstLog.Items.Count - 1; // Son kayda kaydır
        }

        if (entry.EventType == KeyEventType.KeyDown)
            UpdateStatistics();

        UpdateCountLabel();
    }

    private void AttachPreviewKeyDown(Control parent)
    {
        foreach (Control child in parent.Controls)
        {
            child.PreviewKeyDown += (s, e) =>
            {
                if (_logger.IsRecording) e.IsInputKey = true;
            };
            AttachPreviewKeyDown(child);
        }
    }

    // ------------------------------------------------------------------
    // Butonlar
    // ------------------------------------------------------------------

    private void BtnStart_Click(object? sender, EventArgs e)
    {
        _logger.Start();
        UpdateRecordingUi();
        FocusTestArea();
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
        _logger.Stop();
        UpdateRecordingUi();
        FocusTestArea();
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        if (_logger.Entries.Count > 0)
        {
            var answer = MessageBox.Show(
                $"{_logger.Entries.Count} kayıt silinecek. Emin misiniz?",
                "Kayıtları Temizle", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer != DialogResult.Yes) return;
        }

        _logger.Clear();
        lstLog.Items.Clear();
        txtTest.Clear();
        UpdateStatistics();
        UpdateCountLabel();
        FocusTestArea();
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        var entries = GetVisibleEntries().ToList();
        if (entries.Count == 0)
        {
            MessageBox.Show("Kaydedilecek kayıt yok.", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Title = "Kayıtları .txt dosyasına kaydet",
            Filter = "Metin dosyası (*.txt)|*.txt",
            FileName = $"keylog_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;

        try
        {
            int count = _fileService.Save(dialog.FileName, entries);
            MessageBox.Show($"{count} kayıt kaydedildi:\n{dialog.FileName}", "Kaydedildi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            MessageBox.Show($"Dosya kaydedilemedi:\n{ex.Message}", "Hata",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        FocusTestArea();
    }

    private void BtnFilter_Click(object? sender, EventArgs e)
    {
        if (dtpFrom.Value > dtpTo.Value)
        {
            MessageBox.Show("Başlangıç zamanı bitiş zamanından sonra olamaz.", "Geçersiz aralık",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _filter = (dtpFrom.Value, dtpTo.Value);
        lblFilterState.ForeColor = Color.DarkBlue;
        lblFilterState.Text = $"Filtre aktif:\n{dtpFrom.Value:dd.MM.yyyy HH:mm:ss}\n– {dtpTo.Value:dd.MM.yyyy HH:mm:ss}";
        RefreshLogList();
        FocusTestArea();
    }

    private void BtnClearFilter_Click(object? sender, EventArgs e)
    {
        _filter = null;
        lblFilterState.ForeColor = Color.DimGray;
        lblFilterState.Text = "Filtre yok — tüm kayıtlar gösteriliyor.";
        RefreshLogList();
        FocusTestArea();
    }

    // ------------------------------------------------------------------
    // Pencere odağı göstergesi
    // ------------------------------------------------------------------

    private void MainForm_Activated(object? sender, EventArgs e)
    {
        lblFocus.ForeColor = Color.DimGray;
        lblFocus.Text = "ⓘ Pencere aktif. Yalnızca bu pencere aktifken basılan tuşlar kaydedilir.";
    }

    private void MainForm_Deactivate(object? sender, EventArgs e)
    {
        lblFocus.ForeColor = Color.DarkOrange;
        lblFocus.Text = "⚠ Pencere aktif değil — başka uygulamalardaki tuşlar kaydedilmez.";
    }

    // ------------------------------------------------------------------
    // Arayüz yardımcıları
    // ------------------------------------------------------------------

    /// <summary>Kayıt durumuna göre göstergeyi ve butonları günceller.</summary>
    private void UpdateRecordingUi()
    {
        bool rec = _logger.IsRecording;

        lblStatus.Text = rec ? "● KAYIT AÇIK" : "■ KAYIT KAPALI";
        lblStatus.BackColor = rec ? Color.Firebrick : Color.DimGray;
        Text = rec
            ? "● KAYITTA — YMH219 - Key Event Logger (Eğitsel)"
            : "YMH219 - Key Event Logger (Eğitsel)";

        btnStart.Enabled = !rec;
        btnStop.Enabled = rec;
        btnStart.BackColor = rec ? Color.Gray : Color.FromArgb(46, 125, 50);
        btnStop.BackColor = rec ? Color.FromArgb(198, 40, 40) : Color.Gray;
    }

    private void UpdateStatistics()
    {
        lblTotal.Text = $"Toplam tuş basımı: {_logger.TotalKeyPresses}";

        lstTopKeys.BeginUpdate();
        lstTopKeys.Items.Clear();
        int rank = 1;
        foreach (var (key, count) in _logger.GetTopKeys(TopKeyCount))
        {
            lstTopKeys.Items.Add($"{rank++,2}. {key,-14} {count,5}");
        }
        lstTopKeys.EndUpdate();
    }

    private void UpdateCountLabel()
    {
        lblCount.Text = $"Gösterilen: {lstLog.Items.Count}  /  Toplam kayıt: {_logger.Entries.Count}";
    }

    /// <summary>Listeyi, aktif filtreye göre baştan doldurur.</summary>
    private void RefreshLogList()
    {
        lstLog.BeginUpdate();
        lstLog.Items.Clear();
        foreach (var entry in GetVisibleEntries())
        {
            lstLog.Items.Add(entry);
        }
        lstLog.EndUpdate();

        if (lstLog.Items.Count > 0)
            lstLog.TopIndex = lstLog.Items.Count - 1;

        UpdateCountLabel();
    }

    private IEnumerable<KeyLogEntry> GetVisibleEntries() =>
        _filter is { } f ? _logger.Filter(f.From, f.To) : _logger.Entries;

    private bool MatchesFilter(KeyLogEntry entry) =>
        _filter is not { } f || (entry.Time >= f.From && entry.Time <= f.To);

    /// <summary>
    /// Buton tıklamasından sonra odağı deneme alanına verir. Böylece SPACE/Enter
    /// yanlışlıkla odaktaki butona basmaz.
    /// </summary>
    private void FocusTestArea() => txtTest.Focus();
}
