using RecipeRestService.ModelConversion;
using RecipesData.Database;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public class BambooSessionDataControl : IBambooSessionData
    {
        IBambooSessionAccess _BambooSessionAccess;
        public BambooSessionDataControl(IBambooSessionAccess access)
        {
            _BambooSessionAccess = access;
        }

        public Guid Add(BambooSession bambooSession)
        {
             Guid guid;
             try
             {
                 guid = _BambooSessionAccess.CreateBambooSession(bambooSession);
             }
             catch (Exception ex)
             {
                 guid = Guid.Empty;
             }
             return guid;
         }

        public bool Delete(Guid id)
        {
            bool IsDone= false;
            try{
                IsDone = _BambooSessionAccess.DeleteBambooSession(id);
            }catch(Exception ex){
                IsDone = false;
            }

            return IsDone;
        }

        public BambooSession? Get(Guid id)
        {
            BambooSession bambooSession;
            try{
                bambooSession =_BambooSessionAccess.GetBambooSession(id);
            }catch(Exception){
                bambooSession = null;
            }

            return bambooSession;
        }

        public List<BambooSession>? Get()
        {
            List<BambooSession> bambooSessions;
            try{
                bambooSessions =_BambooSessionAccess.GetBambooSessions();
            }catch(Exception ex){
                bambooSessions = null;
            }

            return bambooSessions;
        }

        public List<Seat>? GetSeatsBySessionId(Guid sessionId)
        {
            List<Seat>? seats;
            try{
                seats = _BambooSessionAccess.GetSeatsBySessionId(sessionId);
            }catch(Exception ex){
                seats = null;
            }

            return seats;
        }

        public bool Join(BambooSession session, User user, Seat seat)
        {
            bool returnValue;
            try
            {
                List<Seat> seats = _BambooSessionAccess.GetSeatsBySessionId(session.SessionId);
                foreach (Seat s in seats)
                {

                    if (s.User != null && s.User.UserId == user.UserId)
                    {
                        return false;
                    }
                }
                returnValue = _BambooSessionAccess.JoinBambooSession(session, user, seat);
            }
            catch(Exception )
            {
                returnValue = false;
            }

            return returnValue;
        }

        public Seat? GetSeatBySessionAndSeatId(BambooSession session, Guid seatId)
        {
            Seat? seat;
            try
            {
                seat = _BambooSessionAccess.GetSeatBySessionAndSeatId(session, seatId);
            }
            catch(Exception ex)
            {
                seat = null;
            }

            return seat;
        }
    }

}