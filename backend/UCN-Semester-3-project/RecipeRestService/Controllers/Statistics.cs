using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipesData.Database;

namespace RecipeRestService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Statistics : ControllerBase
    {
        private readonly CountDataBaseAcces _dataBaseAcces;
        public Statistics(CountDataBaseAcces dataBaseAcces)
        {
            _dataBaseAcces = dataBaseAcces;
        }

        [HttpGet]
        public ActionResult<int> GetCount()
        {
            return _dataBaseAcces.GetCount();
        }
    }
}
