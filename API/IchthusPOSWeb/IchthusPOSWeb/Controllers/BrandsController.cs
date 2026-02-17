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
    public class BrandsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public BrandsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllBrands()
        {
            var allBrands = dbContext.Brands.OrderByDescending(p => p.Id)
                .ToList();

            return Ok(allBrands);
        }

        [HttpPost]
        public IActionResult AddBrand(AddBrandDto addBrandDto)
        {
            var BrandEntity = new Brand()
            {
                BrandName = addBrandDto.BrandName,
            };

            dbContext.Brands.Add(BrandEntity);
            dbContext.SaveChanges();

            return Ok(BrandEntity);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateBrand(int id, UpdateBrandDto updateBrandDto)
        {
            var Brand = dbContext.Brands.Find(id);

            if(Brand is null)
            {
                return NotFound();
            }

            Brand.BrandName = updateBrandDto.BrandName;

            dbContext.SaveChanges();

            return Ok(Brand);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteBrand(int id)
        {
            var Brand = dbContext.Brands.Find(id);

            if (Brand is null)
            {
                return NotFound();
            }

            dbContext.Brands.Remove(Brand);
            dbContext.SaveChanges();

            return Ok(Brand);
        }
    }
}
