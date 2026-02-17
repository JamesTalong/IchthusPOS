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
    public class CategoriesTwoController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriesTwoController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCategoriesTwo()
        {
            var allCategoriesTwo = dbContext.CategoriesTwo.OrderByDescending(p => p.Id).ToList();

            return Ok(allCategoriesTwo);
        }

        [HttpPost]
        public IActionResult AddCategoryTwo(AddCategoryTwoDto addCategoryTwoDto)
        {
            var categoryTwoEntity = new CategoryTwo()
            {
                CategoryTwoName = addCategoryTwoDto.CategoryTwoName,
            };

            dbContext.CategoriesTwo.Add(categoryTwoEntity);
            dbContext.SaveChanges();

            return Ok(categoryTwoEntity);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateCategoryTwo(int id, UpdateCategoryTwoDto updateCategoryTwoDto)
        {
            var categoryTwo = dbContext.CategoriesTwo.Find(id);

            if (categoryTwo is null)
            {
                return NotFound();
            }

            categoryTwo.CategoryTwoName = updateCategoryTwoDto.CategoryTwoName;

            dbContext.SaveChanges();

            return Ok(categoryTwo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCategoryTwo(int id)
        {
            var categoryTwo = dbContext.CategoriesTwo.Find(id);

            if (categoryTwo is null)
            {
                return NotFound();
            }

            dbContext.CategoriesTwo.Remove(categoryTwo);
            dbContext.SaveChanges();

            return Ok(categoryTwo);
        }
    }
}
