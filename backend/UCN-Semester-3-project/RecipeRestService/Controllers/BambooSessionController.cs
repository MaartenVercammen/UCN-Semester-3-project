using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeRestService.Businesslogic;
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
        private readonly UserDataControl _uControl;
        private readonly RecipedataControl _rControl;
        private readonly IConfiguration _configuration;


        public BambooSessionController(IConfiguration inConfiguration, IBambooSessionData data)
        {
            _configuration = inConfiguration;
            _bControl = data;
            UserDatabaseAccess uAccess = new UserDatabaseAccess(inConfiguration);
            _uControl = new UserDataControl(uAccess);
            RecipeDatabaseAccess rAccess = new RecipeDatabaseAccess(inConfiguration);
            _rControl = new RecipedataControl(rAccess);
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
                foundReturn = Ok(bambooSessionDto);
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

        [HttpPost]
         [AllowAnonymous] //TODO: Change [AllowAnonymus] to [Authorize(Roles = "ADMIN,VERIFIED" )] once frontend is implemented
         public ActionResult Post([FromBody] BambooSessionDto inBamboo)
         {
            // user id
            Guid userId = new SecurityHelper(_configuration).GetUserFromJWT(Request.Headers["Authorization"]);
            inBamboo.Host = userId;

            // recipe id
            Guid recipeId = inBamboo.Recipe;

             ActionResult foundReturn;
             Guid insertedGuid = Guid.Empty;

             if (inBamboo != null && (userId != Guid.Empty || userId != null) && (recipeId != Guid.Empty || recipeId != null))
             {
                User host = _uControl.Get(userId);
                Recipe recipe = _rControl.Get(recipeId);
                 insertedGuid = _bControl.Add(BambooSessionDtoConvert.ToBambooSession(inBamboo, host, recipe));
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