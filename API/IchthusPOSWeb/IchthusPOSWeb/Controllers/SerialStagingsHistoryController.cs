using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerialStagingsHistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public SerialStagingsHistoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/SerialStagingsHistory
        [HttpGet]
        public async Task<IActionResult> GetAllSerialStagingsHistory()
        {
            var histories = await _dbContext.SerialStagingsHistory
       
                .ToListAsync();

            return Ok(histories);
        }

        // GET: api/SerialStagingsHistory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSerialStagingsHistoryById(int id)
        {
            var history = await _dbContext.SerialStagingsHistory
                
                .FirstOrDefaultAsync(h => h.Id == id);

            if (history == null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        // POST: api/SerialStagingsHistory
        [HttpPost]
        public async Task<IActionResult> AddSerialStagingsHistory(SerialStagingsHistory serialStaginghistory)
        {
            _dbContext.SerialStagingsHistory.Add(serialStaginghistory);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSerialStagingsHistoryById), new { id = serialStaginghistory.Id }, serialStaginghistory);
        }
    }
}
