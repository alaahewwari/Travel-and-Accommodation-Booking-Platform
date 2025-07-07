namespace TABP.API.Contracts.Owners
{
    public record CreateOwnerRequest(
         string FirstName,
        string LastName,
        string PhoneNumber
        );
}