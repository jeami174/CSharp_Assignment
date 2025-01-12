using System.Collections.Immutable;
using System.Diagnostics;
using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly IIdGenerator _idGenerator;

    private List<ContactEntity> _contacts = null!;
    private Dictionary<string, ContactEntity> _contactsMap = null!;

    public ContactService(IContactRepository contactRepository, IIdGenerator idGenerator)
    {
        _contactRepository = contactRepository;
        _idGenerator = idGenerator;

        LoadContacts();
    }

    /// <summary>
    /// Load contacts from the repository into memory.
    /// </summary>
    private void LoadContacts()
    {
        try
        {
            _contacts = _contactRepository.GetContacts();

            // Map the contacts by ID in a dictionary for quick access.
            // UpdateContact and DeleteContact methods uses ID only to find the contact.
            _contactsMap = _contacts.ToDictionary(c => c.Id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to load contacts: {ex.Message}");
            _contacts = new List<ContactEntity>();
            _contactsMap = new Dictionary<string, ContactEntity>();
        }
    }

    /// <summary>
    /// Saves the current list of contacts to the repository 
    /// </summary>
    private void SaveContacts()
    {
        try
        {
            _contactRepository.SaveContacts(_contacts);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to save contacts: {ex.Message}");
        }
    }

    public ImmutableList<ContactEntity> GetContacts()
    {
        // I have used Microsoft´s documentation on ImmutableList to create this method:
        // https://learn.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutablelist-1?view=net-9.0
        // ImmutableList is used to ensure that the returned list of ContactEntity objects is immutable,
        // providing safety against unintended modifications 
        return _contacts.ToImmutableList();
    }

    public void AddContact(ContactRegistrationForm form)
    {
        ContactEntity contact = ContactFactory.Create(form, _idGenerator.NewId());
        _contacts.Add(contact);
        _contactsMap.Add(contact.Id, contact);
        SaveContacts();
    }

    public void DeleteContact(string id)
    {
        if (_contactsMap.ContainsKey(id))
        {
            ContactEntity contact = _contactsMap[id];
            _contacts.Remove(contact);
            _contactsMap.Remove(contact.Id);
            SaveContacts();
        }
    }

    public void UpdateContact(string id, ContactRegistrationForm form)
    {
        if (_contactsMap.ContainsKey(id))
        {
            ContactEntity contactToRemove = _contactsMap[id];
            _contacts.Remove(contactToRemove);
            _contactsMap.Remove(contactToRemove.Id);

            ContactEntity contactToAdd = ContactFactory.Create(form, id);
            _contacts.Add(contactToAdd);
            _contactsMap.Add(contactToAdd.Id, contactToAdd);

            SaveContacts();
        }
        else
        {
            AddContact(form);
        }
    }
}
