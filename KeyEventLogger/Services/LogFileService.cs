using System.Text;
using KeyEventLogger.Models;

namespace KeyEventLogger.Services;

/// <summary>Kayıtları yerel bir .txt dosyasına yazar.</summary>
public class LogFileService
{
    /// <summary>
    /// Kayıtları UTF-8 olarak dosyaya yazar. Dosya varsa üzerine yazılır.
    /// Yazma hatalarında (IOException, UnauthorizedAccessException) istisna fırlatır;
    /// hatayı kullanıcıya göstermek çağıran formun görevidir.
    /// </summary>
    public int Save(string path, IEnumerable<KeyLogEntry> entries)
    {
        int count = 0;

        using var writer = new StreamWriter(path, append: false, Encoding.UTF8);
        writer.WriteLine("YMH219 - Key Event Logger kayıtları");
        writer.WriteLine($"Oluşturulma: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
        writer.WriteLine("Biçim: [Zaman] Olay | Tuş | Kod | Değiştirici (Ctrl/Shift/Alt)");
        writer.WriteLine(new string('-', 90));

        foreach (var entry in entries)
        {
            writer.WriteLine(entry.ToString());
            count++;
        }

        writer.WriteLine(new string('-', 90));
        writer.WriteLine($"Toplam {count} kayıt.");
        return count;
    }
}
