using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    /// <summary>
    /// Delegate for the edit contact dialog (EditContactDialog).
    /// Is called when the user has provided the contact information to be updated.
    /// </summary>
    internal delegate void EditContactDialogDelegate(string id, ContactRegistrationForm form);

    /// <summary>
    /// Console dialog for editing a contact.
    /// </summary>
    internal class EditContactDialog
    {
        static public void Show(IList<ContactEntity> contacts, EditContactDialogDelegate callback)
        {
            Console.Clear();
            Console.WriteLine("----- Edit a Contact -----");

            var index = SelectContactDialog.Show(contacts);

            if (index != -1)
            {
                callback(contacts[index].Id, InputContactDialog.Show());
            }

            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
