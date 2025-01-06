

using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class ContactRegistrationForm
{
    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Phone { get; set; } = null!;

    [Required]
    public string StreetAddress { get; set; } = null!;

    [Required]
    public string PostalCode { get; set; } = null!;

    [Required]
    public string City { get; set; } = null!;

    public ContactRegistrationForm() { }

    public ContactRegistrationForm(ContactEntity contactEntity)
    {
        FirstName = contactEntity.FirstName;
        LastName = contactEntity.LastName;
        Email = contactEntity.Email;
        Phone = contactEntity.Phone;
        StreetAddress = contactEntity.StreetAddress;
        PostalCode = contactEntity.PostalCode;
        City = contactEntity.City;
    }
}
