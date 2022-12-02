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
            throw new NotImplementedException();
        }

        public BambooSessionDto Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<BambooSessionDto> Get()
        {
            throw new NotImplementedException();
        }

        public bool Join(Guid sessionId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }

}