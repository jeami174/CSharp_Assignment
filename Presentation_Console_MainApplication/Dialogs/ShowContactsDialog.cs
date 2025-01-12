using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    /// <summary>
    /// Console dialog for displaying all contacts in list.
    /// </summary>
    internal class ShowContactsDialog
    {
        public static void Show(IList<ContactEntity> contacts)
        {
            Console.Clear();
            Console.WriteLine("----- All Contacts -----");

            if (contacts.Any())
            {
                for (int i = 0; i < contacts.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {contacts[i]}");
                }
            }
            else
            {
                Console.WriteLine("No contacts found.");
            }

            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
