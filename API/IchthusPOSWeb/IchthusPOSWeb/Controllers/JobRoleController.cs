using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IchthusPOSWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobRoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JobRoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobRole>>> GetAll()
        {
            return await _context.JobRoles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobRole>> Get(int id)
        {
            var role = await _context.JobRoles.FindAsync(id);
            if (role == null)
                return NotFound();
            return role;
        }

        [HttpPost]
        public async Task<ActionResult<JobRole>> Add(AddJobRoleDto dto) // <--- Use AddJobRoleDto
        {
            var role = new JobRole
            {
                RoleName = dto.RoleName,
                Dashboard = dto.Dashboard,
                Users = dto.Users,
                UserRestriction = dto.UserRestriction,
                InventoryCost = dto.InventoryCost, // <--- ADD THIS
                TransferItems = dto.TransferItems, // <--- ADD THIS
                Transfer = dto.Transfer,
                Categories = dto.Categories,
                Categories2 = dto.Categories2,
                Categories3 = dto.Categories3,
                Categories4 = dto.Categories4,
                Categories5 = dto.Categories5,
                Brands = dto.Brands,
                Colors = dto.Colors,
                Locations = dto.Locations,
                ProductList = dto.ProductList,
                Pricelists = dto.Pricelists,
                Batches = dto.Batches,
                SerialNumbers = dto.SerialNumbers,
                Customers = dto.Customers,
                Inventory = dto.Inventory,
                InventoryStaging = dto.InventoryStaging,
                Transactions = dto.Transactions,
                POS = dto.POS
            };

            _context.JobRoles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateJobRoleDto dto) // <--- Use UpdateJobRoleDto
        {
            var role = await _context.JobRoles.FindAsync(id);
            if (role == null)
                return NotFound();

            role.RoleName = dto.RoleName;
            role.Dashboard = dto.Dashboard;
            role.Users = dto.Users;
            role.UserRestriction = dto.UserRestriction;
            role.InventoryCost = dto.InventoryCost; // <--- ADD THIS
            role.TransferItems = dto.TransferItems; // <--- ADD THIS
            role.Transfer = dto.Transfer;
            role.Categories = dto.Categories;
            role.Categories2 = dto.Categories2;
            role.Categories3 = dto.Categories3;
            role.Categories4 = dto.Categories4;
            role.Categories5 = dto.Categories5;
            role.Brands = dto.Brands;
            role.Colors = dto.Colors;
            role.Locations = dto.Locations;
            role.ProductList = dto.ProductList;
            role.Pricelists = dto.Pricelists;
            role.Batches = dto.Batches;
            role.SerialNumbers = dto.SerialNumbers;
            role.Customers = dto.Customers;
            role.Inventory = dto.Inventory;
            role.InventoryStaging = dto.InventoryStaging;
            role.Transactions = dto.Transactions;
            role.POS = dto.POS;

            _context.JobRoles.Update(role); // Use Update method to explicitly mark as modified
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _context.JobRoles.FindAsync(id);
            if (role == null)
                return NotFound();

            _context.JobRoles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}