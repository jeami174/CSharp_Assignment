
using Business.Helpers;
using Business.Repositories;
using Business.Services;
using Presentation_Console_MainApplication.Dialogs;


var fileService = new FileService();
var contactRepository = new ContactRepository(fileService);
var idGenerator = new GuidGenerator();
var contactService = new ContactService(contactRepository, idGenerator);

var menuDialogs = new MenuDialogs(contactService);
menuDialogs.ShowMenu();