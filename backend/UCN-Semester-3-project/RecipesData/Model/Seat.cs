namespace RecipesData.Model;

public class Seat
{
    public Guid SeatId;

    public User? User;

    public Seat(User? user, Guid seatId)
    {
        User = user;
        SeatId = seatId;
    }

    public Seat(Guid seatId)
    {
        SeatId = seatId;
    }

    public Seat(User user)
    {
        SeatId = Guid.NewGuid();
        User = user;
    }
    public Seat()
    {
        SeatId = Guid.NewGuid();
    }
}