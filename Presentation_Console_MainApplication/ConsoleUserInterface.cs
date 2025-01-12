
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Presentation_Console_MainApplication.Dialogs;
using Presentation_Console_MainApplication.Interfaces;

namespace Presentation_Console_MainApplication;

/// <summary>
/// Console-based user interface for the contact management application.
/// </summary>
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

    // Delegates for dialog actions.
    // These will be called from the dialog classes when the user interaction is complete.
    // The commands will be forwarded to the contact service.

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

    // Main menu actions (IMainMenuOperations).
    // These will create and show the appropriate dialog when the user selects an option in the main menu.

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
