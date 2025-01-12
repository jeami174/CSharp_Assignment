
using Business.Factories;
using Business.Models;

namespace Business.Tests.Factories;

public class ContactFactory_Tests
{
    // Since ContactFactory is a static class without dependencies, I will test it without using mocks.
    // However, I use mocks in ContactService tests – see that test for reference.
    // These tests focus on verifying that a ContactEntity is created and that the correct data is transferred.

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
