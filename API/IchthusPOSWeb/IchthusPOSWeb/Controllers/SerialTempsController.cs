using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerialTempsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public SerialTempsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/SerialTemps
        [HttpGet]
        public IActionResult GetAllSerialTemps()
        {
            // Load SerialTemps first
            var serialTempsList = dbContext.SerialTemps.ToList();

            // Get all SerialNumbers in one query to avoid multiple DB calls
            var allSerialNumbers = dbContext.SerialNumbers.ToList();

            // Process data in memory
            var serialTemps = serialTempsList.Select(item => new
            {
                Id = item.Id,
                PricelistId = item.PricelistId,
                SerialNumbers = allSerialNumbers
                    .Where(serial => item.SerialNumbers.Contains(serial.Id)) // ✅ Now processed in memory
                    .ToList()
            }).ToList();

            return Ok(serialTemps);
        }

        [HttpPost]
        public IActionResult AddSerialTemp(AddSerialTempDto addSerialTempDto)
        {
            var serialTemp = new SerialTemp()
            {
                PricelistId = addSerialTempDto.PricelistId,
                SerialNumbers = addSerialTempDto.SerialNumbers
            };

            dbContext.SerialTemps.Add(serialTemp);
            dbContext.SaveChanges();
            return Ok(serialTemp);
        }

        // PUT: api/SerialTemps/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateSerialTemp(int id, UpdateSerialTempDto updateSerialTempDto)
        {
            var serialTemp = dbContext.SerialTemps.Find(id);

            if (serialTemp is null)
            {
                return NotFound();
            }

            serialTemp.PricelistId = updateSerialTempDto.PricelistId;
            serialTemp.SerialNumbers = updateSerialTempDto.SerialNumbers;

            dbContext.SaveChanges();
            return Ok(serialTemp);
        }

        // DELETE: api/SerialTemps/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteSerialTemp(int id)
        {
            var serialTemp = dbContext.SerialTemps.Find(id);

            if (serialTemp is null)
            {
                return NotFound();
            }

            dbContext.SerialTemps.Remove(serialTemp);
            dbContext.SaveChanges();
            return Ok(serialTemp);
        }
        // DELETE: api/SerialTemps/by-pricelist/{pricelistId}
        [HttpDelete("by-pricelist/{pricelistId}")]
        public IActionResult DeleteSerialTempsByPricelistId(int pricelistId)
        {
            var serialTemps = dbContext.SerialTemps.Where(st => st.PricelistId == pricelistId).ToList();

            // If no matching serialTemps are found, do nothing and return a 204 No Content response
            if (!serialTemps.Any())
            {
                return NoContent();
            }

            dbContext.SerialTemps.RemoveRange(serialTemps);
            dbContext.SaveChanges();

            return Ok(new { Message = $"{serialTemps.Count} SerialTemps deleted successfully", DeletedItems = serialTemps });
        }
        // DELETE: api/SerialTemps/delete-all
        [HttpDelete("delete-all")]
        public IActionResult DeleteAllSerialTemps()
        {
            var allSerialTemps = dbContext.SerialTemps.ToList();

            if (!allSerialTemps.Any())
            {
                return NoContent(); // No records to delete
            }

            // Save all SerialTemps into SerialMain before deletion
            var serialMains = allSerialTemps.Select(temp => new SerialMain
            {
                PricelistId = temp.PricelistId,
                SerialNumbers = temp.SerialNumbers
            }).ToList();

            dbContext.SerialMains.AddRange(serialMains); // Save to SerialMain
            dbContext.SerialTemps.RemoveRange(allSerialTemps); // Remove from SerialTemps
            dbContext.SaveChanges();

            return Ok(new { Message = $"{allSerialTemps.Count} SerialTemps moved to SerialMain and deleted successfully" });
        }


    }
}
