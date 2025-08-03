using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Owners;
using TABP.Application.Owners.Commands.Create;
using TABP.Application.Owners.Commands.Update;
namespace TABP.API.Mappers
{
    [Mapper]
    public static partial class OwnerMapper
    {
        public static partial CreateOwnerCommand ToCommand(this CreateOwnerRequest request);
        public static partial UpdateOwnerCommand ToCommand(this UpdateOwnerRequest request, int id);
    }
}