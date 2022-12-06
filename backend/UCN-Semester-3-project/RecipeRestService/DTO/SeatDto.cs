
namespace RecipeRestService.DTO{
    public class SeatDto
    {
        public string SeatId {get; set;}

        public UserDto? User {get; set;}

        public SeatDto(UserDto? user, string seatId)
        {
            User = user;
            SeatId = seatId;
        }

        public SeatDto(string seatId)
        {
            SeatId = seatId;
        }

        public SeatDto(UserDto user)
        {
            SeatId = Guid.NewGuid().ToString();
            User = user;
        }
        public SeatDto()
        {
            SeatId = Guid.NewGuid().ToString();
        }
    }
}