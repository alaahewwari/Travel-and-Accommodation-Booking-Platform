namespace TABP.API.Contracts.Cities
{
    public record UpdateCityRequest(string? Name, string? Country, string? PostOffice);
}