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

        BambooSessionDto IBambooSessionData.Get(Guid id)
        {
            throw new NotImplementedException();
        }

        List<BambooSessionDto> IBambooSessionData.Get()
        {
            throw new NotImplementedException();
        }

        bool IBambooSessionData.Join(Guid sessionId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }

}