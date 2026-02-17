using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerialMainController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public SerialMainController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllSerialMains()
        {
            var serialMains = dbContext.SerialMains.ToList();
            return Ok(serialMains);
        }

        [HttpPost]
        public IActionResult AddSerialMain(SerialMain serialMain)
        {
            dbContext.SerialMains.Add(serialMain);
            dbContext.SaveChanges();
            return Ok(serialMain);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSerialMain(int id)
        {
            var serialMain = dbContext.SerialMains.Find(id);
            if (serialMain is null)
            {
                return NotFound();
            }

            dbContext.SerialMains.Remove(serialMain);
            dbContext.SaveChanges();
            return Ok(serialMain);
        }
    }
}
