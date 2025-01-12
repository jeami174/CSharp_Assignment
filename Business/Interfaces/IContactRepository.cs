using Business.Models;

namespace Business.Interfaces;

/// <summary>
/// Interface for a repository that handles operations related to contacts.
/// Provides methods for retriving and saving contact data.
/// </summary>
public interface IContactRepository
{
    /// <summary>
    /// Retrieves the contacts held by the repository as a list.
    /// </summary>
    abstract List<ContactEntity> GetContacts();

    /// <summary>
    /// Updates the repository content with the provided list of contacts.
    /// </summary>
    abstract bool SaveContacts(List<ContactEntity> contacts);
}
