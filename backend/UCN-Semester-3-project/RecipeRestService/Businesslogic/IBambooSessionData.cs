using RecipeRestService.DTO;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic{

    public interface IBambooSessionData 
    {
        BambooSession? Get(Guid id);
        List<BambooSession>? Get();
        Guid Add(BambooSession bambooSession);
        bool Delete(Guid id);
        bool Join(Guid sessionId, Guid userId, Guid seat);

        List<(string, string)>? GetSeatsBySessionId(Guid sessionId);
    }
}