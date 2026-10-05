using OOPLab1.App.Models;
using OOPLab1.FileStorage;
namespace OOPLab1.App.FileMappers;
// Mapper для FootballPlayer
public class FootballPlayerFileMapper
    : PersonFileMapperBase
{
    public override bool CanWrite(
        Person person)
    {
        return person is FootballPlayer;
    }
    public override bool CanRead(
        string typeName)
    {
        return typeName == nameof(FootballPlayer);
    }
    public override void Write(
        IFileStorage storage,
        string filePath,
        Person person)
    {
        FootballPlayer player =
            (FootballPlayer)person;
        storage.WriteLine(
            filePath,
            $"{nameof(FootballPlayer)} {player.FirstName} {{");
        storage.WriteLine(
            filePath,
            $"    \"FirstName\": \"{player.FirstName}\"");
        storage.WriteLine(
            filePath,
            $"    \"LastName\": \"{player.LastName}\"");
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
        return new FootballPlayer(
            firstName,
            lastName);
    }
}