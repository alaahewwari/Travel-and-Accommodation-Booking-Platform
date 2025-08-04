using Riok.Mapperly.Abstractions;
using Sieve.Models;
using TABP.Application.Hotels.Commands.AddToGallery;
using TABP.Application.Hotels.Commands.Create;
using TABP.Application.Hotels.Commands.SetThumbnail;
using TABP.Application.Hotels.Commands.Update;
using TABP.Application.Hotels.Common;
using TABP.Application.Hotels.Queries.Search;
using TABP.Domain.Application.Common;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.Common;
using TABP.Domain.Models.Hotel;
namespace TABP.Application.Hotels.Mapper
{
    [Mapper]
    public static partial class HotelMapper
    {
        public static partial HotelResponse ToHotelResponse(this Hotel hotel);
        public static partial HotelForManagementResponse ToHotelForManagementResponse(this HotelForManagement hotel);
        public static Hotel ToHotelDomain(this CreateHotelCommand command)
        {
            var hotel = ToHotelDomainInternal(command);
            hotel.CreatedAt = DateTime.UtcNow;
            hotel.UpdatedAt = DateTime.UtcNow;
            return hotel;
        }
        public static Hotel ToHotelDomain(this UpdateHotelCommand command)
        {
            var Hotel = ToHotelDomainInternal(command);
            Hotel.UpdatedAt = DateTime.UtcNow;
            return Hotel;
        }
        private static partial Hotel ToHotelDomainInternal(CreateHotelCommand command);
        private static partial Hotel ToHotelDomainInternal(UpdateHotelCommand command);
        public static partial SieveModel ToSieveModel(this SearchHotelsQuery searchHotelsQuery);
        public static partial HotelSearchParameters ToSearchParameters(this SearchHotelsQuery query);
        public static partial HotelPaginationResult ToHotelPaginationResult(this PagedResult<HotelSearchResultResponse> hotels);
        [MapperIgnoreSource(nameof(SetHotelThumbnailCommand.FileStream))]
        [MapperIgnoreSource(nameof(SetHotelThumbnailCommand.FileName))]
        private static partial HotelImage ToHotelImageDomainInternal(SetHotelThumbnailCommand command, string imageUrl);
        public static HotelImage ToHotelImageDomain(this SetHotelThumbnailCommand command, string imageUrl, ImageType type)
        {
            var hotelImage = ToHotelImageDomainInternal(command, imageUrl);
            hotelImage.CreatedAt = DateTime.UtcNow;
            hotelImage.ImageType = type;
            return hotelImage;
        }
        [MapperIgnoreSource(nameof(AddImageToHotelGalleryCommand.FileStream))]
        [MapperIgnoreSource(nameof(AddImageToHotelGalleryCommand.FileName))]
        private static partial HotelImage ToHotelImageDomainInternal(AddImageToHotelGalleryCommand command, string imageUrl);
        public static HotelImage ToHotelImageDomain(this AddImageToHotelGalleryCommand command, string imageUrl, ImageType type)
        {
            var hotelImage = ToHotelImageDomainInternal(command, imageUrl);
            hotelImage.CreatedAt = DateTime.UtcNow;
            hotelImage.ImageType = type;
            return hotelImage;
        }
    }
}