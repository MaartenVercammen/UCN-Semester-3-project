using System.Data.SqlClient;
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
            BambooSession session = new BambooSession();
            string queryCreate = "INSERT INTO BambooSession (sessionId, hostId, [address], recipeId, [description], [dateTime], slotsNumber) VALUES (@sessionId, @hostId, @address, @recipeId, @description, @dateTime, @slotsNumber)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = queryCreate;
                    command.Parameters.AddWithValue("@sessionId", bambooSession.SessionId.ToString());
                    command.Parameters.AddWithValue("@hostId", bambooSession.Host.UserId.ToString());
                    command.Parameters.AddWithValue("@address", bambooSession.Address);
                    command.Parameters.AddWithValue("@recipeId", bambooSession.Recipe.RecipeId.ToString());
                    command.Parameters.AddWithValue("@description", bambooSession.Description);
                    command.Parameters.AddWithValue("@dateTime", bambooSession.DateTime);
                    command.Parameters.AddWithValue("@slotsNumber", bambooSession.SlotsNumber);

                    command.ExecuteNonQuery();
                    session.SessionId = bambooSession.SessionId;
                }
                connection.Close();
            }
            return session.SessionId;
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