using OOPLab1.App.Models;
using OOPLab1.FileStorage;
namespace OOPLab1.App.FileMappers;
// Mapper для Student
// Весь специфічний файловий формат Student знаходиться тільки тут
public class StudentFileMapper
    : PersonFileMapperBase
{
    public override bool CanWrite(
        Person person)
    {
        return person is Student;
    }
    public override bool CanRead(
        string typeName)
    {
        return typeName == nameof(Student);
    }
    public override void Write(
        IFileStorage storage,
        string filePath,
        Person person)
    {
        Student student =
            (Student)person;
        storage.WriteLine(
            filePath,
            $"{nameof(Student)} {student.FirstName} {{");
        storage.WriteLine(
            filePath,
            $"    \"FirstName\": \"{student.FirstName}\"");
        storage.WriteLine(
            filePath,
            $"    \"LastName\": \"{student.LastName}\"");
        storage.WriteLine(
            filePath,
            $"    \"Course\": \"{student.Course}\"");
        storage.WriteLine(
            filePath,
            $"    \"StudentId\": \"{student.StudentId}\"");
        storage.WriteLine(
            filePath,
            $"    \"ServedInArmy\": \"{student.ServedInArmy}\"");
        storage.WriteLine(
            filePath,
            $"    \"MilitaryId\": \"{student.MilitaryId}\"");
        storage.WriteLine(
            filePath,
            "}");
    }
    public override Person? Read(
        string record)
    {
        string firstName =
            GetAttribute(
                record,
                "FirstName");
        string lastName =
            GetAttribute(
                record,
                "LastName");
        string courseValue =
            GetAttribute(
                record,
                "Course");
        string studentId =
            GetAttribute(
                record,
                "StudentId");
        string servedInArmyValue =
            GetAttribute(
                record,
                "ServedInArmy");
        string militaryId =
            GetAttribute(
                record,
                "MilitaryId");
        if (!int.TryParse(
                courseValue,
                out int course))
        {
            return null;
        }
        if (!bool.TryParse(
                servedInArmyValue,
                out bool servedInArmy))
        {
            return null;
        }
        return new Student(
            firstName,
            lastName,
            course,
            studentId,
            servedInArmy,
            militaryId);
    }
}