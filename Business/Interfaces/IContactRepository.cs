using Business.Models;

namespace Business.Interfaces;

public interface IContactRepository
{
    abstract List<ContactEntity> GetContacts();
    abstract bool SaveContacts(List<ContactEntity> contacts);
}
