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
