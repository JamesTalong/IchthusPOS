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
    public class BatchesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public BatchesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all batches with related pricelist and serial numbers
        [HttpGet]
        public IActionResult GetAllBatches()
        {
            var allBatches = dbContext.Batches
                .Include(b => b.Pricelist)
                .Include(b => b.SerialNumbers)
                .Select(b => new
                {
                    b.Id,
                    b.BatchDate,
                    b.NumberOfItems,
                    PricelistId = b.PricelistId,
                    PricelistProduct = b.Pricelist.Product,
                    b.HasSerial,
                    SerialNumbers = b.SerialNumbers.Select(s => new
                    {
                        s.Id,
                        s.SerialName,
                        s.IsSold
                    }).ToList()
                })
                .OrderByDescending(p => p.Id)
                .ToList();


            return Ok(allBatches);
        }

        [HttpGet("{id}")]
        public IActionResult GetBatchById(int id)
        {
            var batch = dbContext.Batches
                .Include(b => b.Pricelist)
                .Include(b => b.SerialNumbers)
                .Select(b => new
                {
                    b.Id,
                    b.BatchDate,
                    b.NumberOfItems,
                    PricelistId = b.PricelistId,
                    PricelistProduct = b.Pricelist.Product,
                    b.HasSerial,
                    SerialNumbers = b.SerialNumbers.Select(s => new
                    {
                        s.Id,
                        s.SerialName,
                        s.IsSold
                    }).ToList()
                })
                .FirstOrDefault(b => b.Id == id);

            if (batch == null)
            {
                return NotFound(new { message = "Batch not found" });
            }

            return Ok(batch);
        }

        [HttpPost]
        public IActionResult AddBatch(AddBatchDto addBatchDto)
        {
            var pricelistExists = dbContext.Pricelists.Any(p => p.Id == addBatchDto.PricelistId);

            if (!pricelistExists)
            {
                return BadRequest(new { message = $"Pricelist with Id '{addBatchDto.PricelistId}' does not exist." });
            }

            var serials = addBatchDto.SerialNumbers;

            if (addBatchDto.HasSerial == false && (serials == null || !serials.Any()))
            {
                serials = Enumerable.Range(0, addBatchDto.NumberOfItems ?? 0)
                    .Select(_ => new SerialNumber
                    {
                        SerialName = "",
                        IsSold = false
                    }).ToList();
            }

            var batchEntity = new Batch
            {
                BatchDate = addBatchDto.BatchDate,
                NumberOfItems = addBatchDto.NumberOfItems,
                PricelistId = addBatchDto.PricelistId,
                HasSerial = addBatchDto.HasSerial,
                SerialNumbers = serials // just assign them directly
            };

            dbContext.Batches.Add(batchEntity);
            dbContext.SaveChanges();

            return Ok(batchEntity);
        }

        [HttpPost("migrate-from-stagings")]
        public IActionResult MigrateAllFromBatchStagings()
        {
            var batchStagings = dbContext.BatchStagings
                .Include(b => b.SerialStagings)
                .ToList();

            if (!batchStagings.Any())
            {
                return BadRequest(new { message = "No batch stagings found to migrate." });
            }

            var migratedBatches = new List<Batch>();
            var historyEntries = new List<BatchStaginghistory>();

            foreach (var staging in batchStagings)
            {
                // Ensure pricelist exists
                var pricelistExists = dbContext.Pricelists.Any(p => p.Id == staging.PricelistId);
                if (!pricelistExists)
                {
                    return BadRequest(new { message = $"Pricelist with Id '{staging.PricelistId}' does not exist." });
                }

                // Migrate to Batches
                var newBatch = new Batch
                {
                    BatchDate = staging.BatchDate,
                    NumberOfItems = staging.NumberOfItems,
                    PricelistId = staging.PricelistId,
                    HasSerial = staging.HasSerial,
                    SerialNumbers = staging.SerialStagings?.Select(sn => new SerialNumber
                    {
                        SerialName = sn.SerialName,
                        IsSold = sn.IsSold
                    }).ToList()
                };

                dbContext.Batches.Add(newBatch);
                migratedBatches.Add(newBatch);

                // Save history before deletion
                var historyEntry = new BatchStaginghistory
                {
                    UserId = staging.UserId,
                    UserName = staging.UserName,
                    BatchDate = staging.BatchDate,
                    NumberOfItems = staging.NumberOfItems,
                    PricelistId = staging.PricelistId,
                    HasSerial = staging.HasSerial,
                    SerialStagingsHistory = staging.SerialStagings?.Select(sn => new SerialStagingsHistory
                    {
                        SerialName = sn.SerialName,
                        IsSold = sn.IsSold
                    }).ToList()
                };

                historyEntries.Add(historyEntry);
            }

            // Save migrated batches and history
            dbContext.BatchStagingsHistory.AddRange(historyEntries);
            dbContext.SaveChanges();

            // Delete from staging
            dbContext.SerialStagings.RemoveRange(dbContext.SerialStagings);
            dbContext.BatchStagings.RemoveRange(batchStagings);
            dbContext.SaveChanges();

            return Ok(new
            {
                message = $"{migratedBatches.Count} batches migrated and saved to history successfully.",
                migrated = migratedBatches
            });
        }


        // Update an existing batch
        [HttpPut("{id}")]
        public IActionResult UpdateBatch(int id, UpdateBatchDto updateBatchDto)
        {
            var existingBatch = dbContext.Batches
                .Include(b => b.SerialNumbers)
                .AsNoTracking()
                .FirstOrDefault(b => b.Id == id);

            if (existingBatch == null)
                return NotFound(new { message = "Batch not found." });

            // Delete old serial numbers
            var oldSerials = dbContext.SerialNumbers.Where(sn => sn.BatchId == id);
            dbContext.SerialNumbers.RemoveRange(oldSerials);
            dbContext.SaveChanges();

            // Recreate serial numbers
            var newSerials = updateBatchDto.SerialNumbers?.Select(sn => new SerialNumber
            {
                SerialName = sn.SerialName,
                IsSold = sn.IsSold,
                BatchId = id
            }).ToList() ?? new List<SerialNumber>();

            dbContext.SerialNumbers.AddRange(newSerials);
            dbContext.SaveChanges();

            // ⚠ Use raw SQL to update batch directly (bypasses OUTPUT)
            string sql = @"
        UPDATE Batches
        SET BatchDate = @BatchDate,
            NumberOfItems = @NumberOfItems,
            PricelistId = @PricelistId,
            HasSerial = @HasSerial
        WHERE Id = @Id";

            var parameters = new[]
            {
        new SqlParameter("@BatchDate", updateBatchDto.BatchDate),
        new SqlParameter("@NumberOfItems", updateBatchDto.NumberOfItems),
        new SqlParameter("@PricelistId", updateBatchDto.PricelistId),
        new SqlParameter("@HasSerial", updateBatchDto.HasSerial),
        new SqlParameter("@Id", id)
    };

            dbContext.Database.ExecuteSqlRaw(sql, parameters);

            return Ok(new
            {
                message = "Batch updated successfully.",
                batchId = id
            });
        }




        [HttpPut("fixBatches/{id}")]
        public IActionResult UpdateInventory(int id, UpdateBatchDto updateBatchDto)
        {
            var batch = dbContext.Batches
                .Include(b => b.SerialNumbers)
                .FirstOrDefault(b => b.Id == id);

            if (batch == null)
            {
                return NotFound();
            }

            // Update batch properties
            batch.BatchDate = updateBatchDto.BatchDate;
            batch.NumberOfItems = updateBatchDto.NumberOfItems;
            batch.PricelistId = updateBatchDto.PricelistId;
            batch.HasSerial = updateBatchDto.HasSerial;

            // Replace serial numbers
            dbContext.SerialNumbers.RemoveRange(batch.SerialNumbers);

            batch.SerialNumbers = updateBatchDto.SerialNumbers?.Select(sn => new SerialNumber
            {
                SerialName = sn.SerialName,
                IsSold = sn.IsSold,
            }).ToList() ?? new List<SerialNumber>();

            dbContext.SaveChanges();

            return Ok(batch);
        }


        // Delete a batch and its associated serial numbers
        [HttpDelete("{id}")]
        public IActionResult DeleteBatch(int id)
        {
            var batch = dbContext.Batches
      .Include(b => b.SerialNumbers)
      .FirstOrDefault(b => b.Id == id);

            if (batch == null)
            {
                return NotFound();
            }

            dbContext.SerialNumbers.RemoveRange(batch.SerialNumbers);
            dbContext.Batches.Remove(batch);
            dbContext.SaveChanges();

            return Ok(batch);
        }
    }
}
