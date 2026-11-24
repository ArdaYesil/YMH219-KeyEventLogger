namespace KeyEventLogger.Models;

/// <summary>
/// Tek bir klavye olayının kaydı: ne zaman, hangi olay, hangi tuş.
/// Nesne oluşturulduktan sonra değiştirilemez (tüm özellikler salt okunur).
/// </summary>
public class KeyLogEntry
{
    // Enum adı kullanıcıya anlaşılmaz olan tuşlar için okunabilir isimler
    private static readonly Dictionary<Keys, string> FriendlyNames = new()
    {
        [Keys.Return] = "Enter",
        [Keys.Back] = "Backspace",
        [Keys.ControlKey] = "Ctrl",
        [Keys.ShiftKey] = "Shift",
        [Keys.Menu] = "Alt",
        [Keys.Capital] = "CapsLock",
        [Keys.Escape] = "Esc",
        [Keys.Next] = "PageDown",
        [Keys.Prior] = "PageUp",
        [Keys.LWin] = "Win",
        [Keys.RWin] = "Win",
    };

    public DateTime Time { get; }
    public KeyEventType EventType { get; }
    public string KeyName { get; }

    /// <summary>Sanal tuş kodu. KeyPress olayında karakter olduğu için yoktur (null).</summary>
    public int? KeyCode { get; }

    /// <summary>O anda basılı olan Ctrl / Shift / Alt tuşları.</summary>
    public string Modifiers { get; }

    private KeyLogEntry(DateTime time, KeyEventType type, string keyName, int? keyCode, string modifiers)
    {
        Time = time;
        EventType = type;
        KeyName = keyName;
        KeyCode = keyCode;
        Modifiers = modifiers;
    }

    /// <summary>KeyDown / KeyUp olayından kayıt oluşturur.</summary>
    public static KeyLogEntry FromKeyEvent(KeyEventType type, KeyEventArgs e) =>
        new(DateTime.Now, type, GetKeyName(e.KeyCode), (int)e.KeyCode, FormatModifiers(e.Modifiers));

    /// <summary>KeyPress olayından kayıt oluşturur.</summary>
    public static KeyLogEntry FromKeyPress(KeyPressEventArgs e) =>
        new(DateTime.Now, KeyEventType.KeyPress, DescribeChar(e.KeyChar), null, FormatModifiers(Control.ModifierKeys));

    /// <summary>Listede ve .txt dosyasında kullanılan satır biçimi.</summary>
    public override string ToString()
    {
        string code = KeyCode?.ToString() ?? "-";
        return $"[{Time:dd.MM.yyyy HH:mm:ss.fff}] {EventType,-8} | {KeyName,-15} | Kod: {code,-3} | {Modifiers}";
    }

    private static string GetKeyName(Keys key)
    {
        if (FriendlyNames.TryGetValue(key, out string? name)) return name;
        if (key >= Keys.D0 && key <= Keys.D9) return ((int)(key - Keys.D0)).ToString(); // D5 → "5"
        return key.ToString();
    }

    private static string DescribeChar(char c) => c switch
    {
        ' ' => "'(boşluk)'",
        '\r' => "'(Enter)'",
        '\b' => "'(Backspace)'",
        '\t' => "'(Tab)'",
        (char)27 => "'(Esc)'",
        _ when char.IsControl(c) => $"'(0x{(int)c:X2})'",  // Ctrl+harf gibi kontrol karakterleri
        _ => $"'{c}'"
    };

    private static string FormatModifiers(Keys modifiers)
    {
        var parts = new List<string>();
        if (modifiers.HasFlag(Keys.Control)) parts.Add("Ctrl");
        if (modifiers.HasFlag(Keys.Shift)) parts.Add("Shift");
        if (modifiers.HasFlag(Keys.Alt)) parts.Add("Alt");
        return parts.Count == 0 ? "-" : string.Join("+", parts);
    }
}
