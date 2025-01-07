using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation_WPF_MainApplication.Interfaces;

namespace Presentation_WPF_MainApplication.ViewModels;

public partial class ContactAddViewModel(IContactService contactService, INavigation navigation) : ObservableObject
{
    private readonly IContactService _contactService = contactService;
    private readonly INavigation _navigation = navigation;

    [ObservableProperty]
    private ContactRegistrationForm _contact = new();


    [RelayCommand]
    private void Save()
    {
        _contactService.AddContact(Contact);
        _navigation.ShowContactsList();
    }

    [RelayCommand]
    private void Cancel()
    {
        _navigation.ShowContactsList();
    }
}
