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
                 System.Console.WriteLine(ex.Message);
                 System.Console.WriteLine(ex.StackTrace);
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
                System.Console.WriteLine(ex.StackTrace);
                System.Console.WriteLine(ex.Message);
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
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.StackTrace);
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
                System.Console.WriteLine(ex.StackTrace);
                System.Console.WriteLine(ex.Message);
                seats = null;
            }

            return seats;
        }

        public bool Join(Guid sessionId, Guid userId, Guid seat)
        {
            bool returnValue;
            try{
                returnValue = _BambooSessionAccess.JoinBambooSession(sessionId, userId, seat);
            }catch(Exception ex){
                System.Console.WriteLine(ex.StackTrace);
                System.Console.WriteLine(ex.Message);
                returnValue = false;
            }

            return returnValue;
        }
    }

}