using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchStagingsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public BatchStagingsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all batch stagings with related pricelist and serial numbers
        [HttpGet]
        public IActionResult GetAllBatchStagings()
        {
            var allBatchStagings = dbContext.BatchStagings
                .Include(b => b.Pricelist)
                .Include(b => b.SerialStagings)
                .Select(b => new
                {
                    b.Id,
                    b.UserId, // Included UserId
                    b.UserName, // Included UserName
                    b.BatchDate,
                    b.NumberOfItems,
                    PricelistId = b.PricelistId,
                    PricelistProduct = b.Pricelist.Product,
                    PricelistLocation = b.Pricelist.Location,
                    b.HasSerial,
                    SerialStagings = b.SerialStagings.Select(s => new
                    {
                        s.Id,
                        s.SerialName,
                        s.IsSold
                    }).ToList()
                })
                .OrderByDescending(p => p.Id)
                .ToList();


            return Ok(allBatchStagings);
        }

        [HttpGet("{id}")]
        public IActionResult GetBatchStagingById(int id)
        {
            var batchStaging = dbContext.BatchStagings
                .Include(b => b.Pricelist)
                .Include(b => b.SerialStagings)
                .Select(b => new
                {
                    b.Id,
                    b.UserId, // Included UserId
                    b.UserName, // Included UserName
                    b.BatchDate,
                    b.NumberOfItems,
                    PricelistId = b.PricelistId,
                    PricelistProduct = b.Pricelist.Product,
                    b.HasSerial,
                    SerialStagings = b.SerialStagings.Select(s => new
                    {
                        s.Id,
                        s.SerialName,
                        s.IsSold
                    }).ToList()
                })
                .FirstOrDefault(b => b.Id == id);

            if (batchStaging == null)
            {
                return NotFound(new { message = "Batch Staging not found" });
            }

            return Ok(batchStaging);
        }

        [HttpPost]
        public IActionResult AddBatchStaging(AddBatchStagingDto addBatchStagingDto)
        {
            var pricelistExists = dbContext.Pricelists.Any(p => p.Id == addBatchStagingDto.PricelistId);

            if (!pricelistExists)
            {
                return BadRequest(new { message = $"Pricelist with Id '{addBatchStagingDto.PricelistId}' does not exist." });
            }

            var serials = addBatchStagingDto.SerialStagings;

            if (addBatchStagingDto.HasSerial == false && (serials == null || !serials.Any()))
            {
                serials = Enumerable.Range(0, addBatchStagingDto.NumberOfItems ?? 0)
                    .Select(_ => new SerialStaging
                    {
                        SerialName = "",
                        IsSold = false
                    }).ToList();
            }

            var batchStagingEntity = new BatchStaging
            {
                UserId = addBatchStagingDto.UserId, // Included UserId
                UserName = addBatchStagingDto.UserName, // Included UserName
                BatchDate = addBatchStagingDto.BatchDate,
                NumberOfItems = addBatchStagingDto.NumberOfItems,
                PricelistId = addBatchStagingDto.PricelistId,
                HasSerial = addBatchStagingDto.HasSerial,
                SerialStagings = serials // just assign them directly
            };

            dbContext.BatchStagings.Add(batchStagingEntity);
            dbContext.SaveChanges();

            return Ok(batchStagingEntity);
        }

        [HttpPost("bulk")]
        public IActionResult AddBatchStagings([FromBody] List<AddBatchStagingDto> addBatchStagingDtos)

        {
            var createdBatches = new List<BatchStaging>();

            foreach (var dto in addBatchStagingDtos)
            {
                var pricelistExists = dbContext.Pricelists.Any(p => p.Id == dto.PricelistId);
                if (!pricelistExists)
                {
                    return BadRequest(new { message = $"Pricelist with Id '{dto.PricelistId}' does not exist." });
                }

                var serials = dto.SerialStagings;
                if (dto.HasSerial == false && (serials == null || !serials.Any()))
                {
                    serials = Enumerable.Range(0, dto.NumberOfItems ?? 0)
                        .Select(_ => new SerialStaging
                        {
                            SerialName = "",
                            IsSold = false
                        }).ToList();
                }

                var batchStagingEntity = new BatchStaging
                {
                    UserId = dto.UserId, // Included UserId
                    UserName = dto.UserName, // Included UserName
                    BatchDate = dto.BatchDate,
                    NumberOfItems = dto.NumberOfItems,
                    PricelistId = dto.PricelistId,
                    HasSerial = dto.HasSerial,
                    SerialStagings = serials
                };

                dbContext.BatchStagings.Add(batchStagingEntity);
                createdBatches.Add(batchStagingEntity);
            }

            dbContext.SaveChanges();

            return Ok(new
            {
                message = $"{createdBatches.Count} batch stagings added successfully.",
                batches = createdBatches
            });
        }


        // Update an existing batch staging
        [HttpPut("{id}")]
        public IActionResult UpdateBatchStaging(int id, UpdateBatchStagingDto updateBatchStagingDto)
        {
            var existingBatchStaging = dbContext.BatchStagings
                .Include(b => b.SerialStagings)
                .AsNoTracking()
                .FirstOrDefault(b => b.Id == id);

            if (existingBatchStaging == null)
                return NotFound(new { message = "Batch Staging not found." });

            // Delete old serial numbers
            var oldSerials = dbContext.SerialStagings.Where(sn => sn.Id == id);
            dbContext.SerialStagings.RemoveRange(oldSerials);
            dbContext.SaveChanges();

            // Recreate serial numbers
            var newSerials = updateBatchStagingDto.SerialStagings?.Select(sn => new SerialStaging
            {
                SerialName = sn.SerialName,
                IsSold = sn.IsSold,
                BatchStagingId = id // Foreign key to link to the batch
            }).ToList() ?? new List<SerialStaging>();

            dbContext.SerialStagings.AddRange(newSerials);
            dbContext.SaveChanges();

            // ⚠ Use raw SQL to update batch directly (bypasses OUTPUT)
            string sql = @"
                UPDATE BatchStagings
                SET UserId = @UserId, -- Included UserId
                    UserName = @UserName, -- Included UserName
                    BatchDate = @BatchDate,
                    NumberOfItems = @NumberOfItems,
                    PricelistId = @PricelistId,
                    HasSerial = @HasSerial
                WHERE Id = @Id";

            var parameters = new[]
            {
                new SqlParameter("@UserId", updateBatchStagingDto.UserId ?? (object)DBNull.Value), // Handle nullable
                new SqlParameter("@UserName", updateBatchStagingDto.UserName ?? (object)DBNull.Value), // Handle nullable
                new SqlParameter("@BatchDate", updateBatchStagingDto.BatchDate ?? (object)DBNull.Value), // Handle nullable
                new SqlParameter("@NumberOfItems", updateBatchStagingDto.NumberOfItems ?? (object)DBNull.Value), // Handle nullable
                new SqlParameter("@PricelistId", updateBatchStagingDto.PricelistId),
                new SqlParameter("@HasSerial", updateBatchStagingDto.HasSerial ?? (object)DBNull.Value), // Handle nullable
                new SqlParameter("@Id", id)
            };

            dbContext.Database.ExecuteSqlRaw(sql, parameters);

            return Ok(new
            {
                message = "Batch Staging updated successfully.",
                batchStagingId = id
            });
        }


        [HttpPut("fixBatchStagings/{id}")]
        public IActionResult UpdateInventory(int id, UpdateBatchStagingDto updateBatchStagingDto)
        {
            var batchStaging = dbContext.BatchStagings
                .Include(b => b.SerialStagings)
                .FirstOrDefault(b => b.Id == id);

            if (batchStaging == null)
            {
                return NotFound();
            }

            // Update batch properties
            batchStaging.UserId = updateBatchStagingDto.UserId; // Included UserId
            batchStaging.UserName = updateBatchStagingDto.UserName; // Included UserName
            batchStaging.BatchDate = updateBatchStagingDto.BatchDate;
            batchStaging.NumberOfItems = updateBatchStagingDto.NumberOfItems;
            batchStaging.PricelistId = updateBatchStagingDto.PricelistId;
            batchStaging.HasSerial = updateBatchStagingDto.HasSerial;

            // Replace serial numbers
            dbContext.SerialStagings.RemoveRange(batchStaging.SerialStagings);

            batchStaging.SerialStagings = updateBatchStagingDto.SerialStagings?.Select(sn => new SerialStaging
            {
                SerialName = sn.SerialName,
                IsSold = sn.IsSold,
                BatchStagingId = id // Ensure the foreign key is set
            }).ToList() ?? new List<SerialStaging>();

            dbContext.SaveChanges();

            return Ok(batchStaging);
        }


        // Delete a batch staging and its associated serial numbers
        [HttpDelete("{id}")]
        public IActionResult DeleteBatchStaging(int id)
        {
            var batchStaging = dbContext.BatchStagings
        .Include(b => b.SerialStagings)
        .FirstOrDefault(b => b.Id == id);

            if (batchStaging == null)
            {
                return NotFound();
            }

            dbContext.SerialStagings.RemoveRange(batchStaging.SerialStagings);
            dbContext.BatchStagings.Remove(batchStaging);
            dbContext.SaveChanges();

            return Ok(batchStaging);
        }

        [HttpDelete("serials/{id}")]
        public IActionResult DeleteSerial(int id)
        {
            var serial = dbContext.SerialStagings.FirstOrDefault(s => s.Id == id);
            if (serial == null)
            {
                return NotFound(new { message = "Serial not found" });
            }

            dbContext.SerialStagings.Remove(serial);
            dbContext.SaveChanges();

            return Ok(new { message = "Serial deleted successfully" });
        }
        [HttpDelete("all")]
        public IActionResult DeleteAllBatchStagings()
        {
            dbContext.Database.ExecuteSqlRaw("DELETE FROM SerialStagings");
            dbContext.Database.ExecuteSqlRaw("DELETE FROM BatchStagings");

            dbContext.SaveChanges();

            return Ok(new { message = "All batch stagings and associated serials deleted successfully." });
        }
    }
}