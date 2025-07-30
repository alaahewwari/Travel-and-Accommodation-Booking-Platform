using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Hotels;
using TABP.API.Contracts.Images;
using TABP.Application.Hotels.Commands.AddToGallery;
using TABP.Application.Hotels.Commands.Create;
using TABP.Application.Hotels.Commands.SetThumbnail;
using TABP.Application.Hotels.Commands.Update;
using TABP.Application.Hotels.Queries.Search;
namespace TABP.API.Mapping
{
    [Mapper]
    public static partial class HotelMapping
    {
        public static partial CreateHotelCommand ToCommand(this CreateHotelRequest request);
        public static partial UpdateHotelCommand ToCommand(this UpdateHotelRequest request, long id);
        public static partial SearchHotelsQuery ToQuery(this SearchHotelsRequest request);
        public static partial AddImageToHotelGalleryCommand ToHotelGalleryCommand(this SetImageRequest request, long hotelId, Stream fileStream, string fileName);
        public static partial SetHotelThumbnailCommand ToHotelThumbnailCommand(this SetImageRequest request, long hotelId, Stream fileStream, string fileName);
    }
}