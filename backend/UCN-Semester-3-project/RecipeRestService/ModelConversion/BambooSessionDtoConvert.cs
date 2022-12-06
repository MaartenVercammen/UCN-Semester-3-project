using RecipeRestService.DTO;
using RecipesData.Model;

namespace RecipeRestService.ModelConversion
{
    public class BambooSessionDtoConvert
    {
        public static BambooSessionDto? FromBambooSession(BambooSession inBambooSession)
        {
            User host = null;
            Recipe recipe = null;
            BambooSessionDto? bambooSessionDTO = null;
            if (inBambooSession != null)
            {
                bambooSessionDTO = new BambooSessionDto(inBambooSession.SessionId, inBambooSession.Host.UserId, inBambooSession.Address, inBambooSession.Recipe.RecipeId, inBambooSession.Description, inBambooSession.DateTime, inBambooSession.SlotsNumber);
                foreach (var seat in inBambooSession.Seats)
                {                 
                    bambooSessionDTO.Seats.Add(SeatDtoConvert.FromSeat(seat));
                }
            }
            return bambooSessionDTO;
        }

        public static List<BambooSessionDto>? FromBambooSessionCollection(List<BambooSession> inBambooSessions)
        {
            List<BambooSessionDto> bambooSessionDTOs = null;
            if (inBambooSessions != null)
            {
                bambooSessionDTOs = new List<BambooSessionDto>();
                BambooSessionDto tempDto;
                foreach (BambooSession bambooSession in inBambooSessions)
                {
                    if (bambooSession != null)
                    {
                        tempDto = FromBambooSession(bambooSession);
                        bambooSessionDTOs.Add(tempDto);
                    }
                }
            }
            return bambooSessionDTOs;
        }

        public static BambooSession? ToBambooSession(BambooSessionDto inDto, User host, Recipe recipe)
        {
            BambooSession bambooSession = new BambooSession(inDto.SessionId, host, inDto.Address, recipe, inDto.Description, inDto.DateTime, inDto.SlotsNumber);
            foreach (var seat in inDto.Seats)
                {
                    bambooSession.Seats.Add(SeatDtoConvert.ToSeat(seat));
                }
            return bambooSession;
        }
    }
}