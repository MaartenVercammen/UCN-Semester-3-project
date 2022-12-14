
namespace RecipeRestService.DTO{
    public class SeatDto
    {
        public Guid SeatId {get; set;}

        public UserDto? User {get; set;}

        public SeatDto(UserDto? user, Guid seatId)
        {
            User = user;
            SeatId = seatId;
        }

        public SeatDto(Guid seatId)
        {
            SeatId = seatId;
        }

        public SeatDto(UserDto user)
        {
            SeatId = Guid.NewGuid();
            User = user;
        }
        public SeatDto()
        {
            SeatId = Guid.NewGuid();
        }
    }
}