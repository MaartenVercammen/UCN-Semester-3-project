using Microsoft.AspNetCore.Mvc;

namespace RecipeRestService.Controllers
{
    public class RecipesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
