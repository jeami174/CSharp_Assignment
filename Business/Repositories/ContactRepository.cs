
using System.Diagnostics;
using System.Text.Json;
using Business.Interfaces;
using Business.Models;


namespace Business.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly IFileService _fileService;

    public ContactRepository(IFileService fileservice)
    {
        _fileService = fileservice;
    }

    public List<ContactEntity> GetContacts()
    {
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
