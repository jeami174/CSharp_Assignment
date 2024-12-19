
using System.Diagnostics;
using Business.Models;
namespace Business.Factories;

public static class ContactFactory
{
    public static ContactEntity Create(ContactRegistrationForm form, string id)
    {
        return new ContactEntity(
            id: id,
            firstName: form.FirstName,
            lastName: form.LastName,
            email: form.Email,
            phone: form.Phone,
            streetAddress: form.StreetAddress,
            postalCode: form.PostalCode,
            city: form.City
        );
    }
}
