
using RecipesData.Model;

namespace RecipeRestService.DTO
{
    public class BambooSessionDto
    {
        public Guid SessionId {get;set;}
        public string Address {get;set;}
        public Guid Recipe {get;set;}
        public string Description {get;set;}
        public DateTime DateTime {get;set;}
        public int SlotsNumber {get;set;}
        public Guid Host {get; set;}
        public List<SeatDto> Seats {get;set;}

        public BambooSessionDto(Guid sessionId, Guid host, string address, Guid recipe, string description, DateTime dateTime, int slotsNumber)
        {
            this.SessionId = sessionId;
            this.Host = host;
            this.Address = address;
            this.Recipe = recipe;
            this.Description = description;
            this.DateTime = dateTime;
            this.SlotsNumber = slotsNumber;
            this.Seats = new List<SeatDto>();
        }

        public BambooSessionDto(Guid host, string address, Guid recipe, string description, DateTime dateTime, int slotsNumber)
        {
            this.SessionId = Guid.NewGuid();
            this.Host = host;
            this.Address = address;
            this.Recipe = recipe;
            this.Description = description;
            this.DateTime = dateTime;
            this.SlotsNumber = slotsNumber;
            this.Seats = new List<SeatDto>();
        }

        public BambooSessionDto()
        {
            this.Seats = new List<SeatDto>();
        }
        
    }
}
