using RecipeRestService.DTO;
using RecipesData.Model;

namespace RecipeRestService.ModelConversion
{
    public class UserDtoConvert
    {
        public static UserDto? FromUser(User inUser)
        {
            System.Console.WriteLine(inUser.Role.ToString());
            UserDto? userDTO = null;
            if (inUser != null)
            {
                userDTO = new UserDto(inUser.UserId, inUser.Email,  inUser.FirstName, inUser.LastName, inUser.Password, inUser.Address, inUser.Role);
            }
            return userDTO;
        }

        public static List<UserDto>? FromUserCollection(List<User> inUsers)
        {
            List<UserDto> userDTOs = null;
            if (inUsers != null)
            {
                userDTOs = new List<UserDto>();
                UserDto tempDto;
                foreach (User user in inUsers)
                {
                    if (user != null)
                    {
                        tempDto = FromUser(user);
                        userDTOs.Add(tempDto);
                    }
                }
            }
            return userDTOs;
        }
        
        public static User? ToUser(UserDto inDto)
        {
            return new User(inDto.UserId, inDto.Email,  inDto.FirstName, inDto.LastName, inDto.Password, inDto.Address, (Role)Enum.Parse(typeof(Role), inDto.Role));
        }
    }
}