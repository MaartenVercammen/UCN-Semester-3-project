using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Model
{
    public enum Role 
    {
        USER,
        VERIFIEDUSER,
        ADMIN
    }
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }

        public User(Guid userId, string email, string firstName, string lastName, string password, string address, Role role)
        {
            this.UserId = userId;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Address = address;
            this.Role = role;
        }

        public User()
        {
        }

    }
}
