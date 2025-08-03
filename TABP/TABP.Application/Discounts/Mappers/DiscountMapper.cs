using Riok.Mapperly.Abstractions;
using TABP.Application.Discounts.Commands.Create;
using TABP.Application.Discounts.Common;
using TABP.Domain.Entities;
namespace TABP.Application.Discounts.Mappers
{
    [Mapper]
    public static partial class DiscountMapper
    {
        public static Discount ToDiscountDomain(this CreateDiscountCommand command)
        {
            var discount = ToDicountDomainInternal(command);
            discount.CreatedAt = DateTime.UtcNow;
            discount.UpdatedAt = DateTime.UtcNow;
            return discount;
        }
        public static partial DiscountResponse ToDiscountResponse(this Discount discount);
        [MapperIgnoreSource(nameof(command.RoomClassId))]
        private static partial Discount ToDicountDomainInternal(CreateDiscountCommand command);
    }
}