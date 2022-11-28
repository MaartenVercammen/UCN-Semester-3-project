using RecipeRestService.DTO;
using RecipeRestService.Businesslogic;
using RecipeRestService.ModelConversion;

namespace RecipeRestService.Businesslogic
{
    public class AuthenticationDataControl : IAuthenticationData
    {

        private readonly IUserDataAccess _access

        public AuthenticationDataControl(IUserDataAccess access)
        {
            _access = access;
        }

        public UserDto Login(string email, string password){
            try{
                User user = _access.GetUserByEmail(email);
                //TODO: hashing
                if(user.Password == password){
                    return new UserDtoConvert().FromUser(user);
                }
                else{
                    return null;
                }


            }catch() {
                return null;
            }
        }   
    }
}