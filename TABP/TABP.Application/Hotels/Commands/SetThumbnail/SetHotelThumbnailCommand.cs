using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Hotels.Commands.SetThumbnail
{
    public record SetHotelThumbnailCommand(long HotelId, Stream FileStream, string FileName) : IRequest<Result>;
}