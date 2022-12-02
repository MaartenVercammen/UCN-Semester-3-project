using RecipeRestService.DTO;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic{

    public interface IBambooSessionData 
    {
        BambooSession? Get(Guid id);
        List<BambooSession>? Get();
        Guid Add(BambooSessionDto bambooSession);
        Guid Add(BambooSession bambooSession);
        bool Delete(Guid id);
        bool Join(Guid sessionId, Guid userId);
    }
}