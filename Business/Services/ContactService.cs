using System.Collections.Immutable;
using System.Diagnostics;
using Business.Factories;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

//Här skulle jag kunna bryta upp ContactService i separata tjänster för att strikt följa SRP
//Jag skulle också kunna använda en factroy eller stratefi för skapandet av kontakter för bättre OCP


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

    private void LoadContacts()
    {
        _contacts = _contactRepository.GetContacts();
        _contactsMap = _contacts.ToDictionary(c => c.Id);
    }

    private void SaveContacts()
    {
        _contactRepository.SaveContacts(_contacts);
    }

    //I have used Microsoft´s documentation on ImmutableList to create this method:
    // https://learn.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutablelist-1?view=net-9.0
    //ImmutableList is used to ensure that the returned list of ContactEntity objects is immutable,
    //providing safety against unintended modifications 
    public ImmutableList<ContactEntity> GetContacts()
    {
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
