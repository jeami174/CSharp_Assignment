namespace Presentation_Console_MainApplication.Interfaces
{
    internal interface IMainMenuOperations
    {
        abstract public void MenuOptionAddContact();
        abstract public void MenuOptionRemoveContact();
        abstract public void MenuOptionEditContact();
        abstract public void MenuOptionShowContacts();
        abstract public void MenuOptionQuit();
        abstract public void MenuOptionInvalid();
    }
}
