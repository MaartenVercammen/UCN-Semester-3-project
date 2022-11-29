using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeRestService.Businesslogic;
using RecipeRestService.DTO;
using RecipeRestService.ModelConversion;
using RecipeRestService.Security;
using RecipesData.Database;
using RecipesData.Model;

namespace RecipeRestService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class SwipedRecipeController : ControllerBase
    {
        private readonly SwipedRecipeDataControl _swControl;
        private readonly IConfiguration _configuration;

        public SwipedRecipeController(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
            SwipedRecipeDatabaseAccess access = new SwipedRecipeDatabaseAccess(inConfiguration);
            _swControl = new SwipedRecipeDataControl(access);
        }

        [Authorize(Roles = "ADMIN,VERIFIED,USER")]
        [HttpGet, Route("{id}")]
        public ActionResult<SwipedRecipeDto> Get(string id)
        {
            Guid swRecipeId = Guid.Parse(id);
            Guid userId = new SecurityHelper(_configuration).GetUserFromJWT(Request.Headers["Authorization"]);

            ActionResult<SwipedRecipeDto> foundReturn;
            SwipedRecipe? foundSwipedRecipe = _swControl.Get(swRecipeId, userId);

            if (foundSwipedRecipe != null)
            {
                foundReturn = Ok(SwipedRecipeDtoConvert.FromSwipedRecipe(foundSwipedRecipe));
            }
            else
            {
                foundReturn = NotFound();
            }

            return foundReturn;
        }

        [HttpGet, Route("user/{id}")]
        [Authorize(Roles = "ADMIN,VERIFIED,USER")]
        public ActionResult<List<SwipedRecipeDto>> GetPerUser(string id)
        {
            Guid userId = Guid.Parse(id);

            if(new SecurityHelper(_configuration).IsJWTEqualRequestId(Request, id)){
                return new StatusCodeResult(403);
            }

            ActionResult<List<SwipedRecipeDto>> foundReturn;
            // retrieve and convert data
            List<SwipedRecipe>? foundRecipes = _swControl.GetPerUser(userId);
            List<SwipedRecipeDto>? foundDts = null;
            if (foundRecipes != null)
            {
                foundDts = SwipedRecipeDtoConvert.FromSwipedRecipeCollection(foundRecipes);
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

        [HttpGet, Route("user/{id}/liked")]
        [Authorize(Roles = "ADMIN,VERIFIED,USER")]
        public ActionResult<List<SwipedRecipeDto>> GetLikedPerUser(string id)
        {
            System.Console.WriteLine("Here");
            Guid userId = Guid.Parse(id);

            if(new SecurityHelper(_configuration).IsJWTEqualRequestId(Request, id)){
                return new StatusCodeResult(403);
            }

            ActionResult<List<SwipedRecipeDto>> foundReturn;
            // retrieve and convert data
            List<SwipedRecipe>? foundRecipes = _swControl.GetLikedPerUser(userId);
            List<SwipedRecipeDto>? foundDts = null;
            if (foundRecipes != null)
            {
                foundDts = SwipedRecipeDtoConvert.FromSwipedRecipeCollection(foundRecipes);
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
        [Authorize(Roles = "ADMIN,VERIFIED,USER")]
        public ActionResult<SwipedRecipeDto> Post(SwipedRecipeDto inSwipedRecipeDto)
        {
            ActionResult<SwipedRecipeDto> foundReturn;

            Guid userId = new SecurityHelper(_configuration).GetUserFromJWT(Request.Headers["Authorization"]);
            inSwipedRecipeDto.UserId = userId;

            SwipedRecipe? foundSwipedRecipe;
            if (inSwipedRecipeDto != null)
            {
                foundSwipedRecipe = _swControl.Add(SwipedRecipeDtoConvert.ToSWRecipe(inSwipedRecipeDto));
                foundReturn= Ok(foundSwipedRecipe); 
            }
            else
            {
                foundReturn = new StatusCodeResult(500);
            }
            return foundReturn;
        }

    }
}