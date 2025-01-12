using Business.Models;

namespace Presentation_Console_MainApplication.Dialogs
{
    /// <summary>
    /// Console dialog for inputting contact information.
    /// </summary>
    internal class InputContactDialog
    {
        public static ContactRegistrationForm Show()
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
    }
}
