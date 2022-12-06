using RecipeRestService.DTO;
using RecipesData.Model;

namespace RecipeRestService.ModelConversion
{
    public class BambooSessionDtoConvert
    {
        public static BambooSessionDto? FromBambooSession(BambooSession inBambooSession)
        {
            BambooSessionDto? bambooSessionDTO = null;
            if (inBambooSession != null)
            {
                bambooSessionDTO = new BambooSessionDto(inBambooSession.SessionId, inBambooSession.Address, inBambooSession.Recipe, inBambooSession.Description, inBambooSession.DateTime, inBambooSession.SlotsNumber, inBambooSession.Host);
                foreach (var seat in inBambooSession.Seats)
                {             
                    SeatDto? seatDto =  SeatDtoConvert.FromSeat(seat);
                    if(seatDto != null){   
                        bambooSessionDTO.Seats.Add(seatDto);
                    }
                }
            }
            return bambooSessionDTO;
        }

        public static List<BambooSessionDto>? FromBambooSessionCollection(List<BambooSession> inBambooSessions)
        {
            List<BambooSessionDto>? bambooSessionDTOs = null;
            if (inBambooSessions != null)
            {
                bambooSessionDTOs = new List<BambooSessionDto>();
                BambooSessionDto? tempDto;
                foreach (BambooSession bambooSession in inBambooSessions)
                {
                    if (bambooSession != null)
                    {
                        tempDto = FromBambooSession(bambooSession);
                        if(tempDto != null){
                            bambooSessionDTOs.Add(tempDto);
                        }
                    }
                }
            }
            return bambooSessionDTOs;
        }
        
        public static BambooSession? ToBambooSession(BambooSessionDto inDto)
        {
            BambooSession bambooSession = new BambooSession(inDto.SessionId, inDto.Host, inDto.Address, inDto.Recipe, inDto.Description, inDto.DateTime, inDto.SlotsNumber);
            foreach (var seatDto in inDto.Seats)
                {
                    Seat? seat = SeatDtoConvert.ToSeat(seatDto);
                    if(seat != null){
                        bambooSession.Seats.Add(seat);
                    }
                }
            return bambooSession;
        }
    }
}