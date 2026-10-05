using OOPLab1.App.Models;
using OOPLab1.FileStorage;
namespace OOPLab1.App.FileMappers;
// Абстракція для збереження та зчитування конкретного типу Person
public interface IPersonFileMapper
{
    // Перевіряє, чи може mapper записати переданий об'єкт
    bool CanWrite(Person person);
    // Перевіряє назву типу з файлу
    bool CanRead(string typeName);
    // Записує об'єкт у файл
    void Write(
        IFileStorage storage,
        string filePath,
        Person person);
    // Створює об'єкт із текстового запису
    Person? Read(string record);
}