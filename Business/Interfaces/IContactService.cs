
using System.Collections.Immutable;
using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    abstract ImmutableList<ContactEntity> GetContacts();
    abstract void AddContact(ContactRegistrationForm form);
    abstract void UpdateContact(string id, ContactRegistrationForm form);
    abstract void DeleteContact(string id);
}
