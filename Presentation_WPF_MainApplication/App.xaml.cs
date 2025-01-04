using System.Windows;
using Business.Helpers;
using Business.Interfaces;
using Business.Repositories;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation_WPF_MainApplication.ViewModels;
using Presentation_WPF_MainApplication.Views;

namespace Presentation_WPF_MainApplication;

public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<IFileService, FileService>();
                services.AddSingleton<IContactRepository, ContactRepository>();
                services.AddSingleton<IIdGenerator, GuidGenerator>();
                services.AddSingleton<IContactService, ContactService>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddTransient<ContactListViewModel>();
                services.AddTransient<ContactListView>();

                services.AddTransient<ContactAddViewModel>();
                services.AddTransient<ContactAddView>();

                services.AddTransient<ContactEditViewModel>();
                services.AddTransient<ContactEditView>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<ContactListViewModel>();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
