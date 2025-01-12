namespace Presentation_Console_MainApplication.Interfaces
{
    /// <summary>
    /// Interface for the actions accessible from the main menu in the UI (user interface).
    /// </summary>
    internal interface IMainMenuOperations
    {
        /// <summary>
        /// User selects to add a new contact.
        /// </summary>
        abstract public void MenuOptionAddContact();

        /// <summary>
        /// User selects to remove a contact.
        /// </summary>
        abstract public void MenuOptionRemoveContact();

        /// <summary>
        /// User selects to edit a contact.
        /// </summary>
        abstract public void MenuOptionEditContact();

        /// <summary>
        /// User selects to show all contacts.
        /// </summary>
        abstract public void MenuOptionShowContacts();

        /// <summary>
        /// User selects to exit the application.
        /// </summary>
        abstract public void MenuOptionQuit();

        /// <summary>
        /// User selects an invalid option.
        /// </summary>
        abstract public void MenuOptionInvalid();
    }
}
