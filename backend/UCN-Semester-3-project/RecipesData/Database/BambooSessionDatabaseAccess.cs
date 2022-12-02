using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RecipesData.Model;

namespace RecipesData.Database {
    public class BambooSessionDatabaseAccess : IBambooSessionAccess {

        readonly string _connectionString;
        readonly IRecipeAccess _recipeAccess;
        readonly IUserAccess _userAccess;

        public BambooSessionDatabaseAccess(IConfiguration configuration, IRecipeAccess recipeAccess, IUserAccess userAccess)
        {
            _connectionString = configuration.GetConnectionString("UcnConnection");
            _recipeAccess = recipeAccess;
            _userAccess = userAccess;
        }


        //TODO: Implement method
        public BambooSession GetBambooSession(Guid id)
        {
            String guidString = id.ToString();
            BambooSession bambooSession = new BambooSession();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM bambooSession WHERE sessionId = @id";

                    command.Parameters.AddWithValue("@id", guidString);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        bambooSession = BuildBambooObject(reader);
                        bambooSession.Recipe = _recipeAccess.GetRecipeById(Guid.Parse(reader.GetString(reader.GetOrdinal("recipeId"))));
                        bambooSession.Host = _userAccess.GetUserById(Guid.Parse(reader.GetString(reader.GetOrdinal("hostId"))));
                    }
                    reader.Close();
                    GetParticipantsByBambooSession(connection, bambooSession);
                }
                
            }
            return bambooSession;

        }

        //TODO: Implement method
        public List<BambooSession> GetBambooSessions()
        {
            List<BambooSession> bambooSessions;
            BambooSession bambooSession = new BambooSession();

            string queryString = "SELECT sessionId FROM bambooSession";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                // Execute read
                SqlDataReader reader = readCommand.ExecuteReader();
                // Collect data
                 bambooSessions = new List<BambooSession>();
                while (reader.Read())
                {
                    bambooSession = GetBambooSession(Guid.Parse(reader.GetString(reader.GetOrdinal("sessionId"))));
                    bambooSessions.Add(bambooSession);
                }

                con.Close();
            }
            return bambooSessions;
            
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
        private void GetParticipantsByBambooSession(SqlConnection con, BambooSession bamb)
        {
            using (SqlCommand command = con.CreateCommand())
            {
                command.CommandText = "select * from bambooSessionUser WHERE sessionId = @id";

                command.Parameters.AddWithValue("@id", bamb.SessionId);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                   User user = _userAccess.GetUserById(Guid.Parse(reader.GetString(reader.GetOrdinal("userId"))));
                   bamb.Attendees.Add(user);
                }
                reader.Close();
            }
        }

        public bool JoinBambooSession(Guid sessionId, Guid userId)
        {
            bool IsDone;
            string queryString = "INSERT INTO [dbo].[bambooSessionUser]([sessionId],[userId])VALUES(@sessionId,@userId)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                // Execute read
                int rows = readCommand.ExecuteNonQuery();
                // Collect data
                IsDone = rows > 0;

                con.Close();
            }
            return IsDone;
        }

        private BambooSession BuildBambooObject(SqlDataReader reader){
            BambooSession bambooSession = new BambooSession();
            bambooSession.SessionId = Guid.Parse(reader.GetString(reader.GetOrdinal("sessionId")));
            bambooSession.Address = reader.GetString(reader.GetOrdinal("address"));
            bambooSession.DateTime = reader.GetDateTime(reader.GetOrdinal("dateTime"));
            bambooSession.Description = reader.GetString(reader.GetOrdinal("description"));
            bambooSession.SlotsNumber = reader.GetInt32(reader.GetOrdinal("slotsNumber"));
            return bambooSession;
        }
    }
}