namespace RecipesData.Model
{
    public enum Role
    {
        ADMIN,
        USER,
        VERIFIEDUSER
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

        public User(string email, string firstName, string lastName, string password, string address, Role role)
        {
            this.UserId = Guid.NewGuid();
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Address = address;
            this.Role = role;
        }

        public User(Guid userId, string email, string firstName, string lastName, string password, string address)
        {
            this.UserId = userId;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Address = address;
        }

        public User()
        {
        }

    }
}
