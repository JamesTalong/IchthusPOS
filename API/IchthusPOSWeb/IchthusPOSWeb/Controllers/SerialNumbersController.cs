using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerialNumbersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public SerialNumbersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllSerialNumbers()
        {
            var serialNumbers = dbContext.SerialNumbers
                .Select(s => new
                {
                    Id = s.Id,
                    SerialName = s.SerialName,
                    IsSold = s.IsSold,
                    BatchId = s.BatchId,
                    PricelistProduct = dbContext.Pricelists
                        .Where(p => p.Id == dbContext.Batches
                            .Where(b => b.Id == s.BatchId)
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

            return Ok(serialNumbers);
        }
        [HttpGet("{id}")]
        public IActionResult GetSerialNumberById(int id)
        {
            var serialNumber = dbContext.SerialNumbers
                .Select(s => new
                {
                    Id = s.Id,
                    SerialName = s.SerialName,
                    IsSold = s.IsSold,
                    BatchId = s.BatchId
                })
                .FirstOrDefault(s => s.Id == id);

            if (serialNumber == null)
            {
                return NotFound(new { message = "Serial number not found" });
            }

            return Ok(serialNumber);
        }


        [HttpPost]
        public IActionResult AddSerialNumber(AddSerialNumberDto addSerialNumberDto)
        {
            var serialNumberEntity = new SerialNumber
            {
                SerialName = addSerialNumberDto.SerialName,
                IsSold = addSerialNumberDto.IsSold,
            };

            dbContext.SerialNumbers.Add(serialNumberEntity);
            dbContext.SaveChanges();

            return Ok(serialNumberEntity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSerialNumber(int id, UpdateSerialNumberDto updateSerialNumberDto)
        {
            var serialNumber = dbContext.SerialNumbers.Find(id);

            if (serialNumber == null)
            {
                return NotFound();
            }

            serialNumber.SerialName = updateSerialNumberDto.SerialName;
            serialNumber.IsSold = updateSerialNumberDto.IsSold;

            dbContext.SaveChanges();

            return Ok(serialNumber);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSerialNumber(int id)
        {
            var serialNumber = dbContext.SerialNumbers.Find(id);

            if (serialNumber == null)
            {
                return NotFound();
            }

            dbContext.SerialNumbers.Remove(serialNumber);
            dbContext.SaveChanges();

            return Ok(serialNumber);
        }
    }
}
