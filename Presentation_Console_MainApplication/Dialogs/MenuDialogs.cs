
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Presentation_Console_MainApplication.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs;

public class MenuDialogs(IContactService contactService) : IMenuDialogs
{
    private readonly IContactService _contactService = contactService;

    public void ShowMenu()
    {
        while (true)
        {
            MainMenu();
        }
    }

    private void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("----------CONTACT LIST----------");
        Console.WriteLine($"{"1.",-5} Add a Contact");
        Console.WriteLine($"{"2.",-5} Edit or delete a Contact");
        Console.WriteLine($"{"3.",-5} Show all Contacts");
        Console.WriteLine($"{"Q.",-5} Quit Application");
        Console.WriteLine("--------------------");
        Console.Write("Choose your menu option: ");

        var option = Console.ReadLine()!;

        switch (option.ToLower())
        {
            case "q":
                QuitOption();
                break;

            case "1":
                AddOption();
                break;

            case "2":
                EditOrDeleteOption();
                break;

            case "3":
                ViewOption();
                break;

            default:
                InvalidOption();
                break;
        }


    }

    private void AddOption()
    {
        Console.Clear();
        Console.WriteLine("----- Add a New Contact -----");

        var form = CreateContactForm();

        try
        {
            _contactService.AddContact(form);
            Console.WriteLine("Contact added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errpr: {ex.Message}");
        }

        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }

    // Extracted this into a separate method to separate responsibilities 
    // according to SOLID principles i.e., creating the contact form and gathering user input.
    private ContactRegistrationForm CreateContactForm()
    {
        var form = new ContactRegistrationForm();

        Console.Write("First Name: ");
        form.FirstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        form.LastName = Console.ReadLine()!;

        Console.Write("Email: ");
        form.Email = Console.ReadLine()!;

        Console.Write("Phone: ");
        form.Phone = Console.ReadLine()!;

        Console.Write("Street Adress: ");
        form.StreetAddress = Console.ReadLine()!;

        Console.Write("Postal Code: ");
        form.PostalCode = Console.ReadLine()!;

        Console.Write("City: ");
        form.City = Console.ReadLine()!;

        return form;
    }


    private void EditOrDeleteOption()
    {
        Console.Clear();
        Console.WriteLine("----- Edit or Delete a Contact -----");

        var contacts = _contactService.GetContacts();
        if (!contacts.Any())
        {
            Console.WriteLine("No contacts found.");
            Console.WriteLine("Press any key to return to the main menu..");
            Console.ReadKey();
            return;
        }

        DisplayContactList(contacts);

        Console.Write("Enter the number of the contact: ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > contacts.Count)
        {
            Console.WriteLine("Invalid option.");
            Console.WriteLine("Press any key to return to the main menu..");
            Console.ReadKey();

            return;
        }

        var selectedContact = contacts[index - 1];

        EditOrDeleteSubMenu(selectedContact);

    }

    private void EditOrDeleteSubMenu(ContactEntity contact)
    {
        Console.Clear();
        Console.WriteLine("----- Selected Contact -----");
        Console.WriteLine(contact);
        Console.WriteLine();
        Console.WriteLine("Do you want to edit or delete this contact?");
        Console.WriteLine("1. Edit Contact");
        Console.WriteLine("2. Delete Contact");
        Console.Write("Choose an option: ");

        var subOption = Console.ReadLine();

        switch (subOption)
        {
            case "1":
                EditContact(contact);
                break;

            case "2":
                DeleteContact(contact);
                break;

            default:
                InvalidOption();
                break;
        }

        Console.WriteLine("Press any Key to return to the main menu...");
        Console.ReadKey();
    }

    private void EditContact(ContactEntity contact)
    {
        var form = CreateContactForm();

        try
        {
            _contactService.UpdateContact(contact.Id, form);
            Console.WriteLine("Contact updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void DeleteContact(ContactEntity contact)
    {
        try
        {
            _contactService.DeleteContact(contact.Id);
            Console.WriteLine("Contact deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void DisplayContactList(IList<ContactEntity> contacts)
    {
        for (int i = 0; i < contacts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {contacts[i]}");
        }
    }

    private void ViewOption()
    {
        Console.Clear();
        Console.WriteLine("----- All Contacts -----");

        var contacts = _contactService.GetContacts();

        if (contacts.Any())
        {
            DisplayContactList(contacts);
        }
        else
        {
            Console.WriteLine("No contacts found.");
        }

        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();

    }

    private void QuitOption()
    {
        Console.WriteLine("Exiting application.");
        Environment.Exit(0);
    }

    private void InvalidOption()
    {
        Console.WriteLine("Invalid option. Please try again.");
        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }
}
