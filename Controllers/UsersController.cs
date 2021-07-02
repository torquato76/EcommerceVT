using EcommerceVT.Data;
using EcommerceVT.Interface;
using EcommerceVT.Model;
using EcommerceVT.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceVT.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUser _user;
        private readonly ILocation _location;
        public UsersController(AppDbContext context, IUser user, ILocation location)
        {
            _context = context;
            _user = user;
            _location = location;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _user.Get(id);
            if (user == null)
                return NotFound(new { error = $"User {id} not found" });

            user.Location = await _location.Get(user.Location_Id);
            return Ok(user);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (user == null)
                return BadRequest(new { error = "Object is null" });

            using (var transaction = _context.Database.BeginTransaction())
            {
                await _location.Post(user.Location);

                user.Location_Id = user.Location.Location_Id;
                user.Password = new CryptoString().EncryptText(user.Password);

                await _user.Post(user);
            }

            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            if (id != user.User_Id)
                return BadRequest(new { error = $"User code {id} is not the same as object" });

            using (var transaction = _context.Database.BeginTransaction())
            {
                await _location.Put(user.Location);

                user.Password = new CryptoString().EncryptText(user.Password);
                await _user.Put(user);

                transaction.Commit();
            }

            return Ok(user);
        }
    }
}
