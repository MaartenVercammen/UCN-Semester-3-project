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
        public ActionResult<Recipe> Get(Guid id)
        {
            ActionResult foundReturn;
            Recipe recipe = _rControl.Get(id);
            if (recipe == null)
            {
                foundReturn = NotFound();
            }
            else
            {
                foundReturn = Ok(recipe);
            }
        }

        public ActionResult<List<RecipeDTO>> Get()
        {
            ActionResult<List<RecipeDTO>> foundRecipes;
            // retrieve and convert data
            List<Recipe>? recipes = _rControl.Get();
            List<RecipeDTO> recipeDTOs = null;
            if (foundRecipes != null)
            {
                recipeDTOs = ModelConverter.ConvertToDTO(recipes);
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