using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    internal delegate void RemoveContactDialogDelegate(string id);

    internal class RemoveContactDialog
    {
        static public void Show(IList<ContactEntity> contacts, RemoveContactDialogDelegate callback) //Delegate-konceptet - design pattern
        {
            Console.Clear();
            Console.WriteLine("----- Remove a Contact -----");

            var index = SelectContactDialog.Show(contacts);

            if (index != -1)
            {
                callback(contacts[index].Id);
            }

            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
