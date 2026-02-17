using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public LocationsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllLocations()
        {
            var allLocations = dbContext.Locations.OrderByDescending(p => p.Id).ToList();

            return Ok(allLocations);
        }

        [HttpPost]
        public IActionResult AddLocation(AddLocationDto addLocationDto)
        {
            var locationEntity = new Location()
            {
                LocationName = addLocationDto.LocationName,
            };

            dbContext.Locations.Add(locationEntity);
            dbContext.SaveChanges();

            return Ok(locationEntity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLocation(int id, UpdateLocationDto updateLocationDto)
        {
            var location = dbContext.Locations.Find(id);

            if (location is null)
            {
                return NotFound();
            }

            location.LocationName = updateLocationDto.LocationName;

            dbContext.SaveChanges();

            return Ok(location);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLocation(int id)
        {
            var location = dbContext.Locations.Find(id);

            if (location is null)
            {
                return NotFound();
            }

            dbContext.Locations.Remove(location);
            dbContext.SaveChanges();

            return Ok(location);
        }
    }
}
