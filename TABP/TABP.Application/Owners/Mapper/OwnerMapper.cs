using Riok.Mapperly.Abstractions;
using TABP.Application.Owners.Commands.Create;
using TABP.Application.Owners.Commands.Update;
using TABP.Application.Owners.Common;
using TABP.Domain.Entities;
using TABP.Domain.Models.Owner;
namespace TABP.Application.Owners.Mapper
{
    [Mapper]
    public static partial class OwnerMapper
    {
        public static partial OwnerResponse ToOwnerResponse(this Owner Owner);
        public static partial Owner ToOwnerDomain(this CreateOwnerCommand command);
        public static partial Owner ToOwnerDomain(this UpdateOwnerCommand command);
        public static partial OwnerForManagement ToOwnerForManagement(this Owner owner);
    }
}