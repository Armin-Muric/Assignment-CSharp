using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models;
using System.Diagnostics;

namespace Shared.Services;

public class ContactService : IContactService
{
    public readonly FileService _fileService;
    public List<ContactModel> Contacts { get; private set; } = [];

    public ContactService(FileService fileService)
    {
        _fileService = fileService;

        //Hämtar alla kontakter från filen
        LoadContactsFromFile();

    }

    private readonly string _filePath = @"C:\Users\46704\source\Assignment-CSharp\contacts.json";


    public bool AddContactToList(ContactModel contact)
    {
        try
        {
            bool emailExists = Contacts.Any(x => x.Email == contact.Email);
            if (emailExists)
            {
                return false;
            }
            else
            {
                Contacts.Add(contact);
                var json = JsonConvert.SerializeObject(Contacts, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                });
                _fileService.SaveToFile(_filePath, json);
                return true;
            }
        }
        catch (Exception e) { Debug.WriteLine(e); return false; }
    }

    public IEnumerable<ContactModel> GetAllContactsFromList()
    {
        try
        {
            return Contacts;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }

    public ContactModel GetSpecificContact(string email)
    {
        try
        {
            var result = Contacts.FirstOrDefault(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase))!;

            if (result != null)
            {
                return result!;
            }
            else
            {
                return null!;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }

    public ContactModel DeleteSpecificContact(string email)
    {
        try
        {
            var result = Contacts.FirstOrDefault(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));

            if (result != null)
            {
                Contacts.Remove(result);

                var json = JsonConvert.SerializeObject(Contacts, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                });

                _fileService.SaveToFile(_filePath, json);

                return result!;
            }
            return null!;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }

    public void PressAnyKeyToContinue()
    {
        Console.WriteLine("\n Press any key to go back to main menu");
        Console.ReadKey();
        Console.Clear();
    }

    private void LoadContactsFromFile()
    {
        try
        {
            var content = _fileService.GetContentFromFile(_filePath);
            if (!string.IsNullOrEmpty(content))
            {
                Contacts = JsonConvert.DeserializeObject<List<ContactModel>>(content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects
                })!;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
    }
}
