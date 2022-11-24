using RecipesData.Database;
using RecipesData.Model;
using UserRestService.Businesslogic;

namespace RecipeRestService.Businesslogic
{
    public class UserDataControl : IUserData
    {
        IUserAccess _UserDatabaseAccess;
        public UserDataControl(IConfiguration inConfiguration)
        {
            _UserDatabaseAccess = new UserDatabaseAccess(inConfiguration);
        }

        public User? Get(Guid id)
        {
            User? foundUser;
            try
            {
                foundUser = _UserDatabaseAccess.GetUserById(id);
            }
            catch (Exception)
            {
                foundUser = null;
            }
            return foundUser;
        }

        public Guid Add(User UserToAdd)
        {
            Guid guid;
            try
            {
                guid = _UserDatabaseAccess.CreateUser(UserToAdd);
            }
            catch (Exception)
            {
                guid = Guid.Empty;
            }
            return guid;
        }

        public bool Delete(Guid id)
        {
            bool IsCompleted = false;
            try
            {
                IsCompleted = _UserDatabaseAccess.DeleteUser(id);

            }
            catch (Exception)
            {
                IsCompleted = false;
            }
            return IsCompleted;
        }

        public List<User>? Get()
        {
            List<User>? foundUsers;
            try
            {
                foundUsers = _UserDatabaseAccess.GetUsers();
            }
            catch (Exception)
            {
                foundUsers = null;
            }
            return foundUsers;
        }

        public bool Put(User userToUpdate)
        {
            bool update = false;
            try
            {
                update = _UserDatabaseAccess.UpdateUser(userToUpdate);
                update = true;
            }
            catch (Exception)
            {
                update = false;
            }
            return update;
        }
    }
}