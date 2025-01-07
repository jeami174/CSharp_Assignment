using Business.Models;

namespace Presentation_WPF_MainApplication.Interfaces
{
    public interface INavigation
    {
        void ShowContactsList();
        void ShowAddContact();
        void ShowContactDetails(ContactEntity contact);
        void ShowEditContact(ContactEntity contact);
    }
}
