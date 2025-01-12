using System.Collections.Immutable;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Business.Tests.Services;

// These tests verify the logic in ContactService, specifically contact management operations.

public class ContactService_Tests
{
    private readonly Mock<IContactRepository> _contactRepositoryMock;
    private readonly Mock<IIdGenerator> _idGeneratorMock; // Mocking the ID generator to create realistic tests
    private readonly IContactService _contactService;

    public ContactService_Tests()
    {
        _contactRepositoryMock = new Mock<IContactRepository>();
        _idGeneratorMock = new Mock<IIdGenerator>();
        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(new List<ContactEntity>());
        _contactService = new ContactService(_contactRepositoryMock.Object, _idGeneratorMock.Object);
    }

    [Fact]
    public void AddContact_ShouldAddContact_AndSave_AndbeRetrievable()
    {
        //Arrange
        var form = new ContactRegistrationForm
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "123456789",
            StreetAddress = "Main Street 1",
            PostalCode = "12345",
            City = "Eskilstuna"
        };

        var generatedId = "123";

        var expectedContact = new ContactEntity(
            generatedId,
            form.FirstName,
            form.LastName,
            form.Email,
            form.Phone,
            form.StreetAddress,
            form.PostalCode,
            form.City
        );

        // Mock ID generation
        _idGeneratorMock.Setup(id => id.NewId()).Returns(generatedId);

        // Mock to track saved contacts
        List<ContactEntity> savedContacts = new();

        _contactRepositoryMock.Setup(cr => cr.SaveContacts(It.IsAny<List<ContactEntity>>()))
            .Callback<List<ContactEntity>>(contacts => savedContacts = contacts);

        //Act
        _contactService.AddContact(form);

        //Assert
        // Verify that SaveContacts was called and contact is in the saved list
        _contactRepositoryMock.Verify(cr => cr.SaveContacts(It.IsAny<List<ContactEntity>>()), Times.Once);

        Assert.Single(savedContacts);
        Assert.Equal(expectedContact, savedContacts.First());

        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(savedContacts);

        var result = _contactService.GetContacts();

        //Assert

        Assert.Single(result);
        Assert.Equal(expectedContact, result.First());
    }

    [Fact]
    public void DeleteContact_ShouldRemoveContact_AndSave_AndBeRetrievable()
    {
        // Arrange: Start with a list containing one contact
        var contact = new ContactEntity("123", "Jane", "Doe", "jane.doe@example.com", "987654321", "Minor Street", "67890", "Stockholm");

        List<ContactEntity> contacts = new() { contact };

        // Mock to return initial contact list
        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(contacts);

        // Mock to track updated contacts
        List<ContactEntity> savedContacts = new();
        _contactRepositoryMock.Setup(cr => cr.SaveContacts(It.IsAny<List<ContactEntity>>()))
            .Callback<List<ContactEntity>>(updatedContacts => savedContacts = updatedContacts);

        // Reinitialize ContactService to reload contact list
        var contactService = new ContactService(_contactRepositoryMock.Object, Mock.Of<IIdGenerator>());

        // Act: Remove the contact
        contactService.DeleteContact("123");

        // Assert: Verify SaveContacts was called and the list is empty after deletion
        _contactRepositoryMock.Verify(cr => cr.SaveContacts(It.IsAny<List<ContactEntity>>()), Times.Once);

        Assert.Empty(savedContacts);

        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(savedContacts);

        var result = contactService.GetContacts();

        // Assert: Verify the contact is no longer retrievable
        Assert.Empty(result);
    }

    [Fact]
    public void UpdateContact_ShouldUpdateContact_AndSave_AndBeRetrievable()
    {
        //Arrange

        var existingContact = new ContactEntity(
            "123",
            "Jane",
            "Doe",
            "jane.doe@example.com",
            "987654321",
            "Main street",
            "67890",
            "Stockholm"
        );

        List<ContactEntity> contacts = new() { existingContact };

        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(contacts);

        List<ContactEntity> savedContacts = new();
        _contactRepositoryMock.Setup(cr => cr.SaveContacts(It.IsAny<List<ContactEntity>>()))
            .Callback<List<ContactEntity>>(updatadeContacts => savedContacts = updatadeContacts);

        var form = new ContactRegistrationForm
        {
            FirstName = "Updated",
            LastName = "Doe",
            Email = "updatade@example.com",
            Phone = "111222333",
            StreetAddress = "New Address",
            PostalCode = "54321",
            City = "Updatade City"
        };

        var expectedContact = new ContactEntity(
            "123",
            form.FirstName,
            form.LastName,
            form.Email,
            form.Phone,
            form.StreetAddress,
            form.PostalCode,
            form.City
        );

        var contactService = new ContactService(_contactRepositoryMock.Object, Mock.Of<IIdGenerator>());

        // Act: Update the contact
        contactService.UpdateContact("123", form);

        // Assert: Verify SaveContacts was called and the contact is updated
        _contactRepositoryMock.Verify(cr => cr.SaveContacts(It.IsAny<List<ContactEntity>>()), Times.Once);
        
        Assert.Single(savedContacts);
        var updatedContact = savedContacts.First();

        Assert.Equal(expectedContact, updatedContact);

        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(savedContacts);

        var result = contactService.GetContacts();

        // Assert: Verify the updated contact is retrievable
        Assert.Single(result);
        Assert.Equal(expectedContact, result[0]);

    }

    [Fact]
    public void GetContacts_ShouldReturnImmutableList()
    {
        //Arrange
        var expectedContact = new ContactEntity(
            "123",
            "Clark",
            "Kent",
            "clark.kent@example.com",
            "777777777",
            "Daily Planet",
            "85479",
            "Fiction world"
        );

        var contacts = new List<ContactEntity> { expectedContact};

        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(contacts);

        var contactService = new ContactService(_contactRepositoryMock.Object, Mock.Of<IIdGenerator>());

        // Act: Retrieve contacts

        var result = contactService.GetContacts();

        Assert.Single(result);

        // Assert: Verify the result is an ImmutableList and contains the expected contact
        Assert.IsType<ImmutableList<ContactEntity>>(result);

       Assert.Equal(expectedContact, result.First());
    }
}
