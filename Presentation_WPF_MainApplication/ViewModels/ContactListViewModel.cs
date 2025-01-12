using System.Collections.ObjectModel;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation_WPF_MainApplication.Interfaces;

namespace Presentation_WPF_MainApplication.ViewModels;

/// <summary>
/// View model for the contact list view.
/// </summary>
public partial class ContactListViewModel : ObservableObject
{
    private readonly IContactService _contactService;
    private readonly INavigation _navigation;

    [ObservableProperty]
    private ObservableCollection<ContactEntity> _contacts = [];

    public ContactListViewModel(IContactService contactService, INavigation navigation)
    {
        _contactService = contactService;
        _navigation = navigation;

        _contacts = new ObservableCollection<ContactEntity>(_contactService.GetContacts());
    }

    [RelayCommand]
    private void GoToAddView()
    {
        _navigation.ShowAddContact();
    }

    [RelayCommand]
    private void GoToDetailsView(ContactEntity contactEntity) 
    {
        _navigation.ShowContactDetails(contactEntity);
    }
}
