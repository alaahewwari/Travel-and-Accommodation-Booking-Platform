//using MediatR;
//using TABP.Application.Common;
//using TABP.Application.Hotels.Common;
//using TABP.Application.Hotels.Mapping;
//using TABP.Domain.Interfaces.Repositories;
//namespace TABP.Application.Hotels.Queries.GetById
//{
//    public class GetHotelByIdQueryHandler(
//        IHotelRepository hotelRepository,
//        HotelMapper mapper
//        ) : IRequestHandler<GetHotelByIdQuery, Result<HotelResponse>>
//    {
//        public async Task<Result<HotelResponse>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
//        {
//            var hotel = await hotelRepository.GetHotelByIdAsync(request.Id, cancellationToken);
//            if (hotel is null)
//            {
//                return Result<HotelResponse>.Failure(HotelErrors.HotelNotFound);
//            }
//            var hotelResponse = mapper.ToHotelResponse(hotel);
//            return Result<HotelResponse>.Success(hotelResponse);
//        }
//    }
//}