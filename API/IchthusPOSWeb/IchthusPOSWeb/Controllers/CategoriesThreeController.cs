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
    public class CategoriesThreeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriesThreeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCategoriesThree()
        {
            var allCategoriesThree = dbContext.CategoriesThree.OrderByDescending(p => p.Id).ToList();

            return Ok(allCategoriesThree);
        }

        [HttpPost]
        public IActionResult AddCategoryThree(AddCategoryThreeDto addCategoryThreeDto)
        {
            var categoryThreeEntity = new CategoryThree()
            {
                CategoryThreeName = addCategoryThreeDto.CategoryThreeName,
            };

            dbContext.CategoriesThree.Add(categoryThreeEntity);
            dbContext.SaveChanges();

            return Ok(categoryThreeEntity);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateCategoryThree(int id, UpdateCategoryThreeDto updateCategoryThreeDto)
        {
            var categoryThree = dbContext.CategoriesThree.Find(id);

            if (categoryThree is null)
            {
                return NotFound();
            }

            categoryThree.CategoryThreeName = updateCategoryThreeDto.CategoryThreeName;

            dbContext.SaveChanges();

            return Ok(categoryThree);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCategoryThree(int id)
        {
            var categoryThree = dbContext.CategoriesThree.Find(id);

            if (categoryThree is null)
            {
                return NotFound();
            }

            dbContext.CategoriesThree.Remove(categoryThree);
            dbContext.SaveChanges();

            return Ok(categoryThree);
        }
    }
}
