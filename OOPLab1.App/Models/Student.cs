namespace OOPLab1.App.Models;
// Клас описує студента
public class Student : Person
{
    // Курс навчання
    public int Course { get; set; }
    // Номер студентського квитка
    public string StudentId { get; set; }
    // Чи служив студент в армії
    public bool ServedInArmy { get; set; }
    // Номер військового квитка для тих, хто не служив, значення може бути порожнім
    public string MilitaryId { get; set; }
    public Student(
        string firstName,
        string lastName,
        int course,
        string studentId,
        bool servedInArmy,
        string militaryId)
        : base(firstName, lastName)
    {
        Course = course;
        StudentId = studentId;
        ServedInArmy = servedInArmy;
        MilitaryId = militaryId;
    }
    // Операція Student
    public void Study()
    {
        Course++;
    }
}