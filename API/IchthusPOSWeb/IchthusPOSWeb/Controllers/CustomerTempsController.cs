
using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTempsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CustomerTempsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all CustomerTemps
        [HttpGet]
        public IActionResult GetAllCustomerTemps()
        {
            var allCustomerTemps = dbContext.CustomerTemps
                .Select(ct => new
                {
                    Id = ct.Id,
                    CustomerId = ct.CustomerId,
                    CustomerName = ct.CustomerName,
                 
    })
                .ToList();

            return Ok(allCustomerTemps);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerTempById(int id)
        {
            var customerTemp = dbContext.CustomerTemps
                .Select(ct => new
                {
                    Id = ct.Id,
                    CustomerId = ct.CustomerId,
                    CustomerName = ct.CustomerName,
        
                })
                .FirstOrDefault(ct => ct.Id == id);

            if (customerTemp == null)
            {
                return NotFound(new { message = "CustomerTemp not found" });
            }

            return Ok(customerTemp);
        }

        [HttpPost]
        public IActionResult AddCustomerTemp(AddCustomerTempDto addCustomerTempDto)
        {
            var customerTempEntity = new CustomerTemp
            {
                CustomerId = addCustomerTempDto.CustomerId,
                CustomerName = addCustomerTempDto.CustomerName
            };

            dbContext.CustomerTemps.Add(customerTempEntity);
            dbContext.SaveChanges();

            return Ok(customerTempEntity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomerTemp(int id, UpdateCustomerTempDto updateCustomerTempDto)
        {
            var customerTemp = dbContext.CustomerTemps.FirstOrDefault(ct => ct.Id == id);

            if (customerTemp == null)
            {
                return NotFound();
            }

            customerTemp.CustomerId = updateCustomerTempDto.CustomerId;
            customerTemp.CustomerName = updateCustomerTempDto.CustomerName;

            dbContext.SaveChanges();

            return Ok(customerTemp);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomerTemp(int id)
        {
            var customerTemp = dbContext.CustomerTemps.FirstOrDefault(ct => ct.Id == id);

            if (customerTemp == null)
            {
                return NotFound();
            }

            dbContext.CustomerTemps.Remove(customerTemp);
            dbContext.SaveChanges();

            return Ok(customerTemp);
        }
        // DELETE: api/CustomerTemps/by-customer/{customerId}
        [HttpDelete("by-customer/{customerId}")]
        public IActionResult DeleteCustomerTempsByCustomerId(int customerId)
        {
            var customerTemps = dbContext.CustomerTemps.Where(ct => ct.CustomerId == customerId).ToList();

            if (!customerTemps.Any())
            {
                return NoContent(); // No matching records to delete
            }

            dbContext.CustomerTemps.RemoveRange(customerTemps);
            dbContext.SaveChanges();

            return Ok(new { Message = $"{customerTemps.Count} CustomerTemps deleted successfully", DeletedItems = customerTemps });
        }

        // DELETE: api/CustomerTemps/delete-all
        [HttpDelete("delete-all")]
        public IActionResult DeleteAllCustomerTemps()
        {
            var allCustomerTemps = dbContext.CustomerTemps.ToList();

            if (!allCustomerTemps.Any())
            {
                return NoContent(); // No records to delete
            }

            dbContext.CustomerTemps.RemoveRange(allCustomerTemps);
            dbContext.SaveChanges();

            return Ok(new { Message = $"{allCustomerTemps.Count} CustomerTemps deleted successfully" });
        }
    }
}
