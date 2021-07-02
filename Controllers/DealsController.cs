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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DealsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDeal _deal;
        private readonly ILocation _location;
        public static IHostEnvironment _environment;
        public static IFile _file;

        public DealsController(AppDbContext context, IDeal deal, 
            ILocation location, IHostEnvironment environment, IFile file)
        {
            _context = context;
            _deal = deal;
            _location = location;
            _environment = environment;
            _file = file;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var deal = await _deal.Get(id);
            if (deal == null)
                return NotFound(new { error = $"Deal code {id} not found" });

            var photos = await _file.GetFiles("deal", deal.Deal_Id.ToString());

            return Ok(new { deal, photos });
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromForm] DealPost deal)
        {
            if (deal == null)
                return NotFound(new { error = "object is null" });

            if (deal.Photos == null || deal.Photos.Count == 0)
                return BadRequest(new { error = "file not selected" });

            using (var transaction = _context.Database.BeginTransaction())
            {
                await _location.Put(deal.Location);
                deal.Location_Id = deal.Location.Location_Id;

                await _deal.Post(deal);

                await _file.SaveFiles(deal.Photos, "deal", deal.Deal_Id.ToString());

                transaction.Commit();
            }

            return Ok(deal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] Deal deal)
        {
            if (id != deal.Deal_Id)
                return BadRequest(new { error = $"User code {id} is not the same as object" });

            using (var transaction = _context.Database.BeginTransaction())
            {
                await _location.Put(deal.Location);

                await _deal.Put(deal);

                _file.RemoveFilesFolder("deal", deal.Deal_Id.ToString());

                var photos = await _file.GetFiles("deal", deal.Deal_Id.ToString());
                
                transaction.Commit();
            }
            return Ok(deal);
        }
    }
}
