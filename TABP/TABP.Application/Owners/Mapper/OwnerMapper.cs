using Riok.Mapperly.Abstractions;
using TABP.Application.Owners.Commands.Create;
using TABP.Application.Owners.Commands.Update;
using TABP.Application.Owners.Common;
using TABP.Domain.Entites;
namespace TABP.Application.Owners.Mapper
{
    [Mapper]
    public static partial class OwnerMapper
    {
        public static partial OwnerResponse ToOwnerResponse(this Owner Owner);
        public static partial Owner ToOwnerDomain(this CreateOwnerCommand command);
        public static partial Owner ToOwnerDomain(this UpdateOwnerCommand command);
    }
}