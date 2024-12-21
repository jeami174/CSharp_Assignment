using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    internal delegate void EditContactDialogDelegate(string id, ContactRegistrationForm form);

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
