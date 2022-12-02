using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Model
{
    public class BambooSessionDto
    {
        public Guid SessionId {get;set;}
        public User Host {get;set;}
        public string Address {get;set;}
        public Recipe Recipe {get;set;}
        public string Description {get;set;}
        public DateTime DateTime {get;set;}
        public int SlotsNumber {get;set;}
        public List<User> Attendees {get;set;}

        public BambooSessionDto(Guid sessionId, User host, string address, Recipe recipe, string description, DateTime dateTime, int slotsNumber)
        {
            this.SessionId = sessionId;
            this.Host = host;
            this.Address = address;
            this.Recipe = recipe;
            this.Description = description;
            this.DateTime = dateTime;
            this.SlotsNumber = slotsNumber;
            this.Attendees = new List<User>();
        }

        public BambooSessionDto(User host, string address, Recipe recipe, string description, DateTime dateTime, int slotsNumber)
        {
            this.SessionId = Guid.NewGuid();
            this.Host = host;
            this.Address = address;
            this.Recipe = recipe;
            this.Description = description;
            this.DateTime = dateTime;
            this.SlotsNumber = slotsNumber;
            this.Attendees = new List<User>();
        }

        public BambooSessionDto()
        {
        }
        
    }
}
