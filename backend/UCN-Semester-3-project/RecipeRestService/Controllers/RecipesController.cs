using Microsoft.AspNetCore.Mvc;
using RecipeRestService.Businesslogic;

namespace RecipeRestService.Controllers
{
    public class RecipesController : ControllerBase
    {
        private readonly RecipedataControl _rControl;
        private readonly IConfiguration _configuration;

        [HttpGet]
        public IActionResult Get()
        {
            return new StatusCodeResult(500);
        }
    }
}
