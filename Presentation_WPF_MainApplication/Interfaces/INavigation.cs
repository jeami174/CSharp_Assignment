using Business.Models;

namespace Presentation_WPF_MainApplication.Interfaces
{
    /// <summary>
    /// Interface for navigating between different views in the application.
    /// </summary>
    public interface INavigation
    {
        /// <summary>
        /// Shows the list of contacts.
        /// </summary>
        void ShowContactsList();

        /// <summary>
        /// Shows the form for adding a new contact.
        /// </summary>
        void ShowAddContact();

        /// <summary>
        /// Shows the details of provided contact.
        /// </summary>
        void ShowContactDetails(ContactEntity contact);

        /// <summary>
        /// Shows the form for editing the provided contact.
        /// </summary>
        void ShowEditContact(ContactEntity contact);
    }
}
