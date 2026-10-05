using System.Text.RegularExpressions;
using OOPLab1.App.Models;
namespace OOPLab1.App;
// Клас відповідає тільки за взаємодію користувача з консоллю
public class ConsoleMenu
{
    // Виводить головне меню
    public void Show()
    {
        Console.WriteLine();
        Console.WriteLine("===== OOP Lab 1 =====");
        Console.WriteLine("1 - Ввести та зберегти дані людей");
        Console.WriteLine("2 - Прочитати дані з файлу");
        Console.WriteLine("3 - Видалити дані з файлу");
        Console.WriteLine("4 - Пошук за прізвищем");
        Console.WriteLine("5 - Пошук студента за студентським квитком");
        Console.WriteLine(
            "6 - Кількість студентів 5 курсу, які служили в армії");
        Console.WriteLine("7 - Декламування віршів");
        Console.WriteLine("0 - Вихід");
        Console.Write("Ваш вибір: ");
    }
    // Зчитує пункт меню
    public int ReadChoice()
    {
        while (true)
        {
            string? input =
                Console.ReadLine();
            if (int.TryParse(
                    input,
                    out int choice))
            {
                return choice;
            }
            Console.Write(
                "Введіть число: ");
        }
    }
    // Зчитує кількість людей
    // За методичкою N має бути більше 5
    public int ReadPeopleCount()
    {
        while (true)
        {
            Console.Write(
                "Введіть кількість людей N (> 5): ");
            string? input =
                Console.ReadLine();
            if (int.TryParse(
                    input,
                    out int count) &&
                count > 5)
            {
                return count;
            }
            Console.WriteLine(
                "Кількість людей повинна бути більшою за 5.");
        }
    }
    // Виводить список доступних типів людей та зчитує вибір користувача
    // Список передається з Program, тому ConsoleMenu не знає конкретних класів людей
    public int ReadPersonType(
        string[] typeNames)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Оберіть тип людини:");
            for (int i = 0;
                 i < typeNames.Length;
                 i++)
            {
                Console.WriteLine(
                    $"{i + 1} - {typeNames[i]}");
            }
            Console.Write("Ваш вибір: ");
            string? input =
                Console.ReadLine();
            if (int.TryParse(
                    input,
                    out int type) &&
                type >= 1 &&
                type <= typeNames.Length)
            {
                return type;
            }
            Console.WriteLine(
                "Оберіть номер зі списку.");
        }
    }
    // Зчитує ім'я або прізвище
    public string ReadName(
        string message)
    {
        while (true)
        {
            Console.Write(message);
            string? value =
                Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value) &&
                Regex.IsMatch(
                    value,
                    @"^[A-Za-zА-Яа-яІіЇїЄєҐґ'-]+$"))
            {
                return value;
            }
            Console.WriteLine(
                "Некоректне значення. " +
                "Використовуйте лише літери, апостроф або дефіс.");
        }
    }
    // Зчитує курс
    public int ReadCourse()
    {
        while (true)
        {
            Console.Write(
                "Курс: ");
            string? input =
                Console.ReadLine();\
            if (int.TryParse(
                    input,
                    out int course) &&
                course >= 1 &&
                course <= 6)
            {
                return course;
            }
            Console.WriteLine(
                "Курс має бути числом від 1 до 6.");
        }
    }
    // Зчитує студентський квиток
    public string ReadStudentId()
    {
        while (true)
        {
            Console.Write(
                "Студентський квиток: ");
            string? value =
                Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value) &&
                Regex.IsMatch(
                    value,
                    @"^[A-ZА-ЯІЇЄҐ]{2}\d{6}$"))
            {
                return value;
            }
            Console.WriteLine(
                "Некоректний номер. " +
                "Формат: 2 літери + 6 цифр.");
        }
    }
    // Зчитує номер військового квитка
    public string ReadMilitaryId()
    {
        while (true)
        {
            Console.Write(
                "Номер військового квитка: ");
            string? value =
                Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value) &&
                Regex.IsMatch(
                    value,
                    @"^\d{7,10}$"))
            {
                return value;
            }
            Console.WriteLine(
                "Некоректний номер військового квитка.");
        }
    }
    // Зчитує логічне значення
    public bool ReadBool(
        string message)
    {
        while (true)
        {
            Console.Write(
                $"{message} (так/ні): ");
            string? value =
                Console.ReadLine();
            if (value != null)
            {
                if (value.Equals(
                        "так",
                        StringComparison.OrdinalIgnoreCase) ||
                    value.Equals(
                        "yes",
                        StringComparison.OrdinalIgnoreCase) ||
                    value.Equals(
                        "y",
                        StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                if (value.Equals(
                        "ні",
                        StringComparison.OrdinalIgnoreCase) ||
                    value.Equals(
                        "no",
                        StringComparison.OrdinalIgnoreCase) ||
                    value.Equals(
                        "n",
                        StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            Console.WriteLine(
                "Введіть «так» або «ні».");
        }
    }
    // Виводить Student
    public void PrintStudent(
        Student student)
    {
        Console.WriteLine();
        Console.WriteLine("===== Student =====");
        Console.WriteLine(
            $"Ім'я: {student.FirstName}");
        Console.WriteLine(
            $"Прізвище: {student.LastName}");
        Console.WriteLine(
            $"Курс: {student.Course}");
        Console.WriteLine(
            $"Студентський квиток: {student.StudentId}");
        Console.WriteLine(
            $"Служба в армії: {student.ServedInArmy}");
        Console.WriteLine(
            $"Військовий квиток: {student.MilitaryId}");
    }
    // Виводить людину загального типу
    public void PrintPerson(
        Person person)
    {
        Console.WriteLine();
        Console.WriteLine(
            $"===== {person.GetType().Name} =====");
        Console.WriteLine(
            $"Ім'я: {person.FirstName}");
        Console.WriteLine(
            $"Прізвище: {person.LastName}");
    }
    // Виводить результат декламування
    public void PrintPoem(
        Person person)
    {
        if (person is IPoemReciter reciter)
        {
            Console.WriteLine(
                reciter.RecitePoems());
        }
    }
}