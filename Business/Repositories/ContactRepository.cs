
using System.Diagnostics;
using System.Text.Json;
using Business.Interfaces;
using Business.Models;


namespace Business.Repositories;

/// <summary>
/// This repository persists contact data as JSON format in a file.
/// </summary>
public class ContactRepository : IContactRepository
{
    private readonly IFileService _fileService;

    public ContactRepository(IFileService fileservice)
    {
        _fileService = fileservice;
    }

    public List<ContactEntity> GetContacts()
    {
        // Read the JSON content from file and deserialize it to a list of contacts.
        try
        {
            var json = _fileService.GetContentFromFile();
            return JsonSerializer.Deserialize<List<ContactEntity>>(json) ?? [];
        }
        catch (Exception ex)
        { 
            Debug.WriteLine(ex.Message);
            return [];
        }
    }

    public bool SaveContacts(List<ContactEntity> contacts)
    {
        // Serialize the list of contacts to JSON and save it to file.
        try
        {
            var json = JsonSerializer.Serialize(contacts);
            _fileService.SaveContentToFile(json);
            return true;
        }
        catch (Exception ex)
        {
            Debug.Write(ex.Message);
            return false;
        }
    }
}
