using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerialStagingsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public SerialStagingsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllSerialStagings()
        {
            var serialStagings = dbContext.SerialStagings
                .Select(s => new
                {
                    Id = s.Id,
                    SerialName = s.SerialName,
                    IsSold = s.IsSold,
                    BatchStagingId = s.BatchStagingId,
                    PricelistProduct = dbContext.Pricelists
                        .Where(p => p.Id == dbContext.BatchStagings
                            .Where(b => b.Id == s.BatchStagingId)
                            .Select(b => b.PricelistId)
                            .FirstOrDefault())
                        .Select(p => p.Product == null ? null : new
                        {
                            p.Product.Id,
                            p.Product.ProductName,
                            p.Product.Description
                        })
                        .FirstOrDefault()
                })
                .ToList();

            return Ok(serialStagings);
        }
        [HttpGet("{id}")]
        public IActionResult GetSerialStagingById(int id)
        {
            var serialStaging = dbContext.SerialStagings
                .Select(s => new
                {
                    Id = s.Id,
                    SerialName = s.SerialName,
                    IsSold = s.IsSold,
                    BatchStagingId = s.BatchStagingId
                })
                .FirstOrDefault(s => s.Id == id);

            if (serialStaging == null)
            {
                return NotFound(new { message = "Serial staging not found" });
            }

            return Ok(serialStaging);
        }


        [HttpPost]
        public IActionResult AddSerialStaging(AddSerialStagingDto addSerialStagingDto)
        {
            var serialStagingEntity = new SerialStaging
            {
                SerialName = addSerialStagingDto.SerialName,
                IsSold = addSerialStagingDto.IsSold,
            };

            dbContext.SerialStagings.Add(serialStagingEntity);
            dbContext.SaveChanges();

            return Ok(serialStagingEntity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSerialStaging(int id, UpdateSerialStagingDto updateSerialStagingDto)
        {
            var serialStaging = dbContext.SerialStagings.Find(id);

            if (serialStaging == null)
            {
                return NotFound();
            }

            serialStaging.SerialName = updateSerialStagingDto.SerialName;
            serialStaging.IsSold = updateSerialStagingDto.IsSold;

            dbContext.SaveChanges();

            return Ok(serialStaging);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSerialStaging(int id)
        {
            var serialStaging = dbContext.SerialStagings.Find(id);

            if (serialStaging == null)
            {
                return NotFound();
            }

            dbContext.SerialStagings.Remove(serialStaging);
            dbContext.SaveChanges();

            return Ok(serialStaging);
        }
    }
}

