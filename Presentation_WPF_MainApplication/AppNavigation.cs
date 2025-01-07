using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Presentation_WPF_MainApplication.Interfaces;
using Presentation_WPF_MainApplication.ViewModels;

namespace Presentation_WPF_MainApplication;

public class AppNavigation(IServiceProvider serviceProvider) : INavigation
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private void ChangeView(ObservableObject viewModel)
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = viewModel;
    }

    public void ShowAddContact()
    {
        ChangeView(_serviceProvider.GetRequiredService<ContactAddViewModel>());
    }

    public void ShowContactDetails(ContactEntity contact)
    {
        var contactDetailViewModel = _serviceProvider.GetRequiredService<ContactDetailViewModel>();
        contactDetailViewModel.Setup(contact);

        ChangeView(contactDetailViewModel);
    }

    public void ShowContactsList()
    {
        ChangeView(_serviceProvider.GetRequiredService<ContactListViewModel>());
    }

    public void ShowEditContact(ContactEntity contact)
    {
        var contactEditViewModel = _serviceProvider.GetRequiredService<ContactEditViewModel>();
        contactEditViewModel.Setup(contact);

        ChangeView(contactEditViewModel);
    }
}