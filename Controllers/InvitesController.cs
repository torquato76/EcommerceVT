using EcommerceVT.Data;
using EcommerceVT.Interface;
using EcommerceVT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceVT.Controllers
{
    [Authorize]
    [Route("api/v1/users/{id}/[controller]")]
    [ApiController]
    public class InvitesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IInvite _invite;
        private readonly IUser _user;
        private readonly IAuthenticate _authenticate;

        public InvitesController(AppDbContext context, IInvite invite,
            IUser user,IAuthenticate authenticate)
        {
            _context = context;
            _invite = invite;
            _user = user;
            _authenticate = authenticate;
        }

        [HttpGet("{inviteId}")]
        public async Task<IActionResult> Get(int id, int inviteId)
        {
            if (int.Parse(_authenticate.GetClaimValue("UserId")) != id)
                return BadRequest(new { error = $"User {inviteId} not is valid authenticate" });

            var invite = await _invite.Get(inviteId);
            if (invite == null)
                return BadRequest(new { error = $"Invite {inviteId} not found" });

            return Ok(invite);
        }

        [HttpGet()]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
                return BadRequest(new { error = $"Invite id {id} invalid" });

            if (int.Parse(_authenticate.GetClaimValue("UserId")) != id)
                return BadRequest(new { error = $"User {id} not is valid authenticate" });

            return Ok(await _invite.GetAll(id));
        }

        [HttpPost()]
        public async Task<IActionResult> Post(int id, [FromBody] InvitePost invite)
        {
            if (invite == null)
                return BadRequest(new { error = "object is null" });

            if (int.Parse(_authenticate.GetClaimValue("UserId")) != id)
                return BadRequest(new { error = $"User {id} not is valid authenticate" });


            using (var transaction = _context.Database.BeginTransaction())
            {
                var userInvited = await _user.GetByEmail(invite.Email);

                /*add user invited also not exists*/
                if (userInvited == null)
                {
                    userInvited = await _user.Post(new User { Email = invite.Email, Name = invite.Name });
                    invite.User_Invited = userInvited.User_Id;
                }
                else
                    invite.User_Invited = userInvited.User_Id;

                invite.User_Id = id;
                await _invite.Post(invite);

                transaction.Commit();
            }

            return Ok(invite);
        }

        [HttpPut("{inviteId}")]
        public async Task<IActionResult> Put(int id, int inviteId, [FromBody] InvitePost invite)
        {
            if (invite == null)
                return BadRequest(new { error = "object is null" });

            if (int.Parse(_authenticate.GetClaimValue("UserId")) != id)
                return BadRequest(new { error = $"User {id} not is valid authenticate" });

            if (await _invite.Get(inviteId) == null)
                return BadRequest(new { error = $"Invite {id} not found" });

            using (var transaction = _context.Database.BeginTransaction())
            {
                var userInvited = await _user.Get(invite.User_Invited);

                /*add user invited also not exists*/
                if (userInvited != null)
                {
                    userInvited.Name = invite.Name;
                    userInvited.Email = invite.Email;

                   await _user.Put(userInvited);
                }
                else
                    return BadRequest(new { error = $"User Invite {inviteId} not found" });

                invite.User_Id = id;
                await _invite.Put(invite);

                transaction.Commit();
            }

            invite.User_Id = id;
            invite.Invite_Id = inviteId;
            await _invite.Put(invite);

            return Ok(invite);
        }
    }
}