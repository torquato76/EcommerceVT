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
    public class MessagesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMessage _message;
        private readonly IDeal _deal;

        public MessagesController(AppDbContext context, IMessage message, IDeal deal)
        {
            _context = context;
            _message = message;
            _deal = deal;
        }

        [HttpGet("{messageId}")]
        public async Task<IActionResult> Get(int id, int messageId)
        {
            var deal = await _deal.Get(id);
            if (deal == null)
                return BadRequest(new { error = $"Message {id} not found" });

            var message = await _message.Get(messageId);
            if (message == null)
                return BadRequest(new { error = $"Message {messageId} not found" });

            return Ok(message);
        }

        [HttpGet()]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
                return BadRequest(new { error = $"Deal id {id} invalid" });

            return Ok(await _message.GetAll(id));
        }

        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> Post(int id, [FromBody] Messages message)
        {
            if (message == null)
                return BadRequest(new { error = "object is null" });

            var deal = await _deal.Get(id);
            if (deal == null)
                return BadRequest(new { error = $"Deal {id} not found" });

            message.Deal_Id = id;
            await _message.Post(message);
            return Ok(message);
        }

        [Authorize]
        [HttpPut("{messageId}")]
        public async Task<IActionResult> Put(int id, int messageId, [FromBody] Messages message)
        {
            if (message == null)
                return BadRequest(new { error = "object is null" });

            var deal = await _deal.Get(id);

            if (deal == null)
                return BadRequest(new { error = $"Deal {id} not found" });

            message.Deal_Id = id;
            message.Message_Id = messageId;
            await _message.Put(message);
            return Ok(message);
        }
    }
}