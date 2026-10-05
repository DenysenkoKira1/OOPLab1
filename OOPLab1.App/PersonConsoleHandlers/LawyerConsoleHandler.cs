using OOPLab1.App.Models;
namespace OOPLab1.App.PersonConsoleHandlers;
// Handler для введення та виведення Lawyer
public class LawyerConsoleHandler
    : IPersonConsoleHandler
{
    // Назва типу, яка показується в меню
    public string TypeName =>
        nameof(Lawyer);
    // Перевіряє, чи handler працює з Lawyer
    public bool CanHandle(
        Person person)
    {
        return person is Lawyer;
    }
    // Вводить Lawyer через ConsoleMenu
    public Person ReadPerson(
        ConsoleMenu menu)
    {
        Console.WriteLine();
        Console.WriteLine(
            "===== Введення Lawyer =====");
        string firstName =
            menu.ReadName(
                "Ім'я: ");
        string lastName =
            menu.ReadName(
                "Прізвище: ");
        return new Lawyer(
            firstName,
            lastName);
    }
    // Виводить Lawyer
    public void PrintPerson(
        Person person,
        ConsoleMenu menu)
    {
        if (person is Lawyer)
        {
            menu.PrintPerson(
                person);
        }
    }
}