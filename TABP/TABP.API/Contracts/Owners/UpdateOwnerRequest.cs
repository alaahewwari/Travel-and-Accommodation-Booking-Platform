namespace TABP.API.Contracts.Owners
{
    public record UpdateOwnerRequest(
         string FirstName,
        string LastName,
        string PhoneNumber
        );
}