using RecipesData.Model;

namespace RecipesData.Database
{
    public interface IBambooSessionAccess
    {
        BambooSession GetBambooSession(Guid id);

        List<BambooSession> GetBambooSessions();

        Guid CreateBambooSession(BambooSession bambooSession);

        bool DeleteBambooSession(Guid id);

        bool JoinBambooSession(Guid sessionId, Guid userId, Guid seat);
        List<Seat> GetSeatsBySessionId(Guid sessionId);
    }
}
