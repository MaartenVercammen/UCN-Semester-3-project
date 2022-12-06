using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeRestService.Businesslogic;
using RecipeRestService.DTO;
using RecipeRestService.ModelConversion;
using RecipeRestService.Security;
using RecipesData.Database;
using RecipesData.Model;

namespace UserRestService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserDataControl _rControl;
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
            UserDatabaseAccess access = new UserDatabaseAccess(inConfiguration);
            _rControl = new UserDataControl(access);
        }

        [HttpGet, Route("{id}")]
        [Authorize(Roles = "USER,VERIFIEDUSER,ADMIN")]
        public ActionResult<UserDto> Get(string id)
        {
            Guid UserId = Guid.Parse(id);
            Role role = new SecurityHelper(_configuration).GetRoleFromJWT(Request.Headers["Authorization"]);
            //check if user is user role all others are allowed to see the other users
            if(role == Role.USER){
                if(new SecurityHelper(_configuration).IsJWTEqualRequestId(Request, id)){
                    return new StatusCodeResult(403);
                }
            }

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
        [Authorize(Roles = "ADMIN")]
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
        [AllowAnonymous]
        public ActionResult Post([FromBody] UserDto inUser)
        {
            ActionResult foundReturn;
            Guid insertedGuid = Guid.Empty;
            if (inUser != null)
            {
                User? user = UserDtoConvert.ToUser(inUser);
                if(user != null){
                insertedGuid = _rControl.Add(user);
                }else{
                    foundReturn = new StatusCodeResult(500);
                }
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

        [HttpPut]
        [Authorize(Roles = "USER,VERIFIEDUSER,ADMIN")]
        public ActionResult Edit(UserDto inUser)
        {
            ActionResult foundReturn;
            bool updated = false;

            Role role = new SecurityHelper(_configuration).GetRoleFromJWT(Request.Headers["Authorization"]);

            //check if user or verified user are theimselves
            if(role == Role.USER || role == Role.VERIFIEDUSER){
                if(new SecurityHelper(_configuration).IsJWTEqualRequestId(Request, inUser.UserId.ToString())){
                    return new StatusCodeResult(403);
                }
            }

            if (inUser != null)
            {
                User? user = UserDtoConvert.ToUser(inUser);
                if(user !=  null){
                    updated = _rControl.Put(user);
                }else{
                    updated = false;
                }
            }
            if (updated)
            {
                foundReturn = Ok();
            }
            else
            {
                foundReturn = new StatusCodeResult(500);        // Internal server error
            }
            return foundReturn;
        }

        [HttpDelete, Route("{id}")]
        [Authorize(Roles = "USER,VERIFIEDUSER,ADMIN")]
        public ActionResult Delete(string id)
        {
            Guid userId = Guid.Parse(id);

             Role role = new SecurityHelper(_configuration).GetRoleFromJWT(Request.Headers["Authorization"]);            

            //check if user or verified user are theimselves
            if(role == Role.USER || role == Role.VERIFIEDUSER){
                if(new SecurityHelper(_configuration).IsJWTEqualRequestId(Request, id)){
                    return new StatusCodeResult(403);
                }
            }

            ActionResult foundReturn;
            bool IsCompleted = _rControl.Delete(userId);
            if (IsCompleted)
            {
                foundReturn = new StatusCodeResult(200);
            }
            else
            {
                foundReturn = new StatusCodeResult(500);
            }
            return foundReturn;

        }
        
    }
}