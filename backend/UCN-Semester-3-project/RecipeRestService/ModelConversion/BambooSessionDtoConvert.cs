using RecipeRestService.DTO;
using RecipesData.Database;
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
                if (inBambooSession.Host != null)
                {
                    host = inBambooSession.Host;
                    if (inBambooSession.Recipe != null)
                    {
                        recipe = inBambooSession.Recipe;
                        bambooSessionDTO = new BambooSessionDto(inBambooSession.SessionId, host.UserId, inBambooSession.Address, inBambooSession.Recipe.RecipeId, inBambooSession.Description, inBambooSession.DateTime, inBambooSession.SlotsNumber);
                    }
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
            return new BambooSession(inDto.SessionId, host, inDto.Address, recipe, inDto.Description, inDto.DateTime, inDto.SlotsNumber);
        }
    }
}