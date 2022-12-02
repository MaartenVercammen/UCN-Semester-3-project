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
    public class BambooSessionController : ControllerBase
    {
        private readonly BambooSessionDataControl _rControl;
        private readonly IConfiguration _configuration;

        public BambooSessionController(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
            BambooSessionDatabaseAccess access = new BambooSessionDatabaseAccess(inConfiguration);
            _rControl = new BambooSessionDataControl(access);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Post([FromBody] BambooSessionDto inBamboo)
        {
            ActionResult foundReturn;
            Guid insertedGuid = Guid.Empty;

            if (inBamboo != null)
            {
                insertedGuid = _rControl.Add(BambooSessionDtoConvert.ToBambooSession(inBamboo));
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

    }
}