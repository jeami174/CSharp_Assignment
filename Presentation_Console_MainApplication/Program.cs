
using System;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using Business.Helpers;
using Business.Interfaces;
using Business.Repositories;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation_Console_MainApplication;
using Presentation_Console_MainApplication.Interfaces;

IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<IContactRepository, ContactRepository>();
        services.AddSingleton<IIdGenerator, GuidGenerator>();
        services.AddSingleton<IContactService, ContactService>();
        services.AddSingleton<IUserInterface, ConsoleUserInterface>();

    })
.Build();

var ui = host.Services.GetRequiredService<IUserInterface>();
ui.ShowUI();

