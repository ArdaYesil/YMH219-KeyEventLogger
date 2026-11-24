namespace KeyEventLogger.Models;

/// <summary>Yakalanan klavye olayının türü.</summary>
public enum KeyEventType
{
    KeyDown,  // Tuşa basıldı (fiziksel tuş)
    KeyUp,    // Tuş bırakıldı (fiziksel tuş)
    KeyPress  // Bir karakter üretildi (ör. Shift+a → 'A')
}
