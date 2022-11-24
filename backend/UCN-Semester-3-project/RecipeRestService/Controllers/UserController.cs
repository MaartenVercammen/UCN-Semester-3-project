using Microsoft.AspNetCore.Mvc;
using RecipeRestService.Businesslogic;
using RecipeRestService.DTO;
using RecipeRestService.ModelConversion;
using RecipesData.Model;

namespace UserRestService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserDataControl _rControl;
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
            _rControl = new UserDataControl(_configuration);
        }

        [HttpGet, Route("{id}")]
        public ActionResult<UserDto> Get(string id)
        {
            Guid UserId = Guid.Parse(id);

            ActionResult<UserDto> foundReturn;
            User? foundUser = _rControl.Get(UserId);
            if (foundUser != null)
            {
                foundReturn = Ok(UserDtoConvert.FromUser(foundUser));
            }
            else
            {
                foundReturn = NotFound();
            }
            return foundReturn;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> Get()
        {
            ActionResult<List<UserDto>> foundReturn;
            // retrieve and convert data
            List<User>? foundUsers = _rControl.Get();
            List<UserDto>? foundDts = null;
            if (foundUsers != null)
            {
                foundDts = UserDtoConvert.FromUserCollection(foundUsers);
            }
            // evaluate
            if (foundDts != null)
            {
                if (foundDts.Count > 0)
                {
                    foundReturn = Ok(foundDts);                 // Statuscode 200
                }
                else
                {
                    foundReturn = new StatusCodeResult(204);    // Ok, but no content
                }
            }
            else
            {
                foundReturn = new StatusCodeResult(500);        // Internal server error
            }
            // send response back to client
            return foundReturn;
        }

        [HttpPost]
        public ActionResult Post([FromBody] UserDto inUser)
        {
            ActionResult foundReturn;
            Guid insertedGuid = Guid.Empty;
            if (inUser != null)
            {
                insertedGuid = _rControl.Add(UserDtoConvert.ToUser(inUser));
            }
            if (insertedGuid != Guid.Empty)
            {
                foundReturn = Ok(insertedGuid);
            }
            else
            {
                foundReturn = new StatusCodeResult(500);        // Internal server error
            }
            return foundReturn;
        }

        
    }
}