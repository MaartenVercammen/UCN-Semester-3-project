using RecipesData.Model;
namespace RecipeRestService.DTO
{
    public enum Role 
    {
        USER = 0,
        VERIFIEDUSER = 1,
        ADMIN = 2
    }
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }


        // TODO: Add recipes, liked recipes, disliked recipes, owned bamboo sessions
        //public List<Recipe> Recipes { get; set; }
        //public List<SwipedRecipe> LikedRecipes { get; set; }
        //public List<SwipedRecipe> DislikedRecipes { get; set; }
        //public List<BambooSession> OwnedBambooSessions { get; set; }

        public UserDto(Guid userId, string email, string firstName, string lastName, string password, string address, Role role)
        {
            this.UserId = userId;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Address = address;
            this.Role = role;
        }

        public UserDto(string email, string firstName, string lastName, string password, string address, Role role)
        {
            this.UserId = Guid.NewGuid();
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Address = address;
            this.Role = role;
        }

        public UserDto(Guid userId, string email, string firstName, string lastName, string password, string address)
        {
            this.UserId = userId;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Address = address;
        }

        public UserDto()
        {
        }

    }
}
