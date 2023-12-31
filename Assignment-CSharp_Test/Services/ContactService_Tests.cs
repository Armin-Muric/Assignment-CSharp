using Moq;
using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models;
using Shared.Services;

namespace CSharpAddressBookProject_Test.Services;

public class ContactService_Tests
{
    [Fact]
    public void AddContactToList_Should_AddAContactToContactList_ThenReturnTrue()
    {
        //Arrange
        //IContact contact = new Contact { FirstName = "Frida", LastName = "Persson", Address = "Gatan 18", Email = "mail@gmail.com", Phone = "1234567890" };

        //Mock
        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(x => x.SaveToFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        IContactService contactService = new ContactService(mockFileService.Object, new List<IContact>());
        IContact contact = new ContactModel { FirstName = "Nisse", LastName = "Hult", Address = "Gatan 18", Email = "mail@gmail.com", Phone = "1234567890" };

        //Act
        bool result = contactService.AddContactToList(contact);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void GetAllContactsFromList_Should_GetAllContactsFromContactList_Then_ReturnListOfContacts()
    {
        //Mock
        var mockFileService = new Mock<IFileService>();
        //Arrange
        //var json = @"[{ \"$type\":\"AdressbookConsoleProject.Models.Contact, AdressbookConsoleProject\", \"Id\":1,\"FirstName\":\"Frida\", \"LastName\":\"Persson\", \"Email\":\"Frida.persson@gmail.com\" }]";

        var contacts = new List<IContact> { new ContactModel { FirstName = "Nisse", LastName = "Hult", Address = "gatan 3", Email = "mail@gmail.com"} };

        string json = JsonConvert.SerializeObject(contacts, Formatting.None, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        });

        mockFileService.Setup(x => x.GetContentFromFile(It.IsAny<string>())).Returns(json);

        IContactService contactService = new ContactService(mockFileService.Object, new List<IContact>());

        //Act
        IEnumerable<IContact> result = contactService.GetAllContactsFromList();

        //Assert
        Assert.NotNull(result);
        IContact returnedContact = result.FirstOrDefault()!;
        Assert.True(result.Any());
    }


    [Fact]
    public void GetSpecificContactFromList_Should_ReturnASpecificContactBasedOnEmail_Then_ReturnSpecificContactFromList()
    {
        //Mock
        var mockFileService = new Mock<IFileService>();
        //Arrange
        IContactService contactService = new ContactService(mockFileService.Object, new List<IContact>());
        IContact contact = new ContactModel { FirstName = "Nisse", LastName = "Hult", Address = "Gatan 1", Email = "mail@gmail.com", Phone = "1234567890" };
        contactService.AddContactToList(contact);

        string emailToSearch = "mail@gmail.com";


        //Act
        IContact specificContact = contactService.GetSpecificContact(emailToSearch);

        //Assert
        Assert.NotNull(specificContact);
        Assert.Equal(emailToSearch, specificContact.Email);
        Assert.Equal(contact.FirstName, specificContact.FirstName);
    }

    [Fact]
    public void DeleteSpecificContact_Should_DeleteASpecificContactBasedOnEmail_Then_DeleteSpecificContactFromList()
    {
        //Arrange
        //Mock
        var mockFileService = new Mock<IFileService>();

        List<IContact> contacts = new List<IContact>
        {
            new ContactModel { FirstName = "Nisse", LastName = "Hult", Address = "Gatan 8", Email = "mail@gmail.com", Phone = "1234567890" }
        };
        IContactService contactService = new ContactService(mockFileService.Object, contacts);
        //Hämta emailen för kontakten som ska tas bort
        string emailToDelete = "mail@gmail.com";


        //Act
        IContact deletedContact = contactService.DeleteSpecificContact(emailToDelete);


        //Assert
        Assert.Null(contactService.GetSpecificContact(emailToDelete));

        Assert.Equal(emailToDelete, deletedContact.Email);
    }
}
