namespace OOPLab1.App.Models;
// Абстрактний базовий клас для всіх людей
public abstract class Person
{
    // Ім'я людини
    public string FirstName { get; set; }
    // Прізвище людини
    public string LastName { get; set; }
    // Конструктор базового класу
    protected Person(
        string firstName,
        string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}