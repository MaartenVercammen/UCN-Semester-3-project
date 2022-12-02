using RecipeRestService.ModelConversion;
using RecipesData.Database;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public class BambooSessiondataControl : IBambooSessionData
    {
        IBambooSessionAccess _BambooSessionAccess;
        public BambooSessiondataControl(IBambooSessionAccess access)
        {
            _BambooSessionAccess = access;
        }

        Guid IBambooSessionData.Add(BambooSessionDto bambooSession)
        {
            throw new NotImplementedException();
        }

        bool IBambooSessionData.Delete(Guid id)
        {
            throw new NotImplementedException();
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

        bool IBambooSessionData.Join(Guid sessionId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }

}