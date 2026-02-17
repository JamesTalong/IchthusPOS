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
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var allCategories = dbContext.Categories.OrderByDescending(p => p.Id)
                .ToList();

            return Ok(allCategories);
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryDto addCategoryDto)
        {
            var categoryEntity = new Category()
            {
                CategoryName = addCategoryDto.CategoryName,
            };

            dbContext.Categories.Add(categoryEntity);
            dbContext.SaveChanges();

            return Ok(categoryEntity);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = dbContext.Categories.Find(id);

            if(category is null)
            {
                return NotFound();
            }

            category.CategoryName = updateCategoryDto.CategoryName;

            dbContext.SaveChanges();

            return Ok(category);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = dbContext.Categories.Find(id);

            if (category is null)
            {
                return NotFound();
            }

            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();

            return Ok(category);
        }
    }
}
