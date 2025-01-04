using System.Collections.Immutable;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Business.Tests.Services;

//Dessa tester, testar logiken i ContactService dvs hantering av kontakterna.

public class ContactService_Tests
{
    private readonly Mock<IContactRepository> _contactRepositoryMock;
    private readonly Mock<IIdGenerator> _idGeneratorMock; //Väljer att mocka ID-generatorn här för att fullt utt kunna skapa realistiska test
    private readonly IContactService _contactService;

    public ContactService_Tests()
    {
        _contactRepositoryMock = new Mock<IContactRepository>();
        _idGeneratorMock = new Mock<IIdGenerator>();

        //Denna del av koden mockar GetContacts för att retunera en tom lista initialt
        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(new List<ContactEntity>());

        //Skapar ContactSErvice med mockade beroenden
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

        //Mocka ID-generering
        _idGeneratorMock.Setup(id => id.NewId()).Returns(generatedId);

        //Mock för att lagra sparade kontakter
        List<ContactEntity> savedContacts = new();

        _contactRepositoryMock.Setup(cr => cr.SaveContacts(It.IsAny<List<ContactEntity>>()))
            .Callback<List<ContactEntity>>(contacts => savedContacts = contacts);

        //Act
        _contactService.AddContact(form);

        //ASSERT

        //Verifiera att SaveContacts anropades
        _contactRepositoryMock.Verify(cr => cr.SaveContacts(It.IsAny<List<ContactEntity>>()), Times.Once);

        //Verifiera att kontakten finns i den sparade listan

        Assert.Single(savedContacts);
        Assert.Equal(expectedContact, savedContacts.First());

        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(savedContacts);

        var result = _contactService.GetContacts();

        //Assert

        Assert.Single(result);
        Assert.Equal(expectedContact, result.First());
    }

    [Fact]
    //Test som säkerställer att DeleteContact, sparar korrekt 
    //och verifierar att kontakten inte längre finns när vi hämtar listan.
    public void DeleteContact_ShouldRemoveContact_AndSave_AndBeRetrievable()
    {
        //Arrange
        var contact = new ContactEntity("123", "Jane", "Doe", "jane.doe@example.com", "987654321", "Minor Street", "67890", "Stockholm");

        //Starta med en lista som innehåller en kontakt
        List<ContactEntity> contacts = new() { contact };

        //Mock för att retunera initial kontaktlista
        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(contacts);

        //Lista för att spara uppdaterade kontakter
        List<ContactEntity> savedContacts = new();
        _contactRepositoryMock.Setup(cr => cr.SaveContacts(It.IsAny<List<ContactEntity>>()))
            .Callback<List<ContactEntity>>(updatedContacts => savedContacts = updatedContacts);

        //Återskapa ContactService för att ladda kontaktlistan från början
        var contactService = new ContactService(_contactRepositoryMock.Object, Mock.Of<IIdGenerator>());

        //Act - ta bort kontakten

        contactService.DeleteContact("123");

        //Assert - kontrollera att saveContacts anropades exakt en gång
        _contactRepositoryMock.Verify(cr => cr.SaveContacts(It.IsAny<List<ContactEntity>>()), Times.Once);

        //Assert - kontrollera om listan är tom efter radering
        Assert.Empty(savedContacts);

        //Mock för att simulera att GetContacts returnerar den uppdaterade listan
        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(savedContacts);

        //Act - hämta kontakterna igen
        var result = contactService.GetContacts();

        //Assert - verifiera att kontakten inte längre finns
        Assert.Empty(result);
    }

    [Fact]
    //Detta test verifierar att kontakten uppdateras korrekt och sparas via SaveContacts och att vi sedan kan hämta tillbaka den uppdaterade filen från GetContacts.
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

        //Startar testet med en lista som innehåller en kontakt
        List<ContactEntity> contacts = new() { existingContact };

        //Mock för att retunera initial kontaktlista
        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(contacts);

        //Lista för att lagra sparade kontakter
        List<ContactEntity> savedContacts = new();
        _contactRepositoryMock.Setup(cr => cr.SaveContacts(It.IsAny<List<ContactEntity>>()))
            .Callback<List<ContactEntity>>(updatadeContacts => savedContacts = updatadeContacts);

        //Skapar en uppdaterat kontaktformulär

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



        //Skapa ContactService för att ladda kontaktlistan
        var contactService = new ContactService(_contactRepositoryMock.Object, Mock.Of<IIdGenerator>());

        // Act - Uppdatera kontakten
        contactService.UpdateContact("123", form);

        //Assert - Verifierar att SaveContacts har anropats exakt en gång
        _contactRepositoryMock.Verify(cr => cr.SaveContacts(It.IsAny<List<ContactEntity>>()), Times.Once);
        
        //Assert - Kontrollerar att listan innehåller den uppdaterade kontakten
        Assert.Single(savedContacts);
        var updatedContact = savedContacts.First();

        Assert.Equal(expectedContact, updatedContact);

        //Använder Mock för att simulera att GetContacts retunerar den uppdaterade listan
        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(savedContacts);

        //Act - hämta kontakterna igen
        var result = contactService.GetContacts();

        //Assert - verifierar att den uppdaterade kontakten finns med
        Assert.Single(result);
        Assert.Equal(expectedContact, result[0]);

    }

    [Fact]
    public void GetContacts_ShouldReturnImmutableList()
    {
        //ARRANGE
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

        //ACT

        var result = contactService.GetContacts();

        //Assert
        Assert.Single(result);

        //Kontrollerar att resultatet är av tupen ImmutableList
        Assert.IsType<ImmutableList<ContactEntity>>(result);

       Assert.Equal(expectedContact, result.First());
    }
}
