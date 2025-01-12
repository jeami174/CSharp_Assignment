

using System.Text.Json;
using Business.Interfaces;
using Business.Models;
using Business.Repositories;
using Moq;

namespace Business.Tests.Repositories;

// These tests verify serialization and deserialization, as well as exceptions when IFileService fails.
// I believe it is important to test ContactService (high level), ContactRepository (mid level), and FileService (low level)
// in separate test files to ensure separation of concerns in testing.

public class ContactRepository_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly ContactRepository _repository;

    public ContactRepository_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _repository = new ContactRepository( _fileServiceMock.Object );
    }

    [Fact]
    public void GetContacts_ShouldReturnContacts_WhenJsonIsValid()
    {
        //Arrange
        var contacts = new List<ContactEntity>()
        {
            new("1", "John", "Doe", "john@example.com", "123456789", "Main St", "12345", "Eskilstuna")
        };

        var json = JsonSerializer.Serialize(contacts);
        _fileServiceMock.Setup(fs => fs.GetContentFromFile()).Returns(json);

        //Act
        var result = _repository.GetContacts();

        //Assert
        Assert.Single(result);
        Assert.Equal("John", result[0].FirstName);
    }

    [Fact]
    public void GetContacts_ShouldReturnEmptyList_WhenJsonIsNull()
    {
        //Arrange
        _fileServiceMock.Setup(fs => fs.GetContentFromFile()).Returns((string)null!);

        //Act
        var result = _repository.GetContacts();

        //Assert
        Assert.Empty(result);

    }

    [Fact]
    public void GetContacts_ShouldHandleException_AndReturnEmptyList()
    {
        //Arrange
        _fileServiceMock.Setup(fs => fs.GetContentFromFile()).Throws(new Exception("File read error"));

        //Act
        var result = _repository.GetContacts();

        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public void SaveContacts_ShouldReturnTrue_WhenSavingIsSuccessful()
    {
        //Arrange
        var contacts = new List<ContactEntity>
        {
            new("1", "Kalle", "Kula", "kalle@kula.com", "987654321", "sol Manor", "67890", "Eskilstuna")
        };

        _fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>()));

        //Act
        var result = _repository.SaveContacts(contacts);

        //Assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.SaveContentToFile(It.IsAny<string>()), Times.Once);

    }

    [Fact]
    public void SaveContacts_ShouldReturnFalse_WhenExceptionsIsThrown()
    {
        //Arrange
        var contacts = new List<ContactEntity>()
        {
            new("1", "Kalle", "Kula", "Kalle@kula.com", "987654321", "sol Manor", "67890", "Eskilstuna")
        };

        _fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>()))
            .Throws(new Exception("Write error"));

        //Act
        var result = _repository.SaveContacts(contacts);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void SaveContacts_ShouldSerializeContactsCorrectly()
    {
        //Arrange
        var contacts = new List<ContactEntity>
        {
            new("1", "Clark", "Kent", "clark.kent@example.com", "111111111", "Daily Planet", "99999", "Metropolis")
        };

        string? serializedJson = null;

        _fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>()))
            .Callback<string>(json => serializedJson = json);

        //Act
        _repository.SaveContacts(contacts);

        //Assert
        var expectedJson = JsonSerializer.Serialize(contacts);
        Assert.Equal(expectedJson, serializedJson);
    }


}
