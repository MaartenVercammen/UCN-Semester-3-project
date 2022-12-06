using RecipeRestService.DTO;
using RecipesData.Model;

namespace RecipeRestService.ModelConversion{

    public class SeatDtoConvert{
        public static SeatDto? FromSeat(Seat inSeat)
        {
            SeatDto? seatDto = null;
            if (inSeat != null)
            {
                seatDto = new SeatDto(UserDtoConvert.FromUser(inSeat.User), inSeat.SeatId);
            }
            return seatDto;
        }
    
        public static List<SeatDto>? FromSeatCollection(List<Seat> inSeatCollection)
        {
            List<SeatDto> seatDtos = null;
            if (inSeatCollection != null)
            {
                seatDtos = new List<SeatDto>();
                SeatDto tempDto;
                foreach (Seat seat in inSeatCollection)
                {
                    if (seat != null)
                    {
                        tempDto = FromSeat(seat);
                        seatDtos.Add(tempDto);
                    }
                }
            }
            return seatDtos;
        }
        
        public static Seat? ToSeat(SeatDto inDto)
        {
            Seat seat = new Seat(UserDtoConvert.ToUser(inDto.User), inDto.SeatId);
            return seat;
        }
    }


}