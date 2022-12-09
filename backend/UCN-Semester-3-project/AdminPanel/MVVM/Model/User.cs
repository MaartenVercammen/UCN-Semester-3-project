using Newtonsoft.Json.Linq;
using System;

namespace AdminPanel.MVVM.Model
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        
        public string FullName { get; set; }

        public User(Guid userId, string email, string firstName, string lastName, string password, string address,
            string role)
        {
            this.UserId = userId;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Address = address;
            this.Role = role;
            this.FullName = $"{firstName} {lastName}";
        }

        public User(string email, string firstName, string lastName, string password, string address, string role)
        {
            this.UserId = Guid.NewGuid();
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Address = address;
            this.Role = role;
            this.FullName = $"{firstName} {lastName}";
        }

        public User(Guid userId, string email, string firstName, string lastName, string password, string address)
        {
            this.UserId = userId;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Address = address;
            this.Role = "";
            this.FullName = $"{firstName} {lastName}";
        }

        public User(JObject result)
        {
            this.UserId = (Guid)result["userId"];
            this.Email = (string)result["email"];
            this.FirstName = (string)result["firstName"];
            this.LastName = (string)result["lastName"]; 
            this.Password = (string)result["password"];
            this.Address = (string)result["address"];
            this.Role = (string)result["role"];
            this.FullName = $"{FirstName} {LastName}";
        }
    }
}