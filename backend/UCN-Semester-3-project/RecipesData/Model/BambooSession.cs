using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Model
{
    public class BambooSession
    {
        public Guid SessionId {get;set;}

        public User Host {get; set;}
        public string Address {get;set;}
        public Recipe Recipe {get;set;}
        public string Description {get;set;}
        public DateTime DateTime {get;set;}
        public int SlotsNumber {get;set;}

        public List<Seat> Seats;

        public BambooSession(Guid sessionId, User host, string address, Recipe recipe, string description, DateTime dateTime, int slotsNumber)
        {
            this.SessionId = sessionId;
            this.Host = host;
            this.Address = address;
            this.Recipe = recipe;
            this.Description = description;
            this.DateTime = dateTime;
            this.SlotsNumber = slotsNumber;
            this.Seats = new List<Seat>();
            this.Host = host;
        }

        public BambooSession(string address, User host, Recipe recipe, string description, DateTime dateTime, int slotsNumber)
        {
            this.SessionId = Guid.NewGuid();
            this.Host = host;
            this.Address = address;
            this.Recipe = recipe;
            this.Description = description;
            this.DateTime = dateTime;
            this.SlotsNumber = slotsNumber;
            this.Seats = new List<Seat>();
        }

        public BambooSession()
        {
            this.Seats = new List<Seat>();
            Host = new User();
            Address = "";
            Recipe = new Recipe();
            Description = "";
        }
        
    }
}
