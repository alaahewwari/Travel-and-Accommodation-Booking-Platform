namespace TABP.API.Contracts.Owners
{
    /// <summary>
    /// Request contract for creating a new hotel owner in the system.
    /// Contains essential personal and contact information required for owner registration.
    /// </summary>
    /// <param name="FirstName">The owner's first name.</param>
    /// <param name="LastName">The owner's last name.</param>
    /// <param name="PhoneNumber">The owner's contact phone number for communication.</param>
    public record CreateOwnerRequest(
         string FirstName,
        string LastName,
        string PhoneNumber
        );
}