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
                    GetSeatsByBambooSession(connection, bambooSession);
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
             BambooSession session = new BambooSession();
             string queryCreate = "INSERT INTO BambooSession (sessionId, hostId, [address], recipeId, [description], [dateTime], slotsNumber) VALUES (@sessionId, @hostId, @address, @recipeId, @description, @dateTime, @slotsNumber)";

             string queryCreateSeats = "INSERT INTO bambooSessionUser (sessionId, seat) values (@sessionId, @seat)"; 

             using (SqlConnection connection = new SqlConnection(_connectionString))
             {
                 connection.Open();
                 SqlCommand command = connection.CreateCommand();

                var transaction = connection.BeginTransaction();

                command.Connection = connection;
                command.Transaction = transaction;
                 try{
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

                     command.CommandText = queryCreateSeats;
                     for (int i = 0; i < bambooSession.SlotsNumber; i++)
                     {
                        command.Parameters.Clear();
                        Guid seat = Guid.NewGuid();
                        command.Parameters.AddWithValue("@sessionId", bambooSession.SessionId.ToString());
                        command.Parameters.AddWithValue("@seat", seat);
                        int rows = command.ExecuteNonQuery();
                     }
                     command.Transaction.Commit();
                     
                 }
                 catch(Exception){
                    command.Transaction.Rollback();
                 }
                 connection.Close();
             }
             return session.SessionId;
         }

        public bool DeleteBambooSession(Guid sessionId)
        {
            string querySeats = "Delete bambooSessionUser where sessionId = @sessionId";
            string querryBamboo = "Delete BambooSession where sessionId = @sessionId";
            
            bool isDone = false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand  cmd = new SqlCommand(querryBamboo, conn))
            {
                conn.Open();

                var transaction = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = transaction;

                try
                {
                    cmd.CommandText = querySeats;
                    cmd.Parameters.AddWithValue("sessionId", sessionId.ToString());
                    int rows = cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.CommandText = querryBamboo;
                    cmd.Parameters.AddWithValue("sessionId", sessionId.ToString());
                    rows += cmd.ExecuteNonQuery();
                    
                    cmd.Transaction.Commit();
                    isDone = rows > 0 ? true : false;
                }
                catch (Exception)
                {
                    cmd.Transaction.Rollback();
                }

                conn.Close();
            }
            return isDone;
        }
        public void GetSeatsByBambooSession(SqlConnection con, BambooSession bamb)
        {
            using (SqlCommand command = con.CreateCommand())
            {
                command.CommandText = "select * from bambooSessionUser WHERE sessionId = @id";

                command.Parameters.AddWithValue("@id", bamb.SessionId);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User? user;
                    try{
                        user = _userAccess.GetUserById(Guid.Parse(reader.GetString(reader.GetOrdinal("userId"))));
                    }catch(Exception){
                        user = null;
                    }
                    Guid seatId = Guid.Parse(reader.GetString(reader.GetOrdinal("seat")));
                    Seat seat = new Seat(user, seatId);
                    bamb.Seats.Add(seat);
                }
                reader.Close();
            }
        }

        public bool JoinBambooSession(BambooSession session, User user, Seat seat)
        {
            bool IsDone;
            string queryString = "UPDATE [bambooSessionUser] set userId = @userId where sessionId = @sessionId and userId is NULL and seat = @seat";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(queryString, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("sessionId", session.SessionId.ToString());
                cmd.Parameters.AddWithValue("userId", user.UserId);
                cmd.Parameters.AddWithValue("seat", seat.SeatId.ToString());
                // Execute read
                int rows = cmd.ExecuteNonQuery();
                // Collect data
                IsDone = rows > 0;

                con.Close();
            }
            return IsDone;
        }

        public List<Seat> GetSeatsBySessionId(Guid sessionId)
        {
            List<Seat> seats = new List<Seat>();
            string queryString = "SELECT * from bambooSessionUser WHERE sessionId = @sessionId";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(queryString, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("sessionId", sessionId);
                // Execute read
                SqlDataReader reader = cmd.ExecuteReader();
                // Collect data
                while(reader.Read()){
                    Seat seat = new Seat(Guid.Parse(reader.GetString(reader.GetOrdinal("seat"))));
                    try
                    {
                        User user = _userAccess.GetUserById(Guid.Parse(reader.GetString(reader.GetOrdinal("userId"))));
                        seat.User = user;
                    } 
                    catch (Exception ex)
                    {
                        seat.User = null;
                    }
                    seats.Add(seat);
                }

                con.Close();
            }
            return seats;
        }

        public Seat? GetSeatBySessionAndSeatId(BambooSession session, Guid seatId) 
        {
            Seat? seat;
            string querySeat = "SELECT * from bambooSessionUser WHERE sessionId = @sessionId and seat = @seatId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(querySeat, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("sessionId", session.SessionId);
                cmd.Parameters.AddWithValue("seatId", seatId);
                // Execute read
                SqlDataReader reader = cmd.ExecuteReader();
                // Collect data
                if(reader.Read()){
                    seat = new Seat(Guid.Parse(reader.GetString(reader.GetOrdinal("seat"))));
                    try
                    {
                        User user = _userAccess.GetUserById(Guid.Parse(reader.GetString(reader.GetOrdinal("userId"))));
                        seat.User = user;
                    } 
                    catch (Exception ex)
                    {
                        seat.User = null;
                    }
                }
                else
                {
                    seat = null;
                }

                con.Close();
            }
            return seat;
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