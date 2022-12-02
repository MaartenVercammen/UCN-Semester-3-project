using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeRestService.Businesslogic;
using RecipeRestService.DTO;
using RecipeRestService.ModelConversion;
using RecipeRestService.Security;
using RecipesData.Database;
using RecipesData.Model;

namespace BambooSessionController.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class BambooSessionController: ControllerBase
    {
        private readonly BambooSessiondataControl _rControl;
        private readonly IConfiguration _configuration;

        public BambooSessionController(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
            BambooSessionDatabaseAccess access = new BambooSessionDatabaseAccess(inConfiguration);
            _rControl = new BambooSessiondataControl(access);
        }
        
    }
}