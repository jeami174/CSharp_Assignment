
using System.Collections.Immutable;
using Business.Models;

namespace Business.Interfaces;

/// <summary>
/// Interface for managing a collection of contacts.
/// </summary>
public interface IContactService
{
    /// <summary>
    /// Returns an immutable list of all contacts.
    /// </summary>
    abstract ImmutableList<ContactEntity> GetContacts();

    /// <summary>
    /// Adds a new contact using the provided registration form.
    /// </summary>
    abstract void AddContact(ContactRegistrationForm form);

    /// <summary>
    /// Updates an existing contact with new details. If the contact dosen´t exist, it adds a new one.
    /// </summary>
    abstract void UpdateContact(string id, ContactRegistrationForm form);

    /// <summary>
    /// Deletes a contact based on the provided ID.
    /// </summary>
    abstract void DeleteContact(string id);
}
