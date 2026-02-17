using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasedProductsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PurchasedProductsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all purchased products
        [HttpGet]
        public IActionResult GetAllPurchasedProducts()
        {
            var products = dbContext.PurchasedProducts.OrderByDescending(p => p.Id)
                .ToList();
            return Ok(products);
        }

        // Get purchased product by ID
        [HttpGet("{id}")]
        public IActionResult GetPurchasedProductById(int id)
        {
            var product = dbContext.PurchasedProducts.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = "Purchased product not found" });
            }

            return Ok(product);
        }

        // Add a new purchased product
        [HttpPost]
        public IActionResult AddPurchasedProduct(AddPurchasedProductDto addPurchasedProductDto)
        {
            var purchasedProduct = new PurchasedProduct
            {
                ProductId = addPurchasedProductDto.ProductId,
                Quantity = addPurchasedProductDto.Quantity,

            };

            dbContext.PurchasedProducts.Add(purchasedProduct);
            dbContext.SaveChanges();

            return Ok(purchasedProduct);
        }

        // Update an existing purchased product
        [HttpPut("{id}")]
        public IActionResult UpdatePurchasedProduct(int id, UpdatePurchasedProductDto updatePurchasedProductDto)
        {
            var product = dbContext.PurchasedProducts.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = "Purchased product not found" });
            }

            product.ProductId = updatePurchasedProductDto.ProductId;
            product.Quantity = updatePurchasedProductDto.Quantity;


            dbContext.SaveChanges();

            return Ok(product);
        }

        // Delete a purchased product
        [HttpDelete("{id}")]
        public IActionResult DeletePurchasedProduct(int id)
        {
            var product = dbContext.PurchasedProducts.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = "Purchased product not found" });
            }

            dbContext.PurchasedProducts.Remove(product);
            dbContext.SaveChanges();

            return Ok(product);
        }
    }
}
