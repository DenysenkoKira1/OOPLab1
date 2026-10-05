using OOPLab1.App.Models;
namespace OOPLab1.App.PersonConsoleHandlers;
// Абстракція для введення та виведення конкретного типу людини
public interface IPersonConsoleHandler
{
    // Назва типу для меню
    string TypeName { get; }
    // Перевіряє, чи відповідає handler переданому об'єкту
    bool CanHandle(Person person);
    // Вводить дані конкретного типу через консоль
    Person ReadPerson(
        ConsoleMenu menu);
    // Виводить дані конкретного типу
    void PrintPerson(
        Person person,
        ConsoleMenu menu);
}