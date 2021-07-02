using EcommerceVT.Interface;
using EcommerceVT.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceVT.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IAuthenticate _authenticate;
        private IUser _user;
        private ILocation _location;
        public AuthenticateController(IAuthenticate authenticate,
            IUser user, ILocation location)
        {
            _authenticate = authenticate;
            _user = user;
            _location = location;
        }
        
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Authenticate authenticate)
        {
            var user = await _authenticate.Login(authenticate);

            if (user == null)
                return Unauthorized(new { error = "User or password invalid" });

            return Ok(new { token = _authenticate.CreateTokenJWT(user), user });
        }
    }
}
