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

        public User()
        {
            this.Email = "";
            this.FirstName = "";
            this.LastName = "";
            this.Password = "";
            this.Address = "";
            this.Role = "";
            this.FullName = $"{FirstName} {LastName}";
        }
    }
}