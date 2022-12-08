using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeRestService.Businesslogic;
using RecipeRestService.DTO;
using RecipeRestService.ModelConversion;
using RecipeRestService.Security;
using RecipesData.Model;

namespace RecipeRestService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class BambooSessionController : ControllerBase
    {
        private readonly IBambooSessionData _bControl;
        private readonly ISecurityHelper _securityHelper;

        private readonly IUserData _userData;

        private readonly IRecipeData _recipeData;


        public BambooSessionController(ISecurityHelper securityHelper, IBambooSessionData data, IUserData userData, IRecipeData recipeData)
        {
            _securityHelper = securityHelper;
            _bControl = data;
            _userData = userData;
            _recipeData = recipeData;
        }

        [HttpGet, Route("{id}")]
        [Authorize(Roles = "ADMIN,VERIFIEDUSER")]
        public ActionResult<BambooSessionDto> GetBambooSession(string id)
        {
            Guid bamboosessionId = Guid.Parse(id);
            ActionResult<BambooSessionDto> foundReturn;
            BambooSession? bambooSession = _bControl.Get(bamboosessionId);
            BambooSessionDto? bambooSessionDto;

            if (bambooSession != null)
            {
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
        [Authorize(Roles = "ADMIN,VERIFIEDUSER,USER")]
        public ActionResult<List<BambooSessionDto>> GetBambooSessions()
        {

            ActionResult<List<BambooSessionDto>> foundReturn;
            List<BambooSession>? bambooSessions = _bControl.Get();
            List<BambooSessionDto>? bambooSessionsDto;

            if (bambooSessions != null)
            {
                bambooSessionsDto = BambooSessionDtoConvert.FromBambooSessionCollection(bambooSessions);
                if (bambooSessions.Count > 0)
                {
                    foundReturn = Ok(bambooSessionsDto);
                }
                else
                {
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
        [Authorize(Roles = "ADMIN,VERIFIEDUSER")]
        public ActionResult<Guid> Post([FromBody] BambooSessionDto inBamboo)
        {
            // user id
            Guid insertedGuid = Guid.Empty;
            ActionResult foundReturn;
            Guid userId = _securityHelper.GetUserFromJWT(Request.Headers["Authorization"]);
            inBamboo.Host = userId;
            if (inBamboo != null)
            {
                User? host = _userData.Get(userId);
                Recipe? recipe = _recipeData.Get(inBamboo.Recipe);
                if (host != null && recipe != null)
                {
                    BambooSession? bambooSession = BambooSessionDtoConvert.ToBambooSession(inBamboo, host, recipe);
                    if (bambooSession != null)
                    {
                        insertedGuid = _bControl.Add(bambooSession);
                    }
                }
                else
                {
                    insertedGuid = Guid.Empty;
                }
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

        [HttpPut]
        [Authorize(Roles = "ADMIN,VERIFIEDUSER")]
        public ActionResult<bool> JoinBambooSession(string sessionId, string seatId)
        {
            ActionResult foundReturn;

            // session
            Guid sessionGuid = Guid.Parse(sessionId);
            BambooSession? session = _bControl.Get(sessionGuid);

            // seat
            Guid seatGuid = Guid.Parse(seatId);
            Seat? seat = _bControl.GetSeatBySessionAndSeatId(session, seatGuid);



            Guid userId = _securityHelper.GetUserFromJWT(Request.Headers["Authorization"]);
            User? user = _userData.Get(userId);

            bool IsDone = _bControl.Join(session, user, seat);


            foundReturn = Ok(IsDone);

            return foundReturn;
        }

        [HttpGet, Route("{session}/seats")]
        [Authorize(Roles = "ADMIN,VERIFIEDUSER")]
        public ActionResult<List<SeatDto>> GetSeatsBySessionId(string session)
        {
            ActionResult foundreturn;
            Guid sessionId = Guid.Parse(session);
            List<Seat>? seats = _bControl.GetSeatsBySessionId(sessionId);
            if (seats == null)
            {
                foundreturn = new StatusCodeResult(500);
            }
            else
            {
                if (seats.Count <= 0)
                {
                    foundreturn = NotFound();
                }
                else
                {
                    List<SeatDto>? seatDtos = SeatDtoConvert.FromSeatCollection(seats);
                    foundreturn = Ok(seatDtos);
                }
            }
            return foundreturn;
        }

        [Authorize(Roles = "ADMIN,VERIFIEDUSER")]
        [HttpDelete, Route("{id}")]
        public ActionResult<bool> Delete(string id)
        {
            ActionResult result;
            Guid sessionId = Guid.Parse(id);
            bool Isdone = _bControl.Delete(sessionId);
            if (Isdone)
            {
                result = Ok(Isdone);
            }
            else
            {
                result = NotFound();
            }

            return result;
        }

    }
}