

using Business.Interfaces;
using Business.Models;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation_WPF_MainApplication.ViewModels;

public partial class ContactDetailViewModel(IContactService contactService, IServiceProvider serviceProvider) : ObservableObject
{
    private readonly IContactService _contactService = contactService;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    [ObservableProperty]
    private ContactEntity _contact = new();


    [RelayCommand]
    private void GoToEditView()
    {
        var contactEditViewModel = _serviceProvider.GetService<ContactEditViewModel>();
        contactEditViewModel.Setup(Contact);

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = contactEditViewModel;
    }

    [RelayCommand]
    private void Delete()
    {
        _contactService.DeleteContact(Contact.Id);

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }

    [RelayCommand]
    private void GoToListView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }
}
