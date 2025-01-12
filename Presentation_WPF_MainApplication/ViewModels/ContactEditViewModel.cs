

using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation_WPF_MainApplication.Interfaces;

namespace Presentation_WPF_MainApplication.ViewModels;

/// <summary>
/// View model for editing a contact.
/// </summary>
public partial class ContactEditViewModel(IContactService contactService, INavigation navigation) : ObservableObject
{
    private readonly IContactService _contactService = contactService;
    private readonly INavigation _navigation = navigation;

    [ObservableProperty]
    private ContactRegistrationForm _contact = new();

    private string _contactId = "";

    public void Setup(ContactEntity contactEntity)
    {
        _contactId = contactEntity.Id;
        Contact = new ContactRegistrationForm(contactEntity);
    }

    [RelayCommand]
    private void Save()
    {
        _contactService.UpdateContact(_contactId, Contact);
        _navigation.ShowContactsList();
    }

    [RelayCommand]
    private void Delete()
    {
        _contactService.DeleteContact(_contactId);
        _navigation.ShowContactsList();
    }

    [RelayCommand]
    private void Cancel()
    {
        _navigation.ShowContactsList();
    }


}
