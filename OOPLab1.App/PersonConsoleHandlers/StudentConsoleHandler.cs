using OOPLab1.App.Models;
namespace OOPLab1.App.PersonConsoleHandlers;
// Handler для введення та виведення Student
// Уся специфіка взаємодії зі Student знаходиться в цьому класі
public class StudentConsoleHandler
    : IPersonConsoleHandler
{
    // Назва типу, яка показується в меню
    public string TypeName =>
        nameof(Student);
    // Перевіряє, чи handler працює зі Student
    public bool CanHandle(
        Person person)
    {
        return person is Student;
    }
    // Вводить Student через ConsoleMenu
    public Person ReadPerson(
        ConsoleMenu menu)
    {
        Console.WriteLine();
        Console.WriteLine(
            "===== Введення Student =====");
        string firstName =
            menu.ReadName(
                "Ім'я: ");
        string lastName =
            menu.ReadName(
                "Прізвище: ");
        int course =
            menu.ReadCourse();
        string studentId =
            menu.ReadStudentId();
        bool servedInArmy =
            menu.ReadBool(
                "Служив(ла) в армії?");
        string militaryId = "";
        // Номер військового квитка потрібний тільки для тих, хто служив
        if (servedInArmy)
        {
            militaryId =
                menu.ReadMilitaryId();
        }
        return new Student(
            firstName,
            lastName,
            course,
            studentId,
            servedInArmy,
            militaryId);
    }
    // Виводить Student
    public void PrintPerson(
        Person person,
        ConsoleMenu menu)
    {
        if (person is Student student)
        {
            menu.PrintStudent(
                student);
        }
    }
}