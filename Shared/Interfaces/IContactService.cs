using Shared.Models;

namespace Shared.Interfaces;

public interface IContactService
{
    /// <summary>
    /// Adds a new contact to list
    /// </summary>
    /// <param name="customer">Contact to be added</param>
    /// <returns>True if the contact was added successfully, else false</returns>
    bool AddContactToList(ContactModel contact);

    /// <summary>
    /// Gets all contacts from list
    /// </summary>
    /// <returns>returns a list of contacts, else returns null if no contacts were found</returns>
    IEnumerable<ContactModel> GetAllContactsFromList();

    /// <summary>
    /// Gets a specific contact from list based on email
    /// </summary>
    /// <param name="email">The email of the contact to be searched</param>
    /// <returns>return a specific contact if found, else null</returns>
    ContactModel GetSpecificContact(string email);

    /// <summary>
    /// Deletes a specific contact based on email
    /// </summary>
    /// <param name="email">The email of the contact to be deleted</param>
    /// <returns>The deleted contact if found and removed successfully, else null</returns>
    ContactModel DeleteSpecificContact(string email);
    //public void ExitApplication();

    /// <summary>
    /// Pauses the application and waits for keypress to continue
    /// </summary>
    public void PressAnyKeyToContinue();
}
