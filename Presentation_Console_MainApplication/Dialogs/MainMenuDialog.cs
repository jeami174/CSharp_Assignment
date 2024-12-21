using Presentation_Console_MainApplication.Interfaces;

namespace Presentation_Console_MainApplication.Dialogs
{
    internal class MainMenuDialog
    {
        public static void Show(IMainMenuOperations handler)
        {
            Console.Clear();
            Console.WriteLine("----------CONTACT LIST----------");
            Console.WriteLine($"{"1.",-5} Add a Contact");
            Console.WriteLine($"{"2.",-5} Edit  a Contact");
            Console.WriteLine($"{"3.",-5} Delete a Contact");
            Console.WriteLine($"{"4.",-5} Show all Contacts");
            Console.WriteLine($"{"Q.",-5} Quit Application");
            Console.WriteLine("--------------------");
            Console.Write("Choose your menu option: ");

            var option = Console.ReadLine()!;

            switch (option.ToLower())
            {
                case "q":
                    handler.MenuOptionQuit();
                    break;

                case "1":
                    handler.MenuOptionAddContact();
                    break;

                case "2":
                    handler.MenuOptionEditContact();
                    break;

                case "3":
                    handler.MenuOptionRemoveContact();
                    break;
                case "4":
                    handler.MenuOptionShowContacts();
                    break;

                default:
                    handler.MenuOptionInvalid();
                    break;
            }
        }
    }
}
