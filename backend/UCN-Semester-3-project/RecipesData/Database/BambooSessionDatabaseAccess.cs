using Microsoft.Extensions.Configuration;
using RecipesData.Model;

namespace RecipesData.Database {
    public class BambooSessionDatabaseAccess : IBambooSessionAccess {

        readonly string _connectionString;

        public BambooSessionDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UcnConnection");
        }

        public BambooSessionDatabaseAccess(string connetionstring)
        {
            _connectionString = connetionstring;
        }

        //TODO: Implement method
        public BambooSession GetBambooSession(Guid id)
        {
            return null;
        }

        //TODO: Implement method
        public List<BambooSession> GetBambooSessions()
        {
            return null;
        }

        public Guid CreateBambooSession(BambooSession bambooSession)
        {
            return Guid.NewGuid();
        }

        //TODO: Implement method
        public bool DeleteBambooSession(Guid id)
        {
            return false;
        }

        public bool JoinBambooSession(Guid sessionId, Guid userId)
        {
            return false;
        }
    }
}