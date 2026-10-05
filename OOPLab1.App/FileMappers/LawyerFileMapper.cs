using OOPLab1.App.Models;
using OOPLab1.FileStorage;
namespace OOPLab1.App.FileMappers;
// Mapper для Lawyer
public class LawyerFileMapper
    : PersonFileMapperBase
{
    public override bool CanWrite(
        Person person)
    {
        return person is Lawyer;
    }
    public override bool CanRead(
        string typeName)
    {
        return typeName == nameof(Lawyer);
    }
    public override void Write(
        IFileStorage storage,
        string filePath,
        Person person)
    {
        Lawyer lawyer =
            (Lawyer)person;
        storage.WriteLine(
            filePath,
            $"{nameof(Lawyer)} {lawyer.FirstName} {{");
        storage.WriteLine(
            filePath,
            $"    \"FirstName\": \"{lawyer.FirstName}\"");
        storage.WriteLine(
            filePath,
            $"    \"LastName\": \"{lawyer.LastName}\"");
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
        return new Lawyer(
            firstName,
            lastName);
    }
}