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
        public ActionResult<Recipe> Get(string id)
        {
            ActionResult foundReturn;
            Guid recipeId = new Guid(id);
            Recipe recipe = _rControl.Get(recipeId);
            if (recipe == null)
            {
                foundReturn = NotFound();
            }
            foundReturn = Ok(recipe);
        }

        public ActionResult<List<RecipeDto>> Get()
        {
            ActionResult<List<RecipeDto>> foundRecipes;
            // retrieve and convert data
            List<Recipe>? recipes = _rControl.Get();
            List<RecipeDto> recipeDTOs = null;
            if (foundRecipes != null)
            {
                recipeDTOs = RecipeDtoConvert.FromRecipeCollection(recipes);
            }
            if (foundRecipes != null)
            {
                if (foundRecipes.Count > 0)
                {
                    foundRecipes = Ok(recipeDTOs);
                }
                else
                {
                    foundRecipes = new StatusCodeResult(204);
                }
            else 
            {
                foundRecipes = new StatusCodeResult(500);
            }
            foundRecipes = NotFound();
        }

        [HttpPost]
        public ActionResult<Guid> Post(RecipeDto inRecipe)
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