namespace OOPLab1.FileStorage;
// Абстракція для роботи з файлами
// PersonFileService залежить від цього інтерфейсу, а не від конкретної реалізації FileStorage
public interface IFileStorage
{
    // Записує один рядок у файл
    void WriteLine(string filePath, string line);
    // Читає весь вміст файлу
    string ReadAll(string filePath);
    // Видаляє файл
    void Delete(string filePath);
}