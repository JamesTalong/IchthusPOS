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
    public class CategoriesFiveController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriesFiveController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCategoriesFive()
        {
            var allCategoriesFive = dbContext.CategoriesFive.OrderByDescending(p => p.Id).ToList();

            return Ok(allCategoriesFive);
        }

        [HttpPost]
        public IActionResult AddCategoryFive(AddCategoryFiveDto addCategoryFiveDto)
        {
            var categoryFiveEntity = new CategoryFive()
            {
                CategoryFiveName = addCategoryFiveDto.CategoryFiveName,
            };

            dbContext.CategoriesFive.Add(categoryFiveEntity);
            dbContext.SaveChanges();

            return Ok(categoryFiveEntity);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateCategoryFive(int id, UpdateCategoryFiveDto updateCategoryFiveDto)
        {
            var categoryFive = dbContext.CategoriesFive.Find(id);

            if (categoryFive is null)
            {
                return NotFound();
            }

            categoryFive.CategoryFiveName = updateCategoryFiveDto.CategoryFiveName;

            dbContext.SaveChanges();

            return Ok(categoryFive);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCategoryFive(int id)
        {
            var categoryFive = dbContext.CategoriesFive.Find(id);

            if (categoryFive is null)
            {
                return NotFound();
            }

            dbContext.CategoriesFive.Remove(categoryFive);
            dbContext.SaveChanges();

            return Ok(categoryFive);
        }
    }
}