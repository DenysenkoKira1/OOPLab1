using System.IO;
namespace OOPLab1.FileStorage;
// Клас відповідає тільки за фізичну роботу з текстовим файлом
public class FileStorage : IFileStorage
{
    // Додає один рядок у кінець файлу
    public void WriteLine(
        string filePath,
        string line)
    {
        using StreamWriter writer =
            new StreamWriter(
                filePath,
                append: true);
        writer.WriteLine(line);
    }
    // Читає весь файл
    public string ReadAll(
        string filePath)
    {
        // Якщо файл ще не створений, повертаємо порожній текст
        if (!File.Exists(filePath))
        {
            return "";
        }
        using StreamReader reader =
            new StreamReader(filePath);
        return reader.ReadToEnd();
    }
    // Видаляє файл
    public void Delete(
        string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}