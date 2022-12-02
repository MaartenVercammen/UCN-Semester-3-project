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
        private readonly IBambooSessionData _bControl;
        private readonly IConfiguration _configuration;


        public BambooSessionController(IConfiguration inConfiguration, IBambooSessionData data)
        {
            _configuration = inConfiguration;
            _bControl = data;
        }

        [HttpGet, Route("{id}")]
        [Authorize(Roles = "ADMIN,VERIFIED" )]
        public ActionResult<BambooSessionDto> GetBambooSession(string id)
        {
            Guid bamboosessionId = Guid.Parse(id);
            ActionResult<BambooSessionDto> foundReturn;
            BambooSession bambooSession = _bControl.Get(bamboosessionId);
            BambooSessionDto bambooSessionDto;

            if(bambooSession != null){
                bambooSessionDto = BambooSessionDtoConvert.FromBambooSession(bambooSession);
                foundReturn = Ok(bambooSession);
            }
             else
            {
                foundReturn = NotFound();   
            }
            
            return foundReturn;
        }

        
        [HttpGet]
        [Authorize(Roles = "ADMIN,VERIFIED" )]
        public ActionResult<List<BambooSessionDto>> GetBambooSessions()
        {
            
            ActionResult<List<BambooSessionDto>> foundReturn;
            List<BambooSession> bambooSessions = _bControl.Get();
            List<BambooSessionDto> bambooSessionsDto;

            if(bambooSessions != null){
                bambooSessionsDto = BambooSessionDtoConvert.FromBambooSessionCollection(bambooSessions);
                if(bambooSessions.Count > 0){
                    foundReturn = Ok(bambooSessionsDto);
                }   
                else{
                    foundReturn = NoContent();
                }
            }
             else
            {
                foundReturn = new StatusCodeResult(500);   
            }
            
            return foundReturn;
        }

        


        
    }
}