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
    public class CategoriesFourController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriesFourController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCategoriesFour()
        {
            var allCategoriesFour = dbContext.CategoriesFour.OrderByDescending(p => p.Id).ToList();

            return Ok(allCategoriesFour);
        }

        [HttpPost]
        public IActionResult AddCategoryFour(AddCategoryFourDto addCategoryFourDto)
        {
            var categoryFourEntity = new CategoryFour()
            {
                CategoryFourName = addCategoryFourDto.CategoryFourName,
            };

            dbContext.CategoriesFour.Add(categoryFourEntity);
            dbContext.SaveChanges();

            return Ok(categoryFourEntity);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateCategoryFour(int id, UpdateCategoryFourDto updateCategoryFourDto)
        {
            var categoryFour = dbContext.CategoriesFour.Find(id);

            if (categoryFour is null)
            {
                return NotFound();
            }

            categoryFour.CategoryFourName = updateCategoryFourDto.CategoryFourName;

            dbContext.SaveChanges();

            return Ok(categoryFour);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCategoryFour(int id)
        {
            var categoryFour = dbContext.CategoriesFour.Find(id);

            if (categoryFour is null)
            {
                return NotFound();
            }

            dbContext.CategoriesFour.Remove(categoryFour);
            dbContext.SaveChanges();

            return Ok(categoryFour);
        }
    }
}
