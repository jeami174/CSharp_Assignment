using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    internal class SelectContactDialog
    {
        public static int Show(IList<ContactEntity> contacts)
        {
            for (int i = 0; i < contacts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {contacts[i]}");
            }

            Console.Write("Enter the number of the contact: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > contacts.Count)
            {
                Console.WriteLine("Invalid option.");
                return -1;
            }

            return index-1;
        }
    }
}
