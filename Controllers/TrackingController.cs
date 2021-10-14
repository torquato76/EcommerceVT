using EcommerceVT.Data;
using EcommerceVT.Interface;
using EcommerceVT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceVT.Controllers
{

    [Authorize]
    [Route("api/v1/deals/{id}/[controller]")]
    [ApiController]
    public class TrackingController : Controller
    {
        private readonly IDeal _deal;
        private readonly IUser _user;
        private readonly IShipping _shipping;
        private readonly ILocation _location;

        public TrackingController(IDeal deal,
            IShipping shipping, IUser user, ILocation location)
        {
            _deal = deal;
            _shipping = shipping;
            _user = user;
            _location = location;
        }

        [HttpPost()]
        public async Task<IActionResult> Get()
        {
            var frete = await _shipping.Tracking("QF677080505BR", "9999999999", "S@1234YWC5");

            return Ok(frete);
        }

    }
}
