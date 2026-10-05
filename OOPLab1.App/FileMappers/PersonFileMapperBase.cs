using System.Text.RegularExpressions;
using OOPLab1.App.Models;
using OOPLab1.FileStorage;
namespace OOPLab1.App.FileMappers;
// Абстрактний базовий клас для mapper-ів
// Містить спільну логіку, яка використовується під час читання атрибутів із текстового файлу
public abstract class PersonFileMapperBase
    : IPersonFileMapper
{
    // Перевіряє можливість запису конкретного типу
    public abstract bool CanWrite(
        Person person);
    // Перевіряє назву типу у файлі
    public abstract bool CanRead(
        string typeName);
    // Запис конкретного типу
    public abstract void Write(
        IFileStorage storage,
        string filePath,
        Person person);
    // Читання конкретного типу
    public abstract Person? Read(
        string record);
    // Отримує значення атрибута з текстового запису
    protected string GetAttribute(
        string record,
        string attributeName)
    {
        string pattern =
            $"\"{attributeName}\"\\s*:\\s*\"([^\"]*)\"";
        Match match =
            Regex.Match(
                record,
                pattern);
        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        return "";
    }
}