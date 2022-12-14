using RecipeRestService.DTO;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic{

    public interface IBambooSessionData 
    {
        BambooSession? Get(Guid id);
        List<BambooSession>? Get();
        Guid Add(BambooSession bambooSession);
        bool Delete(Guid id);
        bool Join(BambooSession session, User user, Seat seat);

        List<Seat>? GetSeatsBySessionId(Guid sessionId);

        Seat? GetSeatBySessionAndSeatId(BambooSession session, Guid seatId);
    }
}