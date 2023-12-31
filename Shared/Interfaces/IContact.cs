namespace Shared.Interfaces;

public interface IContact
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    string Phone { get; set; }
    string Address { get; set; }
}
