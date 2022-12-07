using RecipesData.Model;

namespace RecipesData.Database
{
    public interface IBambooSessionAccess
    {
        BambooSession GetBambooSession(Guid id);

        List<BambooSession> GetBambooSessions();

        Guid CreateBambooSession(BambooSession bambooSession);

        bool DeleteBambooSession(Guid sessionId);

        bool JoinBambooSession(BambooSession session, User user, Seat seat);
        List<Seat> GetSeatsBySessionId(Guid sessionId);

        Seat? GetSeatBySessionAndSeatId(BambooSession session, Guid Seat);
    }
}
