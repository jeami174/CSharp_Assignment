using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    /// <summary>
    /// Delegate for the add contact dialog (AddContactDialog).
    /// Is called when the user has entered the contact information.
    /// </summary>
    internal delegate void AddContactDialogDelegate(ContactRegistrationForm form);

    /// <summary>
    /// Console dialog for adding a new contact.
    /// </summary>
    internal class AddContactDialog
    {
        static public void Show(AddContactDialogDelegate callback)
        {
            Console.Clear();
            Console.WriteLine("----- Add a New Contact -----");

            callback(InputContactDialog.Show());

            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
