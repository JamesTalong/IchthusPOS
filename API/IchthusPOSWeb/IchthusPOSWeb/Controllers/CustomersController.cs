using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CustomersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var allCustomers = dbContext.Customers.OrderByDescending(p => p.Id).ToList();
            return Ok(allCustomers);
        }
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            // Find the customer by ID
            var customer = dbContext.Customers.Find(id);

            // If the customer is not found, return a 404 Not Found response
            if (customer == null)
            {
                return NotFound();
            }

            // Return the customer details if found
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult AddCustomer(AddCustomerDto addCustomerDto)
        {
            var customerEntity = new Customer()
            {
                CustomerName = addCustomerDto.CustomerName,
                Address = addCustomerDto.Address,
                TinNumber = addCustomerDto.TinNumber,
                MobileNumber = addCustomerDto.MobileNumber,
                BusinessStyle = addCustomerDto.BusinessStyle,
                RFID = addCustomerDto.RFID,
                CustomerType = addCustomerDto.CustomerType,
            };

            dbContext.Customers.Add(customerEntity);
            dbContext.SaveChanges();

            return Ok(customerEntity);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = dbContext.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            customer.CustomerName = updateCustomerDto.CustomerName;
            customer.Address = updateCustomerDto.Address;
            customer.TinNumber = updateCustomerDto.TinNumber;
            customer.MobileNumber = updateCustomerDto.MobileNumber;
            customer.BusinessStyle = updateCustomerDto.BusinessStyle;
            customer.RFID = updateCustomerDto.RFID;
            customer.CustomerType = updateCustomerDto.CustomerType;

            dbContext.SaveChanges();

            return Ok(customer);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = dbContext.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            dbContext.Customers.Remove(customer);
            dbContext.SaveChanges();

            return Ok(customer);
        }
    }
}