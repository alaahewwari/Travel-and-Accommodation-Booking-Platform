using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Cities.Commands.SetThumbnail
{
    public record SetCityThumbnailCommand(int CityId, Stream FileStream, string FileName) : IRequest<Result<string>>;
}