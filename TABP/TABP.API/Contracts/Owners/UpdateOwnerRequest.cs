namespace TABP.API.Contracts.Owners
{
    /// <summary>
    /// Request contract for updating hotel owner information.
    /// Contains modifiable personal and contact details for a hotel owner.
    /// </summary>
    /// <param name="FirstName">The owner's updated first name.</param>
    /// <param name="LastName">The owner's updated last name.</param>
    /// <param name="PhoneNumber">The owner's updated contact phone number.</param>
    public record UpdateOwnerRequest(
         string FirstName,
        string LastName,
        string PhoneNumber
        );
}
