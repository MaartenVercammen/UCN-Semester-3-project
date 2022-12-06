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
        private readonly IBambooSessionData _bControl;
        private readonly ISecurityHelper _securityHelper;


        public BambooSessionController(ISecurityHelper securityHelper, IBambooSessionData data)
        {
            _securityHelper = securityHelper;
            _bControl = data;
        }

        [HttpGet, Route("{id}")]
        [Authorize(Roles = "ADMIN,VERIFIED")]
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
        [Authorize(Roles = "ADMIN,VERIFIED")]
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
        [AllowAnonymous] //TODO: Change [AllowAnonymus] to [Authorize(Roles = "ADMIN,VERIFIED" )] once frontend is implemented
        public ActionResult Post([FromBody] BambooSessionDto inBamboo)
        {
            ActionResult foundReturn;
            Guid insertedGuid = Guid.Empty;

            if (inBamboo != null)
            {
                BambooSession? bambooSession = BambooSessionDtoConvert.ToBambooSession(inBamboo);
                if(bambooSession != null){
                    insertedGuid = _bControl.Add(bambooSession);
                }
                else{
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

        [HttpPost, Route("{session}/{seat}")]
        [Authorize(Roles = "ADMIN,VERIFIED")]
        public ActionResult<bool> JoinBambooSession(string session, string seat)
        {
            ActionResult foundReturn;
            Guid sessionId = Guid.Parse(session);
            Guid seatId = Guid.Parse(seat);

            Guid userId = _securityHelper.GetUserFromJWT(Request.Headers["Authorization"]);

            bool IsDone = _bControl.Join(sessionId, userId, seatId);

            foundReturn = Ok(IsDone);

            return foundReturn;
        }

        [HttpPost, Route("{session}")]
        [Authorize(Roles = "ADMIN,VERIFIED")]
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
                    if(seatDtos != null){
                    foundreturn = Ok(seatDtos);
                    }
                    else{
                        foundreturn = new StatusCodeResult(500);
                    }
                }
            }
            return foundreturn;
        }

        [Authorize(Roles = "ADMIN,VERIFIED")]
        [HttpDelete, Route("{id}")]
        public ActionResult<bool> Delete(string id){
            ActionResult result;
            Guid sessionId = Guid.Parse(id);
            bool Isdone = _bControl.Delete(sessionId);
            if(Isdone){
                result = Ok(Isdone);
            }else{
                result = NotFound();
            }

            return result;
        }

    }
}