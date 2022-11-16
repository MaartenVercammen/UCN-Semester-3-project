using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeRestService.Businesslogic;
using RecipeRestService.DTO;
using RecipeRestService.ModelConversion;
using RecipesData.Model;

namespace RecipeRestService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly RecipedataControl _rControl;
        private readonly IConfiguration _configuration;

        public RecipesController(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
            _rControl = new RecipedataControl(_configuration);
        }

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

        [HttpPost]
        public ActionResult<string> Post([FromBody] RecipeDto inRecipe)
        {
            ActionResult foundReturn;
            Guid insertedGuid = Guid.Empty;
            if(inRecipe != null)
            {
                insertedGuid = _rControl.Add(RecipeDtoConvert.ToRecipe(inRecipe));
            }
            if(insertedGuid!= Guid.Empty)
            {
                foundReturn = Ok(insertedGuid);
            }
            else
            {
                foundReturn = new StatusCodeResult(500);
            }
            return foundReturn;
        }
    }
}