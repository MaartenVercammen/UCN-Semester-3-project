
namespace admin_client.MVVM.Model
{
    public class Seat
    {
        public string SeatId {get; set;}

        public User? User {get; set;}

        public Seat(User? user, string seatId)
        {
            User = user;
            SeatId = seatId;
        }

        public Seat(string seatId)
        {
            SeatId = seatId;
        }

        public Seat(User user)
        {
            SeatId = Guid.NewGuid().ToString();
            User = user;
        }
        public Seat()
        {
            SeatId = Guid.NewGuid().ToString();
        }
    }
}