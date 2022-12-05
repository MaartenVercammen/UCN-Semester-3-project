using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipesData.Model;

namespace RecipeRestService.DTO
{
    public class BambooSessionDto
    {
        public Guid SessionId {get;set;}
        public string Address {get;set;}
        public Recipe Recipe {get;set;}
        public string Description {get;set;}
        public DateTime DateTime {get;set;}
        public int SlotsNumber {get;set;}
        public List<Seat> Seats {get;set;}

        public User Host {get; set;}

        public BambooSessionDto(Guid sessionId, string address, Recipe recipe, string description, DateTime dateTime, int slotsNumber, User host)
        {
            this.SessionId = sessionId;
            this.Address = address;
            this.Recipe = recipe;
            this.Description = description;
            this.DateTime = dateTime;
            this.SlotsNumber = slotsNumber;
            this.Seats = new List<Seat>();
            this.Host = host;
        }

        public BambooSessionDto(string address, Recipe recipe, string description, DateTime dateTime, int slotsNumber)
        {
            this.SessionId = Guid.NewGuid();
            this.Address = address;
            this.Recipe = recipe;
            this.Description = description;
            this.DateTime = dateTime;
            this.SlotsNumber = slotsNumber;
            this.Seats = new List<Seat>();
        }

        public BambooSessionDto()
        {
            this.Seats = new List<Seat>();
        }
        
    }
}
