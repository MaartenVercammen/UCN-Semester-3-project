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
        public string Address {get;set;}
        public Recipe Recipe {get;set;}
        public string Description {get;set;}
        public DateTime DateTime {get;set;}
        public int SlotsNumber {get;set;}
        public List<User> Attendees {get;set;}

        public BambooSession(Guid sessionId, string address, Recipe recipe, string description, DateTime dateTime, int slotsNumber)
        {
            this.SessionId = sessionId;
            this.Address = address;
            this.Recipe = recipe;
            this.Description = description;
            this.DateTime = dateTime;
            this.SlotsNumber = slotsNumber;
            this.Attendees = new List<User>();
        }

        public BambooSession(string address, Recipe recipe, string description, DateTime dateTime, int slotsNumber)
        {
            this.SessionId = Guid.NewGuid();
            this.Address = address;
            this.Recipe = recipe;
            this.Description = description;
            this.DateTime = dateTime;
            this.SlotsNumber = slotsNumber;
            this.Attendees = new List<User>();
        }

        public BambooSession()
        {
        }
        
    }
}
