
using Business.Factories;
using Business.Models;

namespace Business.Tests.Factories;

public class ContactFactory_Tests
{
    //Eftersom ContactFactory är en statisk klass utan beroenden så kommer jag att testa den utan mock.
    //Däremot använder jag mock i ContactService - se det testet
    //Dessa tester fokuserar på att verifiera att en ContactEntity skapas, att rätt data överförs

    [Fact]
    public void Create_ShouldReturnContactEntity_WithCorrectValues()
    {
        //Arrange
        var form = new ContactRegistrationForm
        {
            FirstName = "Martin",
            LastName = "Mikkelsen",
            Email = "Martin.Mikkelsen@example.com",
            Phone = "123456789",
            StreetAddress = "HejsanHoppsan",
            PostalCode = "12345",
            City = "Godisstaden"
        };

        var id = "ABC123";

        var expected = new ContactEntity(
        id,
        form.FirstName,
        form.LastName,
        form.Email,
        form.Phone,
        form.StreetAddress,
        form.PostalCode,
        form.City
    );

        //Act
        var result = ContactFactory.Create(form, id);

        //Assert
        Assert.Equal(expected, result);
    }
}
