using RecipeRestService.DTO;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic{

    public interface IBambooSessionData 
    {
        public BambooSessionDto Get(Guid id);
        public List<BambooSessionDto> Get();
        public Guid Add(BambooSessionDto bambooSession);
        public bool Delete(Guid id);
        public bool Join(Guid sessionId, Guid userId);
    }
}