

using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation_WPF_MainApplication.Interfaces;

namespace Presentation_WPF_MainApplication.ViewModels;

/// <summary>
/// View model for the contact detail view.
/// </summary>
public partial class ContactDetailViewModel(IContactService contactService, INavigation navigation) : ObservableObject
{
    private readonly IContactService _contactService = contactService;
    private readonly INavigation _navigation = navigation;

    [ObservableProperty]
    private ContactEntity _contact = new();

    public void Setup(ContactEntity contactEntity)
    {
        Contact = contactEntity;
    }

    [RelayCommand]
    private void GoToEditView()
    {
        _navigation.ShowEditContact(Contact);
    }

    [RelayCommand]
    private void Delete()
    {
        _contactService.DeleteContact(Contact.Id);
        _navigation.ShowContactsList();
    }

    [RelayCommand]
    private void GoToListView()
    {
        _navigation.ShowContactsList();
    }
}
