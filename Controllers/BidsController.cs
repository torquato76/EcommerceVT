using EcommerceVT.Data;
using EcommerceVT.Interface;
using EcommerceVT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceVT.Controllers
{
    [Route("api/v1/deals/{id}/[controller]")]
    [ApiController]
    public class BidsController : Controller
    {
        private readonly IBid _bid;
        private readonly IDeal _deal;
        private readonly IAuthenticate _authenticate;


        public BidsController(IAuthenticate authenticate, IBid bid, IDeal deal)
        {
            _authenticate = authenticate;
            _bid = bid;
            _deal = deal;
        }

        [HttpGet("{bidId}")]
        public async Task<IActionResult> Get(int id, int bidId)
        {
            var deal = await _deal.Get(id);
            if (deal == null)
                return BadRequest(new { error = $"Deal {id} not found" });

            var bid = await _bid.Get(bidId);
            if (bid == null)
                return BadRequest(new { error = $"Bid {bidId} not found" });

            return Ok(bid);
        }

        [HttpGet()]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
                return BadRequest(new { error = $"Deal id {id} invalid" });

            return Ok(await _bid.GetAll(id));
        }

        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> Post(int id, [FromBody] Bid bid)
        {
            if (bid == null)
                return BadRequest(new { error = "object is null" });

            var deal = await _deal.Get(id);
            if (deal == null)
                return BadRequest(new { error = $"Deal {id} not found" });

            bid.Deal_Id = id;
            await _bid.Post(bid);
            return Ok(bid);
        }

        [Authorize]
        [HttpPut("{bidId}")]
        public async Task<IActionResult> Put(int id, int bidId, [FromBody] Bid bid)
        {
            if (bid == null)
                return BadRequest(new { error = "object is null" });

            var deal = await _deal.Get(id);

            if (deal == null)
                return BadRequest(new { error = $"Deal {id} not found" });

            bid.Deal_Id = id;
            bid.Bid_Id = bidId;
            await _bid.Put(bid);
            return Ok(bid);
        }
    }
}