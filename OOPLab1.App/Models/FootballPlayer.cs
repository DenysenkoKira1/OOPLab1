namespace OOPLab1.App.Models;
// Футболіст є людиною та має поведінку декламування віршів
public class FootballPlayer : Person, IPoemReciter
{
    public FootballPlayer(
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