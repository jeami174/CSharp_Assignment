using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    internal delegate void AddContactDialogDelegate(ContactRegistrationForm form);

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
