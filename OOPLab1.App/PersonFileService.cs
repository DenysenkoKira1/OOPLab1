using System.Reflection;
using System.Text.RegularExpressions;
using OOPLab1.App.FileMappers;
using OOPLab1.App.Models;
using OOPLab1.FileStorage;
namespace OOPLab1.App;
// Сервіс роботи з даними Person
public class PersonFileService
{
    private readonly IFileStorage storage;
    public PersonFileService(
        IFileStorage storage)
    {
        this.storage = storage;
    }
    // Зберігає одну людину у файл
    public void SavePerson(
        string filePath,
        Person person)
    {
        IPersonFileMapper? mapper =
            FindMapperForPerson(person);
        if (mapper == null)
        {
            throw new InvalidOperationException(
                $"Для типу {person.GetType().Name} " +
                "не знайдено mapper.");
        }
        mapper.Write(
            storage,
            filePath,
            person);
    }
    // Читає всі записи з файлу
    public Person[] Load(
        string filePath)
    {
        string content =
            storage.ReadAll(filePath);
        if (string.IsNullOrWhiteSpace(content))
        {
            return Array.Empty<Person>();
        }
        string[] records =
            SplitRecords(content);
        Person[] persons =
            new Person[records.Length];
        int count = 0;
        for (int i = 0;
             i < records.Length;
             i++)
        {
            Person? person =
                CreatePersonFromRecord(
                    records[i]);
            if (person != null)
            {
                persons[count] = person;
                count++;
            }
        }
        if (count != persons.Length)
        {
            Person[] result =
                new Person[count];
            for (int i = 0;
                 i < count;
                 i++)
            {
                result[i] = persons[i];
            }
            return result;
        }
        return persons;
    }
    // Видаляє файл
    public void Delete(
        string filePath)
    {
        storage.Delete(filePath);
    }
    // Пошук за прізвищем
    public Person[] FindByLastName(
        string filePath,
        string lastName)
    {
        Person[] persons =
            Load(filePath);
        Person[] result =
            new Person[persons.Length];
        int count = 0;
        for (int i = 0;
             i < persons.Length;
             i++)
        {
            if (persons[i].LastName == lastName)
            {
                result[count] = persons[i];
                count++;
            }
        }
        Person[] foundPersons =
            new Person[count];
        for (int i = 0;
             i < count;
             i++)
        {
            foundPersons[i] = result[i];
        }
        return foundPersons;
    }
    // Пошук студента за студентським квитком
    public Student? FindStudentById(
        string filePath,
        string studentId)
    {
        if (!Regex.IsMatch(
                studentId,
                @"^[A-ZА-ЯІЇЄҐ]{2}\d{6}$"))
        {
            return null;
        }
        Person[] persons =
            Load(filePath);
        for (int i = 0;
             i < persons.Length;
             i++)
        {
            if (persons[i] is Student student &&
                student.StudentId == studentId)
            {
                return student;
            }
        }
        return null;
    }
    // Повертає студентів 5 курсу, які служили в армії
    // Дані беруться з файлу через Load(), тому операція працює саме з даними БД.
    public Student[] FindFifthCourseArmyStudents(
        string filePath)
    {
        Person[] persons =
            Load(filePath);
        Student[] result =
            new Student[persons.Length];
        int count = 0;
        for (int i = 0;
             i < persons.Length;
             i++)
        {
            if (persons[i] is Student student &&
                student.Course == 5 &&
                student.ServedInArmy)
            {
                result[count] = student;
                count++;
            }
        }
        Student[] foundStudents =
            new Student[count];
        for (int i = 0;
             i < count;
             i++)
        {
            foundStudents[i] = result[i];
        }
        return foundStudents;
    }
    //Метод для отримання тільки кількості таких студентів
    public int CountFifthCourseArmyStudents(
        string filePath)
    {
        Student[] students =
            FindFifthCourseArmyStudents(
                filePath);

        return students.Length;
    }
    // Знаходить mapper для конкретного об'єкта
    private IPersonFileMapper? FindMapperForPerson(
        Person person)
    {
        IPersonFileMapper[] mappers =
            GetMappers();
        for (int i = 0;
             i < mappers.Length;
             i++)
        {
            if (mappers[i].CanWrite(person))
            {
                return mappers[i];
            }
        }
        return null;
    }
    // Знаходить mapper за назвою типу з текстового файлу
    private IPersonFileMapper? FindMapperForType(
        string typeName)
    {
        IPersonFileMapper[] mappers =
            GetMappers();
        for (int i = 0;
             i < mappers.Length;
             i++)
        {
            if (mappers[i].CanRead(typeName))
            {
                return mappers[i];
            }
        }
        return null;
    }
    // Автоматично знаходить усі реалізації IPersonFileMapper у поточному assembly
    private IPersonFileMapper[] GetMappers()
    {
        Assembly assembly =
            typeof(IPersonFileMapper).Assembly;
        Type[] types =
            assembly.GetTypes();
        IPersonFileMapper[] mappers =
            new IPersonFileMapper[types.Length];
        int count = 0;
        for (int i = 0;
             i < types.Length;
             i++)
        {
            Type type = types[i];
            if (type.IsAbstract ||
                type.IsInterface)
            {
                continue;
            }
            if (!typeof(IPersonFileMapper)
                    .IsAssignableFrom(type))
            {
                continue;
            }
            object? instance =
                Activator.CreateInstance(type);
            if (instance is IPersonFileMapper mapper)
            {
                mappers[count] = mapper;
                count++;
            }
        }
        IPersonFileMapper[] result =
            new IPersonFileMapper[count];
        for (int i = 0;
             i < count;
             i++)
        {
            result[i] = mappers[i];
        }
        return result;
    }
    // Розділяє один файл на окремі записи
    private string[] SplitRecords(
        string content)
    {
        string[] records =
            new string[content.Length];
        int count = 0;
        int start = 0;
        for (int i = 0;
             i < content.Length;
             i++)
        {
            if (content[i] == '}')
            {
                string record =
                    content.Substring(
                        start,
                        i - start + 1)
                    .Trim();
                if (!string.IsNullOrWhiteSpace(record))
                {
                    records[count] = record;
                    count++;
                }
                start = i + 1;
            }
        }
        string[] result =
            new string[count];
        for (int i = 0;
             i < count;
             i++)
        {
            result[i] = records[i];
        }
        return result;
    }
    // Створює Person із текстового запису
    private Person? CreatePersonFromRecord(
        string record)
    {
        string[] lines =
            record.Split(
                Environment.NewLine,
                StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length == 0)
        {
            return null;
        }
        string firstLine =
            lines[0].Trim();
        string[] headerParts =
            firstLine.Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries);
        if (headerParts.Length < 2)
        {
            return null;
        }
        string typeName =
            headerParts[0];
        IPersonFileMapper? mapper =
            FindMapperForType(typeName);
        if (mapper == null)
        {
            return null;
        }
        return mapper.Read(record);
    }
}