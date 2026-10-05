namespace OOPLab1.App.Models;
// Юрист є людиною та має поведінку декламування віршів
public class Lawyer : Person, IPoemReciter
{
    public Lawyer(
        string firstName,
        string lastName)
        : base(firstName, lastName)
    {
    }
    // Реалізація поведінки IPoemReciter
    public string RecitePoems()
    {
        return $"{FirstName} {LastName} декламує вірші.";
    }
}