using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ProductsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all products
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var allProducts = dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.CategoryTwo)
                .Include(p => p.CategoryThree)
                .Include(p => p.CategoryFour)
                .Include(p => p.CategoryFive)
                .ToList();

            var result = allProducts.Select(p => new
            {
                p.Id,
                p.ProductImage,
                p.ProductName,
                p.ItemCode,
                p.BarCode,
                p.Description,
                p.HasSerial,
                BrandId = p.BrandId,
                BrandName = p.Brand != null ? p.Brand.BrandName : null,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.CategoryName : null,
                CategoryTwoId = p.CategoryTwoId,
                CategoryTwoName = p.CategoryTwo != null ? p.CategoryTwo.CategoryTwoName : null,
                CategoryThreeId = p.CategoryThreeId,
                CategoryThreeName = p.CategoryThree != null ? p.CategoryThree.CategoryThreeName : null,
                CategoryFourId = p.CategoryFourId,
                CategoryFourName = p.CategoryFour != null ? p.CategoryFour.CategoryFourName : null,
                CategoryFiveId = p.CategoryFiveId,
                CategoryFiveName = p.CategoryFive != null ? p.CategoryFive.CategoryFiveName : null
            });

            return Ok(result.OrderByDescending(p => p.Id)
                .ToList());

        }


        // Add a new product
        [HttpPost]
        public IActionResult AddProduct([FromBody] AddProductDto addProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productEntity = new Product
            {
                ProductImage = addProductDto.ProductImage,
                ProductName = addProductDto.ProductName ?? string.Empty,
                ItemCode = addProductDto.ItemCode,
                BarCode = addProductDto.BarCode ?? string.Empty,
                Description = addProductDto.Description ?? string.Empty,
                HasSerial = addProductDto.HasSerial ?? false,
                BrandId = addProductDto.BrandId,
                Brand = dbContext.Brands.Find(addProductDto.BrandId),
                CategoryId = addProductDto.CategoryId,
                Category = dbContext.Categories.Find(addProductDto.CategoryId),
                CategoryTwoId = addProductDto.CategoryTwoId,
                CategoryTwo = dbContext.CategoriesTwo.Find(addProductDto.CategoryTwoId),
                CategoryThreeId = addProductDto.CategoryThreeId,
                CategoryThree = dbContext.CategoriesThree.Find(addProductDto.CategoryThreeId),
                CategoryFourId = addProductDto.CategoryFourId,
                CategoryFour = dbContext.CategoriesFour.Find(addProductDto.CategoryFourId),
                CategoryFiveId = addProductDto.CategoryFiveId,
             CategoryFive = dbContext.CategoriesFive.Find(addProductDto.CategoryFiveId),
            };

            dbContext.Products.Add(productEntity);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetAllProducts), new { id = productEntity.Id }, productEntity);
        }


        // Update a product
        // Update a product
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] AddProductDto updateProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.CategoryTwo)
                .Include(p => p.CategoryThree)
                .Include(p => p.CategoryFour)
                .Include(p => p.CategoryFive)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Update fields only if provided
            product.ProductImage = updateProductDto.ProductImage ?? product.ProductImage;
            product.ProductName = updateProductDto.ProductName ?? product.ProductName;
            product.ItemCode = updateProductDto.ItemCode ?? product.ItemCode;
            product.BarCode = updateProductDto.BarCode ?? product.BarCode;
            product.Description = updateProductDto.Description ?? product.Description;
            product.HasSerial = updateProductDto.HasSerial ?? product.HasSerial;

            if (updateProductDto.BrandId.HasValue)
            {
                product.BrandId = updateProductDto.BrandId.Value;
                product.Brand = dbContext.Brands.Find(updateProductDto.BrandId);
            }

            if (updateProductDto.CategoryId.HasValue)
            {
                product.CategoryId = updateProductDto.CategoryId.Value;
                product.Category = dbContext.Categories.Find(updateProductDto.CategoryId);
            }

            if (updateProductDto.CategoryTwoId.HasValue)
            {
                product.CategoryTwoId = updateProductDto.CategoryTwoId.Value;
                product.CategoryTwo = dbContext.CategoriesTwo.Find(updateProductDto.CategoryTwoId);
            }

            if (updateProductDto.CategoryThreeId.HasValue)
            {
                product.CategoryThreeId = updateProductDto.CategoryThreeId.Value;
                product.CategoryThree = dbContext.CategoriesThree.Find(updateProductDto.CategoryThreeId);
            }

            if (updateProductDto.CategoryFourId.HasValue)
            {
                product.CategoryFourId = updateProductDto.CategoryFourId.Value;
                product.CategoryFour = dbContext.CategoriesFour.Find(updateProductDto.CategoryFourId);
            }

            if (updateProductDto.CategoryFiveId.HasValue)
            {
                product.CategoryFiveId = updateProductDto.CategoryFiveId.Value;
                product.CategoryFive = dbContext.CategoriesFive.Find(updateProductDto.CategoryFiveId);
            }

            dbContext.SaveChanges();

            return Ok(product);
        }


        // Delete a product
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = dbContext.Products.Find(id);

            if (product is null)
            {
                return NotFound();
            }

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();

            return Ok(product);
        }
    }
}
