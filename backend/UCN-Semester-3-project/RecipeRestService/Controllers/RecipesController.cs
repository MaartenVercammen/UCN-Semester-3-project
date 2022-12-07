using System.IdentityModel.Tokens.Jwt;
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
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeData _rControl;
        private readonly IUserData _uControl;

        private readonly ISecurityHelper _securityHelper;

        public RecipesController(IUserData userdata, IRecipeData recipeData, ISecurityHelper securityHelper)
        {
            _rControl = recipeData;
            _uControl = userdata;
            _securityHelper = securityHelper;
        }

        [Authorize(Roles = "ADMIN,VERIFIED,USER")]
        [HttpGet, Route("{id}")]
        public ActionResult<RecipeDto> Get(string id)
        {
            Guid recipeId = Guid.Parse(id);

            ActionResult<RecipeDto> foundReturn;
            Recipe? foundRecipe = _rControl.Get(recipeId);
            if (foundRecipe != null)
            {
                foundReturn = Ok(RecipeDtoConvert.FromRecipe(foundRecipe));
            }
            else
            {
                foundReturn = NotFound();
            }
            return foundReturn;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,VERIFIED,USER")]
        public ActionResult<List<RecipeDto>> Get()
        {
            ActionResult<List<RecipeDto>> foundReturn;
            // retrieve and convert data
            List<Recipe>? foundRecipes = _rControl.Get();
            List<RecipeDto>? foundDts = null;
            if (foundRecipes != null)
            {
                foundDts = RecipeDtoConvert.FromRecipeCollection(foundRecipes);
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

        [Authorize(Roles = "ADMIN,VERIFIED,USER")]
        [HttpGet, Route("user/{userId}/liked")] //liked/{userId}
        public ActionResult<List<RecipeDto>> GetLiked(string userId)
        {
            if (_securityHelper.IsJWTEqualRequestId(Request.Headers["Authorization"], userId))
            {
                return new StatusCodeResult(403);
            }

            Guid userIdGuid = Guid.Parse(userId);
            ActionResult<List<RecipeDto>> foundReturn;
            // retrieve and convert data
            List<Recipe>? foundRecipes = _rControl.GetLikedByUser(userIdGuid);
            List<RecipeDto>? foundDts = null;
            if (foundRecipes != null)
            {
                foundDts = RecipeDtoConvert.FromRecipeCollection(foundRecipes);
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
        [Authorize(Roles = "ADMIN,VERIFIED")]
        public ActionResult<string> Post([FromBody] RecipeDto inRecipe)
        {
            Guid userid = _securityHelper.GetUserFromJWT(Request.Headers["Authorization"]);
            inRecipe.Author = userid;
            ActionResult foundReturn;
            Guid insertedGuid = Guid.Empty;
            if (inRecipe != null && (userid != Guid.Empty || userid != null))
            {
                User? author = _uControl.Get(inRecipe.Author);
                if (author != null)
                {
                    Recipe? recipe = RecipeDtoConvert.ToRecipe(inRecipe, author);
                    if (recipe != null)
                    {
                        insertedGuid = _rControl.Add(recipe);
                    }
                }
            }
            if (insertedGuid != Guid.Empty)
            {
                foundReturn = Ok(insertedGuid);
            }
            else
            {
                foundReturn = new StatusCodeResult(500);
            }
            return foundReturn;
        }

        [HttpDelete, Route("{id}")]
        [Authorize(Roles = "ADMIN,VERIFIED")]
        public ActionResult Delete(string id)
        {

            ActionResult foundReturn;
            Guid recipeId = Guid.Parse(id);

            Recipe? recipe = _rControl.Get(recipeId);
            

                if (_securityHelper.IsJWTEqualRequestId(Request.Headers["Authorization"], recipe.Author.UserId.ToString()))
                {
                    return new StatusCodeResult(403);
                }

                bool IsCompleted = _rControl.Delete(recipeId);
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

        [Authorize(Roles = "ADMIN,VERIFIED,USER")]
        [HttpGet, Route("/Random")]
        public ActionResult<RecipeDto> GetRandomRecipe()
        {
            ActionResult foundReturn;
            Guid userId = _securityHelper.GetUserFromJWT(Request.Headers["Authorization"]);
            Recipe? recipe = _rControl.GetRandomRecipe(userId);
            if (recipe != null)
            {
                foundReturn = Ok(RecipeDtoConvert.FromRecipe(recipe));
            }
            else
            {
                foundReturn = new StatusCodeResult(204);
            }
            return foundReturn;
        }

    }
}