
namespace Business.Models;

public readonly struct ContactEntity
{
    public string Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public string StreetAddress { get; init; }
    public string PostalCode { get; init; }
    public string City { get; init; }

    public ContactEntity(string id, string firstName, string lastName, string email, string phone, string streetAddress, string postalCode, string city)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        StreetAddress = streetAddress;
        PostalCode = postalCode;
        City = city;
    }

    public override string ToString()
    {
        return $"{Id}, {FirstName} {LastName}, {Email}, {Phone}, {StreetAddress}, {PostalCode}, {City}";
    }
}