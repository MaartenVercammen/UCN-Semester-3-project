using RecipesData.Model;

namespace RecipesData.Database
{
    public interface IUserAccess
    {
        User GetUserById(Guid id);

        List<User> GetUsers();

        Guid CreateUser(User user);

        bool UpdateUser(User user);

        bool DeleteUser(Guid id);
    }
}
