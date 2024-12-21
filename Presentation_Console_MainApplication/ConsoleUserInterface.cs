
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Presentation_Console_MainApplication.Dialogs;
using Presentation_Console_MainApplication.Interfaces;

namespace Presentation_Console_MainApplication;

//Innehåller bara interaktion med servicen och skapar dialoger

public class ConsoleUserInterface(IContactService contactService) : IUserInterface, IMainMenuOperations
{
    private readonly IContactService _contactService = contactService;

    public void ShowUI()
    {
        while (true)
        {
            MainMenuDialog.Show(this);
        }
    }

    // Delegates for dialog actions

    private void OnAddContact(ContactRegistrationForm form)
    {
        try
        {
            _contactService.AddContact(form);
            Console.WriteLine("Contact added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errpr: {ex.Message}");
        }
    }

    private void OnEditContact(string id, ContactRegistrationForm form)
    {
        try
        {
            _contactService.UpdateContact(id, form);
            Console.WriteLine("Contact updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void OnDeleteContact(string id)
    {
        try
        {
            _contactService.DeleteContact(id);
            Console.WriteLine("Contact deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Main menu actions

    public void MenuOptionAddContact()
    {
        AddContactDialog.Show(OnAddContact);
    }

    public void MenuOptionRemoveContact()
    {
        RemoveContactDialog.Show(_contactService.GetContacts(), OnDeleteContact);
    }

    public void MenuOptionEditContact()
    {
        EditContactDialog.Show(_contactService.GetContacts(), OnEditContact);
    }

    public void MenuOptionShowContacts()
    {
        ShowContactsDialog.Show(_contactService.GetContacts());
    }

    public void MenuOptionQuit()
    {
        Console.WriteLine("Exiting application.");
        Environment.Exit(0);
    }

    public void MenuOptionInvalid()
    {
        Console.WriteLine("Invalid option. Please try again.");
        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }
}
