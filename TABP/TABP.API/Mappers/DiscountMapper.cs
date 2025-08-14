using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Discounts;
using TABP.Application.Discounts.Commands.Create;
using TABP.Application.Discounts.Common;
using TABP.Domain.Entities;
namespace TABP.API.Mappers
{
    [Mapper]
    public static partial class DiscountMapper
    {
        public static partial CreateDiscountCommand ToCommand(this CreateDiscountRequest request, long roomClassId);
    }
}