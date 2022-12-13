

namespace adminClient.MVVM.Model
{
    public class BambooSession
    {
        public string SessionId {get;set;}
        public string Address {get;set;}
        public string Recipe {get;set;}
        public string Description {get;set;}
        public DateTime DateTime {get;set;}
        public int SlotsNumber {get;set;}
        public string Host {get; set;}
        public List<Seat> Seats {get;set;}

        public BambooSession(string sessionId, string host, string address, string recipe, string description, DateTime dateTime, int slotsNumber)
        {
            this.SessionId = sessionId;
            this.Host = host;
            this.Address = address;
            this.Recipe = recipe;
            this.Description = description;
            this.DateTime = dateTime;
            this.SlotsNumber = slotsNumber;
            this.Seats = new List<Seat>();
        }

        public BambooSession(string host, string address, string recipe, string description, DateTime dateTime, int slotsNumber)
        {
            this.SessionId = Guid.NewGuid().ToString();
            this.Host = host;
            this.Address = address;
            this.Recipe = recipe;
            this.Description = description;
            this.DateTime = dateTime;
            this.SlotsNumber = slotsNumber;
            this.Seats = new List<Seat>();
            this.Host = Guid.Empty.ToString();
        }

        public BambooSession()
        {
            this.Seats = new List<Seat>();
            this.Address = "";
            this.Recipe = Guid.Empty.ToString();
            this.Description = "";
            this.Host =Guid.Empty.ToString();
        }
        
    }
}
