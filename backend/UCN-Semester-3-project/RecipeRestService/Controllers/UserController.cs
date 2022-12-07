using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeRestService.Businesslogic;
using RecipeRestService.DTO;
using RecipeRestService.ModelConversion;
using RecipeRestService.Security;
using RecipesData.Database;
using RecipesData.Model;
using UserRestService.Businesslogic;

namespace UserRestService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserData _rControl;

        private readonly ISecurityHelper _securityHelper;

        public UserController(IUserData userData, ISecurityHelper securityHelper)
        {
            _rControl = userData;
            _securityHelper = securityHelper;
        }

        [HttpGet, Route("{id}")]
        [Authorize(Roles = "USER,VERIFIEDUSER,ADMIN")]
        public ActionResult<UserDto> Get(string id)
        {
            Guid UserId = Guid.Parse(id);
            Role role = _securityHelper.GetRoleFromJWT(Request.Headers["Authorization"]);
            //check if user is user role all others are allowed to see the other users
            string token = Request.Headers["Authorization"];
            if(role == Role.USER){
                if(_securityHelper.IsJWTEqualRequestId(token, id)){
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
        public ActionResult<Guid> Post([FromBody] UserDto inUser)
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

        [HttpPut]
        [Authorize(Roles = "USER,VERIFIEDUSER,ADMIN")]
        public ActionResult<bool> Edit(UserDto inUser)
        {
            ActionResult foundReturn;
            bool updated = false;

            Role role = _securityHelper.GetRoleFromJWT(Request.Headers["Authorization"]);

            //check if user or verified user are theimselves
            string token = Request.Headers["Authorization"];
            if(role == Role.USER || role == Role.VERIFIEDUSER){
                if(_securityHelper.IsJWTEqualRequestId(token, inUser.UserId.ToString())){
                    return new StatusCodeResult(403);
                }
            }

            if (inUser != null)
            {
                var userId = inUser.UserId;
                updated = _rControl.Put(UserDtoConvert.ToUser(inUser));
            }
            if (updated)
            {
                foundReturn = Ok(updated);
            }
            else
            {
                foundReturn = new StatusCodeResult(500);        // Internal server error
            }
            return foundReturn;
        }

        [HttpDelete, Route("{id}")]
        [Authorize(Roles = "USER,VERIFIEDUSER,ADMIN")]
        public ActionResult<bool> Delete(string id)
        {
            Guid userId = Guid.Parse(id);

             Role role = _securityHelper.GetRoleFromJWT(Request.Headers["Authorization"]);            

            //check if user or verified user are theimselves
            string token = Request.Headers["Authorization"];
            if(role == Role.USER || role == Role.VERIFIEDUSER){
                if(_securityHelper.IsJWTEqualRequestId(token, id)){
                    return new StatusCodeResult(403);
                }
            }

            ActionResult foundReturn;
            bool IsCompleted = _rControl.Delete(userId);
            if (IsCompleted)
            {
                foundReturn = Ok(IsCompleted);
            }
            else
            {
                foundReturn = new StatusCodeResult(500);
            }
            return foundReturn;

        }
        
    }
}