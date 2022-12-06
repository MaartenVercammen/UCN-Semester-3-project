using RecipeRestService.DTO;
using RecipesData.Model;

namespace RecipeRestService.ModelConversion{

    public class SeatDtoConvert{
        public static SeatDto? FromSeat(Seat inSeat)
        {
            SeatDto? seatDto = null;
            if (inSeat != null && inSeat.User != null)
            {
                UserDto? userDto = UserDtoConvert.FromUser(inSeat.User);
                if(userDto != null){
                    seatDto = new SeatDto(userDto, inSeat.SeatId.ToString());
                }
            }
            return seatDto;
        }
    
        public static List<SeatDto>? FromSeatCollection(List<Seat> inSeatCollection)
        {
            List<SeatDto>? seatDtos = null;
            if (inSeatCollection != null)
            {
                seatDtos = new List<SeatDto>();
                SeatDto? tempDto;
                foreach (Seat seat in inSeatCollection)
                {
                    if (seat != null)
                    {
                        tempDto = FromSeat(seat);
                        if(tempDto != null){
                            seatDtos.Add(tempDto);
                        }
                    }
                }
            }
            return seatDtos;
        }
        
        public static Seat? ToSeat(SeatDto inDto)
        {
             Seat? seat = null;
            if(inDto.User != null){
                seat = new Seat(UserDtoConvert.ToUser(inDto.User), Guid.Parse(inDto.SeatId));
            }
            return seat;
        }
    }


}