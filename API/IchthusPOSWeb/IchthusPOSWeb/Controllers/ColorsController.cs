using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ColorsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllColors()
        {
            var allColors = dbContext.Colors.OrderByDescending(p => p.Id).ToList();
            return Ok(allColors);
        }

        [HttpPost]
        public IActionResult AddColor(AddColorDto addColorDto)
        {
            var colorEntity = new Color()
            {
                ColorName = addColorDto.ColorName,
            };

            dbContext.Colors.Add(colorEntity);
            dbContext.SaveChanges();
            return Ok(colorEntity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateColor(int id, UpdateColorDto updateColorDto)
        {
            var color = dbContext.Colors.Find(id);

            if (color is null)
            {
                return NotFound();
            }

            color.ColorName = updateColorDto.ColorName;

            dbContext.SaveChanges();
            return Ok(color);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteColor(int id)
        {
            var color = dbContext.Colors.Find(id);

            if (color is null)
            {
                return NotFound();
            }

            dbContext.Colors.Remove(color);
            dbContext.SaveChanges();
            return Ok(color);
        }
    }
}
