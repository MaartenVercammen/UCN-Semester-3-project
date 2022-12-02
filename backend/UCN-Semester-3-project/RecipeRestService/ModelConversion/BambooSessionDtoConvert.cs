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
                bambooSessionDTO = new BambooSessionDto(inBambooSession.SessionId, inBambooSession.Address, inBambooSession.Recipe, inBambooSession.Description, inBambooSession.DateTime, inBambooSession.SlotsNumber);
            }
            return bambooSessionDTO;
        }

        public static List<BambooSession>? FromBambooSessionCollection(List<BambooSession> inBambooSessions)
        {
            List<BambooSession> bambooSessionDTOs = null;
            if (inBambooSessions != null)
            {
                bambooSessionDTOs = new List<BambooSession>();
                BambooSession tempDto;
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
        
        public static BambooSession? ToUser(BambooSessionDto inDto)
        {
            return new BambooSession(inDto.SessionId, inDto.Address, inDto.Recipe, inDto.Description, inDto.DateTime, inDto.SlotsNumber);
        }
    }
}