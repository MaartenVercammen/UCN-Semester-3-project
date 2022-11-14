using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeRestService.Businesslogic;
using RecipeRestService.DTO;
using RecipeRestService.ModelConversion;
using RecipesData.Model;
using System.ComponentModel.DataAnnotations;

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


        [HttpGet]
        public IActionResult Get()
        {
            return new StatusCodeResult(500);
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] RecipeDto inRecipe)
        {
            ActionResult foundReturn;
            Guid insertedGuid = Guid.Empty;
            if(inRecipe != null)
            {
                Recipe recipe = RecipeDtoConvert.ToRecipe(inRecipe);
                ValidationContext context = new ValidationContext(recipe);
                ICollection<ValidationResult> results = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(recipe, context, results);
                if (isValid)
                {
                    insertedGuid = _rControl.Add(recipe);
                }
                
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
