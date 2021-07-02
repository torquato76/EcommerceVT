using EcommerceVT.Data;
using EcommerceVT.Interface;
using EcommerceVT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace EcommerceVT.Controllers
{

    [Authorize]
    [Route("api/v1/deals/{id}/[controller]")]
    [ApiController]
    public class DeliveriesController : Controller
    {
        private readonly IDeal _deal;
        private readonly IUser _user;
        private readonly IShipping _shipping;
        private readonly ILocation _location;

        public DeliveriesController(IDeal deal,
            IShipping shipping, IUser user, ILocation location)
        {
            _deal = deal;
            _shipping = shipping;
            _user = user;
            _location = location;
        }

        [HttpPost()]
        public async Task<IActionResult> Get(int id, [FromBody] Deliverie deliverie)
        {
            var deal = await _deal.Get(id);
            if (deal == null)
                return NotFound(new { error = $"Deal code {id} not found" });

            var user = await _user.Get(deliverie.User_Id);

            var userLocation = await _location.Get(user.Location_Id);
            var dealLocation = await _location.Get(deal.Location_Id);

            var frete = await _shipping.Get(dealLocation.Zip_Code, userLocation.Zip_Code);

            return Ok(frete);
        }

    }
}
