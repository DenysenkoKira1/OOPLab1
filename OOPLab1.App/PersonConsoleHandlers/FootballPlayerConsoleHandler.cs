using OOPLab1.App.Models;
namespace OOPLab1.App.PersonConsoleHandlers;
// Handler для введення та виведення FootballPlayer
public class FootballPlayerConsoleHandler
    : IPersonConsoleHandler
{
    // Назва типу, яка показується в меню
    public string TypeName =>
        nameof(FootballPlayer);
    // Перевіряє, чи handler працює з FootballPlayer
    public bool CanHandle(
        Person person)
    {
        return person is FootballPlayer;
    }
    // Вводить FootballPlayer через ConsoleMenu
    public Person ReadPerson(
        ConsoleMenu menu)
    {
        Console.WriteLine();
        Console.WriteLine(
            "===== Введення FootballPlayer =====");
        string firstName =
            menu.ReadName(
                "Ім'я: ");
        string lastName =
            menu.ReadName(
                "Прізвище: ");
        return new FootballPlayer(
            firstName,
            lastName);
    }
    // Виводить FootballPlayer
    public void PrintPerson(
        Person person,
        ConsoleMenu menu)
    {
        if (person is FootballPlayer)
        {
            menu.PrintPerson(
                person);
        }
    }
}