using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Hotels.Commands.AddToGallery
{
    public record AddImageToHotelGalleryCommand(long HotelId, Stream FileStream, string FileName) : IRequest<Result>;
}