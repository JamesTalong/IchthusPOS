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
    public class BatchStagingsHistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public BatchStagingsHistoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBatchStagingsHistory()
        {
            var history = await _dbContext.BatchStagingsHistory
                .Include(h => h.SerialStagingsHistory)
                .OrderByDescending(h => h.BatchDate)
                .ToListAsync();
            return Ok(history);
        }

        [HttpGet("short")]
        public async Task<IActionResult> GetAllShortBatchStagingsHistory()
        {
            var history = await _dbContext.BatchStagingsHistory
                .Include(h => h.SerialStagingsHistory)
                .OrderByDescending(h => h.BatchDate)
                .Select(h => new
                {
                    h.Id,
                    h.UserId,
                    h.UserName,
                    h.BatchDate,
                    h.NumberOfItems,
                    h.PricelistId,
                    ProductName = _dbContext.Pricelists
                        .Where(p => p.Id == h.PricelistId)
                        .Select(p => p.Product.ProductName)
                        .FirstOrDefault(),
                    Location = _dbContext.Pricelists
                        .Where(p => p.Id == h.PricelistId)
                        .Select(p => p.Location.LocationName)
                        .FirstOrDefault(),
                    Quantity = h.SerialStagingsHistory.Count
         
                })
                .ToListAsync();

            return Ok(history);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBatchStagingsHistoryById(int id)
        {
            var historyEntry = await _dbContext.BatchStagingsHistory
                .Include(h => h.SerialStagingsHistory)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (historyEntry == null)
            {
                return NotFound();
            }

            return Ok(historyEntry);
        }

        [HttpPost]
        public async Task<IActionResult> AddBatchStagingsHistory(BatchStaginghistory batchStagingsHistory)
        {
            _dbContext.BatchStagingsHistory.Add(batchStagingsHistory);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBatchStagingsHistoryById), new { id = batchStagingsHistory.Id }, batchStagingsHistory);
        }

        // DELETE: api/BatchStagingsHistory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatchStagingsHistoryById(int id)
        {
            var historyEntry = await _dbContext.BatchStagingsHistory
                .Include(h => h.SerialStagingsHistory)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (historyEntry == null)
            {
                return NotFound();
            }

            // First delete the related SerialStagingsHistory
            _dbContext.SerialStagingsHistory.RemoveRange(historyEntry.SerialStagingsHistory);

            // Now delete the BatchStagingsHistory
            _dbContext.BatchStagingsHistory.Remove(historyEntry);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/BatchStagingsHistory
        [HttpDelete]
        public async Task<IActionResult> DeleteAllBatchStagingsHistory()
        {
            var allEntries = await _dbContext.BatchStagingsHistory
                .Include(h => h.SerialStagingsHistory)
                .ToListAsync();

            if (!allEntries.Any())
            {
                return NotFound();
            }

            // First delete all SerialStagingsHistory
            var allSerials = allEntries.SelectMany(h => h.SerialStagingsHistory).ToList();
            _dbContext.SerialStagingsHistory.RemoveRange(allSerials);

            // Then delete all BatchStagingsHistory
            _dbContext.BatchStagingsHistory.RemoveRange(allEntries);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
