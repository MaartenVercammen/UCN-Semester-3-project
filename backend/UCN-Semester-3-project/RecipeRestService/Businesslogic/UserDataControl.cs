using RecipesData.Database;
using RecipesData.Model;
using UserRestService.Businesslogic;

namespace RecipeRestService.Businesslogic
{
    public class UserDataControl : IUserData
    {
        IUserAccess _UserAccess;
        public UserDataControl(IUserAccess access)
        {
            _UserAccess = access;
        }

        public User? Get(Guid id)
        {
            User? foundUser;
            try
            {
                foundUser = _UserAccess.GetUserById(id);
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
                guid = _UserAccess.CreateUser(UserToAdd);
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
                IsCompleted = _UserAccess.DeleteUser(id);

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
                foundUsers = _UserAccess.GetUsers();
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
                update = _UserAccess.UpdateUser(userToUpdate);
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