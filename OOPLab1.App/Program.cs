using System.Reflection;
using OOPLab1.App.Models;
using OOPLab1.App.PersonConsoleHandlers;
using OOPLab1.FileStorage;
namespace OOPLab1.App;
public class Program
{
    // Файл, у якому зберігається БД
    private const string FilePath =
        "students.txt";
    public static void Main()
    {
        // Створюємо конкретну реалізацію файлового сховища
        IFileStorage storage =
            new OOPLab1.FileStorage.FileStorage();
        // Передаємо залежність через конструктор
        PersonFileService personFileService =
            new PersonFileService(storage);
        // Об'єкт для Console I/O
        ConsoleMenu menu =
            new ConsoleMenu();
        // Автоматично знаходимо всі handlers для введення та виведення людей
        IPersonConsoleHandler[] handlers =
            GetPersonConsoleHandlers();
        while (true)
        {
            menu.Show();
            int choice =
                menu.ReadChoice();
            Console.WriteLine();
            switch (choice)
            {
                case 1:
                    EnterAndSavePeople(
                        personFileService,
                        menu,
                        handlers);
                    break;
                case 2:
                    ReadFile(
                        personFileService,
                        menu,
                        handlers);
                    break;
                case 3:
                    DeleteFile(
                        personFileService);
                    break;
                case 4:
                    SearchByLastName(
                        personFileService,
                        menu,
                        handlers);
                    break;
                case 5:
                    SearchByStudentId(
                        personFileService,
                        menu);
                    break;
                case 6:
                    ShowFifthCourseArmyStudents(
                        personFileService,
                        menu);
                    break;
                case 7:
                    ShowPoemReciters(
                        personFileService,
                        menu);
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine(
                        "Невірний пункт меню.");
                    break;
            }
        }
    }
    // Вводить N людей та записує їх у файл
    // N > 5 відповідно до вимоги методички
    private static void EnterAndSavePeople(
        PersonFileService personFileService,
        ConsoleMenu menu,
        IPersonConsoleHandler[] handlers)
    {
        // Починаємо нове заповнення БД, тому видаляємо старий файл
        personFileService.Delete(
            FilePath);
        int count =
            menu.ReadPeopleCount();
        // Створюємо масив назв типів для відображення у меню
        string[] typeNames =
            new string[handlers.Length];
        for (int i = 0;
             i < handlers.Length;
             i++)
        {
            typeNames[i] =
                handlers[i].TypeName;
        }
        for (int i = 0;
             i < count;
             i++)
        {
            Console.WriteLine();
            Console.WriteLine(
                $"===== Людина {i + 1} з {count} =====");
            int type =
                menu.ReadPersonType(
                    typeNames);
            IPersonConsoleHandler handler =
                handlers[type - 1];
            Person person =
                handler.ReadPerson(
                    menu);
            // Записуємо об'єкт через PersonFileService
            // Конкретний mapper визначається автоматично
            personFileService.SavePerson(
                FilePath,
                person);
        }
        Console.WriteLine();
        Console.WriteLine(
            $"Успішно введено та збережено {count} людей.");
    }
    // Читає людей із файлу
    private static void ReadFile(
        PersonFileService personFileService,
        ConsoleMenu menu,
        IPersonConsoleHandler[] handlers)
    {
        Person[] persons =
            personFileService.Load(
                FilePath);
        if (persons.Length == 0)
        {
            Console.WriteLine(
                "Дані відсутні.");
            return;
        }
        Console.WriteLine(
            $"З файлу прочитано {persons.Length} записів:");
        for (int i = 0;
             i < persons.Length;
             i++)
        {
            PrintPerson(
                persons[i],
                menu,
                handlers);
        }
    }
    // Видаляє дані з файлу
    private static void DeleteFile(
        PersonFileService personFileService)
    {
        personFileService.Delete(
            FilePath);
        Console.WriteLine(
            "Дані з файлу видалено.");
    }
    // Пошук за прізвищем
    private static void SearchByLastName(
        PersonFileService personFileService,
        ConsoleMenu menu,
        IPersonConsoleHandler[] handlers)
    {
        string lastName =
            menu.ReadName(
                "Введіть прізвище: ");
        Person[] persons =
            personFileService.FindByLastName(
                FilePath,
                lastName);
        if (persons.Length == 0)
        {
            Console.WriteLine(
                "Людей із таким прізвищем не знайдено.");

            return;
        }
        Console.WriteLine(
            $"Знайдено: {persons.Length}");
        for (int i = 0;
             i < persons.Length;
             i++)
        {
            PrintPerson(
                persons[i],
                menu,
                handlers);
        }
    }
    // Пошук студента за студентським квитком
    private static void SearchByStudentId(
        PersonFileService personFileService,
        ConsoleMenu menu)
    {
        string studentId =
            menu.ReadStudentId();
        Student? student =
            personFileService.FindStudentById(
                FilePath,
                studentId);
        if (student == null)
        {
            Console.WriteLine(
                "Студента з таким номером не знайдено.");\
            return;
        }
        menu.PrintStudent(
            student);
    }
    // Отримує з файлу студентів 5 курсу, які служили в армії, та виводить кількість і повні дані цих студентів
    private static void ShowFifthCourseArmyStudents(
        PersonFileService personFileService,
        ConsoleMenu menu)
    {
        Student[] students =
            personFileService.FindFifthCourseArmyStudents(
                FilePath);
        Console.WriteLine(
            "Кількість студентів 5 курсу, " +
            $"які служили в армії: {students.Length}");
        if (students.Length == 0)
        {
            Console.WriteLine(
                "Студентів, які відповідають умові, не знайдено.");
            return;
        }
        Console.WriteLine();
        Console.WriteLine(
            "Дані студентів 5 курсу, які служили в армії:");
        for (int i = 0;
             i < students.Length;
             i++)
        {
            menu.PrintStudent(
                students[i]);
        }
    }
    // Демонстрація спільного вміння декламувати вірші.
    private static void ShowPoemReciters(
        PersonFileService personFileService,
        ConsoleMenu menu)
    {
        Person[] persons =
            personFileService.Load(
                FilePath);
        bool found = false;
        for (int i = 0;
             i < persons.Length;
             i++)
        {
            if (persons[i] is IPoemReciter)
            {
                menu.PrintPoem(
                    persons[i]);
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine(
                "Людей, які декламують вірші, не знайдено.");
        }
    }
    // Виводить конкретну людину
    // Program не перевіряє Student, Lawyer або FootballPlayer напряму
    // Він знаходить відповідний handler через IPersonConsoleHandler.
    private static void PrintPerson(
        Person person,
        ConsoleMenu menu,
        IPersonConsoleHandler[] handlers)
    {
        for (int i = 0;
             i < handlers.Length;
             i++)
        {
            if (handlers[i].CanHandle(person))
            {
                handlers[i].PrintPerson(
                    person,
                    menu);
                return;
            }
        }
        // Загальний варіант, якщо handler для типу не знайдений
        menu.PrintPerson(
            person);
    }
    // Автоматично знаходить усі реалізації IPersonConsoleHandler у поточному assembly
    // Тому Program не містить залежностей від StudentConsoleHandler, FootballPlayerConsoleHandler та LawyerConsoleHandler
    private static IPersonConsoleHandler[]
        GetPersonConsoleHandlers()
    {
        Assembly assembly =
            typeof(IPersonConsoleHandler).Assembly;
        Type[] types =
            assembly.GetTypes();
        IPersonConsoleHandler[] handlers =
            new IPersonConsoleHandler[types.Length];
        int count = 0;
        for (int i = 0;
             i < types.Length;
             i++)
        {
            Type type =
                types[i];
            if (type.IsAbstract ||
                type.IsInterface)
            {
                continue;
            }
            if (!typeof(IPersonConsoleHandler)
                    .IsAssignableFrom(type))
            {
                continue;
            }
            object? instance =
                Activator.CreateInstance(type);
            if (instance is IPersonConsoleHandler handler)
            {
                handlers[count] =
                    handler;
                count++;
            }
        }
        IPersonConsoleHandler[] result =
            new IPersonConsoleHandler[count];
        for (int i = 0;
             i < count;
             i++)
        {
            result[i] =
                handlers[i];
        }
        return result;
    }
}