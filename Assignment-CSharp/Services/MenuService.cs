using CSharpAddressBookProject.Interfaces;
using Shared.Models;
using Shared.Services;
using System.Diagnostics;

namespace CSharpAddressBookProject.Services;

public class MenuService : IMenuService
{
    private readonly ContactService _contactService;

    public MenuService(ContactService contactService)
    {
        _contactService = contactService;
    }

    public void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            MenuTitle("Address Book Project");

            Console.WriteLine($"{"1. ",4} Add Contact");
            Console.WriteLine($"{"2. ",4} Show Specific Contact");
            Console.WriteLine($"{"3. ",4} Show All Contacts");
            Console.WriteLine($"{"4. ",4} Delete Contact");
            Console.WriteLine($"{"0. ",4} Exit Application");



            Console.Write("\nEnter Your Option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ShowAddContactToListMenu();
                    break;

                case "2":
                    ShowSpecificContactMenu();
                    break;

                case "3":
                    ShowGetAllContactsFromListMenu();
                    break;

                case "4":
                    ShowDeleteContactMenu();
                    break;

                case "0":
                    ShowExitMenu();
                    break;

                default:
                    Console.WriteLine("Not a valid choice, try again!");
                    Console.ReadKey();
                    break;
            }
            Console.Clear();
        }
    }

    private void ShowAddContactToListMenu()
    {
        Console.Clear();

        MenuTitle("Add Contact");
        ContactModel contact = new ContactModel();

        Console.Write("Enter Firstname: ");
        contact.FirstName = Console.ReadLine()!;

        Console.Write("Enter Lastname: ");
        contact.LastName = Console.ReadLine()!;

        Console.Write("Enter Email: ");
        contact.Email = Console.ReadLine()!;

        Console.Write("Enter Phone number: ");
        contact.Phone = Console.ReadLine()!;

        Console.Write("Enter Adress: ");
        contact.Address = Console.ReadLine()!;

        bool contactAdded = _contactService.AddContactToList(contact);

        if (contactAdded)
        {
            Console.WriteLine($"\n{contact.FirstName} has been added to the list.");
        }
        else
        {
            Console.WriteLine($"A contact with email: {contact.Email} already exists.");
        }

        _contactService.PressAnyKeyToContinue();
    }

    private void ShowSpecificContactMenu()
    {
        Console.Clear();
        MenuTitle("Show Specific Contact from Address Book");

        Console.Write("Enter email to see details: ");

        string email = Console.ReadLine()!;
        Console.WriteLine();

        ContactModel result = _contactService.GetSpecificContact(email);
        if (result != null)
        {
            Console.Clear();
            Console.WriteLine("Contact Details: \n\n");
            Console.WriteLine($"Firstname: {result.FirstName}");
            Console.WriteLine($"Lastname: {result.LastName}");
            Console.WriteLine($"Email: {result.Email}");
            Console.WriteLine($"Phone: {result.Phone}");
            Console.WriteLine($"Address: {result.Address}");
        }
        else
        {
            Console.WriteLine("No contact found with that Email.");
        }
        _contactService.PressAnyKeyToContinue();
    }

    private void ShowGetAllContactsFromListMenu()
    {
        Console.Clear();
        MenuTitle("All Contacts in Address Book");

        //ShowAllContactsInConsole();

        if (_contactService.GetAllContactsFromList() != null)
        {
            foreach (var contact in _contactService.GetAllContactsFromList())
            {
                Console.WriteLine($"Firstname: {contact.FirstName}  Lastname: {contact.LastName}    Email: {contact.Email}  Phonenumber: {contact.Phone}    Address: {contact.Address}");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");

            }
        }
        _contactService.PressAnyKeyToContinue();
    }

    private void ShowDeleteContactMenu()
    {
        Console.Clear();
        MenuTitle("Delete Contact");
        Console.Write("Enter email you want to remove: ");
        var email = Console.ReadLine()!.ToLower();

        ContactModel result = _contactService.GetSpecificContact(email);

        if (result != null)
        {
            Console.WriteLine($"Are you sure you want to delete {result.FirstName} {result.LastName}? (y/n)");
            string userInput = Console.ReadLine()!.ToLower();

            if (userInput.Equals("y"))
            {
                ContactModel deletedContact = _contactService.DeleteSpecificContact(email);
                if (deletedContact != null)
                {
                    Console.WriteLine($"\nContact {deletedContact.FirstName} {deletedContact.LastName} has successfully been deleted");
                }
                else
                {
                    Console.WriteLine("Something went wrong, try again");
                }
            }
            else
            {
                Console.WriteLine($"\nCanceled, contact {result.FirstName} has not been deleted");
            }
        }
        else
        {
            Console.WriteLine("\nNo contact with that email was found");
        }

        _contactService.PressAnyKeyToContinue();
    }

    private void ShowExitMenu()
    {
        Console.Clear();
        Console.WriteLine("Are you sure you want to exit? (y/n)");
        var input = Console.ReadLine()!.ToLower();

        if (input.Equals("y"))
        {
            Environment.Exit(0);
        }
        _contactService.PressAnyKeyToContinue();
    }

    private void MenuTitle(string menuTitle)
    {
        Console.WriteLine($"{menuTitle}\n\n");
    }
}
