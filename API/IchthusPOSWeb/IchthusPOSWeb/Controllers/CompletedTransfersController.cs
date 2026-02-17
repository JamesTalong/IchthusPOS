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
    public class CompletedTransfersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CompletedTransfersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompletedTransfers()
        {
            var completedTransfers = await dbContext.CompletedTransfers
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.SerialNumbers!)
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.Pricelist!)
                        .ThenInclude(pl => pl.Product!)
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.Pricelist!)
                        .ThenInclude(pl => pl.Location!) // ✅ now using Pricelist.Location
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.Pricelist!)
                        .ThenInclude(pl => pl.Color)
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.Pricelist!)
                        .ThenInclude(pl => pl.Batches!)
                            .ThenInclude(b => b.SerialNumbers!)
                .OrderByDescending(ct => ct.TransferId)
                .Select(ct => new
                {
                    ct.Id,
                    ct.TransferId,
                    ct.FromLocation,
                    ct.ToLocation,
                    ct.Status,
                    ct.ReleaseBy,
                    ct.ReceiveBy,
                    ct.TransferredDate,
                    ct.RecievedDate,
                    Items = ct.Items!.Select(item => new
                    {
                        item.Id,
                        item.ReceiverPricelistId,
                        item.PricelistId,
                        item.Quantity,
                        SerialNumbers = item.SerialNumbers!.Select(sn => new
                        {
                            sn.Id,
                            sn.SerialNumberId,
                            sn.Status,
                            SerialName = sn.SerialNumber != null ? sn.SerialNumber.SerialName : "Unknown",
                        }).ToList(),
                        Pricelist = item.Pricelist != null ? new
                        {
                            item.Pricelist.Id,
                            item.Pricelist.ProductId,
                            Product = item.Pricelist.Product != null ? new
                            {
                                item.Pricelist.Product.Id,
                                item.Pricelist.Product.ItemCode,
                                item.Pricelist.Product.BarCode,
                                item.Pricelist.Product.ProductName,
                            } : null,
                            // ✅ Location now from Pricelist
                            Location = item.Pricelist.Location != null ? new
                            {
                                item.Pricelist.Location.Id,
                                item.Pricelist.Location.LocationName
                            } : null,
                            Color = item.Pricelist.Color != null ? new
                            {
                                item.Pricelist.Color.Id,
                                item.Pricelist.Color.ColorName,
                            } : null,
                            item.Pricelist.VatEx,
                            item.Pricelist.VatInc,
                            item.Pricelist.Reseller,
                            PricelistBatchesSerials = item.Pricelist.Batches!
                                .SelectMany(b => b.SerialNumbers!)
                                .Select(sn => new
                                {
                                    sn.Id,
                                    sn.SerialName,
                                    sn.IsSold,
                                    sn.BatchId
                                }).ToList()
                        } : null
                    }).ToList()
                })
                .ToListAsync();

            return Ok(completedTransfers);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCompletedTransferById(int id)
        {
            var completedTransfer = await dbContext.CompletedTransfers
                .Where(ct => ct.Id == id)
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.SerialNumbers!)
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.Pricelist!)
                        .ThenInclude(pl => pl.Product!)
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.Pricelist!)
                        .ThenInclude(pl => pl.Location!) // ✅ Now using Pricelist.Location
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.Pricelist!)
                        .ThenInclude(pl => pl.Color)
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.Pricelist!)
                        .ThenInclude(pl => pl.Batches!)
                            .ThenInclude(b => b.SerialNumbers!)
                .Select(ct => new
                {
                    ct.Id,
                    ct.TransferId,
                    ct.FromLocation,
                    ct.ToLocation,
                    ct.Status,
                    ct.ReleaseBy,
                    ct.ReceiveBy,
                    ct.TransferredDate,
                    ct.RecievedDate,
                    Items = ct.Items!.Select(item => new
                    {
                        item.Id,
                        item.ReceiverPricelistId,
                        item.PricelistId,
                        item.Quantity,
                        SerialNumbers = item.SerialNumbers!.Select(sn => new
                        {
                            sn.Id,
                            sn.SerialNumberId,
                            sn.Status
                        }).ToList(),
                        Pricelist = item.Pricelist != null ? new
                        {
                            item.Pricelist.Id,
                            item.Pricelist.ProductId,
                            Product = item.Pricelist.Product != null ? new
                            {
                                item.Pricelist.Product.Id,
                                item.Pricelist.Product.ItemCode,
                                item.Pricelist.Product.BarCode,
                                item.Pricelist.Product.ProductName
                            } : null,
                            // ✅ Location from Pricelist (not Product)
                            Location = item.Pricelist.Location != null ? new
                            {
                                item.Pricelist.Location.Id,
                                item.Pricelist.Location.LocationName
                            } : null,
                            Color = item.Pricelist.Color != null ? new
                            {
                                item.Pricelist.Color.Id,
                                item.Pricelist.Color.ColorName,
                            } : null,
                            item.Pricelist.VatEx,
                            item.Pricelist.VatInc,
                            item.Pricelist.Reseller,
                            PricelistBatchesSerials = item.Pricelist.Batches!
                                .SelectMany(b => b.SerialNumbers!)
                                .Select(sn => new
                                {
                                    sn.Id,
                                    sn.SerialName,
                                    sn.IsSold,
                                    sn.BatchId
                                }).ToList()
                        } : null
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (completedTransfer == null)
            {
                return NotFound();
            }

            return Ok(completedTransfer);
        }


        [HttpPost]
        public async Task<IActionResult> AddCompletedTransfer([FromBody] AddCompletedTransferDto addCompletedTransferDto)
        {
            // 1. Create the CompletedTransfer entity
            var completedTransferEntity = new CompletedTransfer
            {
                TransferId = addCompletedTransferDto.TransferId,
                FromLocation = addCompletedTransferDto.FromLocation,
                ToLocation = addCompletedTransferDto.ToLocation,
                Status = addCompletedTransferDto.Status,
                ReleaseBy = addCompletedTransferDto.ReleaseBy,
                ReceiveBy = addCompletedTransferDto.ReceiveBy,
                TransferredDate = addCompletedTransferDto.TransferredDate,
                RecievedDate = addCompletedTransferDto.RecievedDate,
                Items = addCompletedTransferDto.Items?.Select(itemDto => new CompletedTransferItems
                {
                    ReceiverPricelistId = itemDto.ReceiverPricelistId,
                    PricelistId = itemDto.PricelistId,
                    Quantity = itemDto.Quantity, // This quantity is for the overall transfer item
                    SerialNumbers = itemDto.SerialNumbers?.Select(serialDto => new CompletedTransferSerial
                    {
                        SerialNumberId = serialDto.SerialNumberId,
                        Status = serialDto.Status // All serial numbers are saved here, regardless of status
                    }).ToList()
                }).ToList()
            };

            await dbContext.CompletedTransfers.AddAsync(completedTransferEntity);

            // 2. Create corresponding Batches for *only "Received" items/serials* in the transfer
            if (addCompletedTransferDto.Items != null)
            {
                foreach (var itemDto in addCompletedTransferDto.Items)
                {
                    // Filter for only "Received" serial numbers within this item
                    var receivedSerials = itemDto.SerialNumbers?
                                                .Where(s => s.Status == "Received")
                                                .ToList();

                    if (receivedSerials != null && receivedSerials.Any())
                    {
                        foreach (var serialDto in receivedSerials)
                        {
                            var batchEntity = new Batch
                            {
                                BatchDate = addCompletedTransferDto.RecievedDate,
                                NumberOfItems = 1, // Each received serial number creates its own batch entry of 1 item
                                PricelistId = itemDto.ReceiverPricelistId ?? 0,
                                HasSerial = true, // It definitely has a serial if we're in this loop
                                SerialNumbers = new List<SerialNumber>
                        {
                            new SerialNumber
                            {
                                SerialName = serialDto.SerialName,
                                IsSold = false
                            }
                        }
                            };
                            dbContext.Batches.Add(batchEntity);
                        }
                    }
                    else if (!itemDto.SerialNumbers.Any() && itemDto.Quantity > 0)
                    {
                        // This handles non-serial items. If there are no serial numbers
                        // but a quantity is specified, assume all are received and create one batch for the quantity.
                        var batchEntity = new Batch
                        {
                            BatchDate = addCompletedTransferDto.RecievedDate,
                            NumberOfItems = itemDto.Quantity, // Use the total quantity for non-serial items
                            PricelistId = itemDto.ReceiverPricelistId ?? 0,
                            HasSerial = false,
                            SerialNumbers = Enumerable.Range(0, itemDto.Quantity ?? 0)
                                .Select(_ => new SerialNumber
                                {
                                    SerialName = "",
                                    IsSold = false
                                }).ToList()
                        };
                        dbContext.Batches.Add(batchEntity);
                    }
                }
            }

            // 3. Delete the original Transfer record and its associated TransferItems
            var originalTransfer = await dbContext.Transfers
                                          .Include(t => t.Items) // Eagerly load the related TransferItems
                                          .FirstOrDefaultAsync(t => t.Id == addCompletedTransferDto.TransferId);

            if (originalTransfer == null)
            {
                Console.WriteLine($"Warning: Original Transfer with ID {addCompletedTransferDto.TransferId} not found during completion process.");
            }
            else
            {
                if (originalTransfer.Items != null && originalTransfer.Items.Any())
                {
                    dbContext.TransferItems.RemoveRange(originalTransfer.Items);
                }

                // Now it's safe to remove the original Transfer
                dbContext.Transfers.Remove(originalTransfer);
            }

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log the detailed error, including inner exceptions, for better diagnostics
                Console.WriteLine($"Error saving changes: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                // It's good to return a more informative error to the client or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while saving data. See server logs for details.", error = ex.Message });
            }
            return CreatedAtAction(nameof(GetCompletedTransferById), new { id = completedTransferEntity.Id }, completedTransferEntity);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCompletedTransfer(int id, [FromBody] UpdateReceivedTransferDto updateCompletedTransferDto)
        {
            var existingCompletedTransfer = await dbContext.CompletedTransfers
           .Include(ct => ct.Items!)
           .ThenInclude(cti => cti.SerialNumbers!)
           .FirstOrDefaultAsync(ct => ct.Id == id);

            if (existingCompletedTransfer == null)
            {
                return NotFound();
            }
            existingCompletedTransfer.TransferId = updateCompletedTransferDto.TransferId;
            existingCompletedTransfer.FromLocation = updateCompletedTransferDto.FromLocation;
            existingCompletedTransfer.ToLocation = updateCompletedTransferDto.ToLocation;
            existingCompletedTransfer.Status = updateCompletedTransferDto.Status;
            existingCompletedTransfer.ReleaseBy = updateCompletedTransferDto.ReleaseBy;
            existingCompletedTransfer.ReceiveBy = updateCompletedTransferDto.ReceiveBy;
            existingCompletedTransfer.TransferredDate = updateCompletedTransferDto.TransferredDate;
            existingCompletedTransfer.RecievedDate = updateCompletedTransferDto.RecievedDate;

            // Clear existing items and their serial numbers if they exist
            if (existingCompletedTransfer.Items != null)
            {
                foreach (var existingItem in existingCompletedTransfer.Items.ToList())
                {
                    if (existingItem.SerialNumbers != null)
                    {
                        dbContext.CompletedTransferSerials.RemoveRange(existingItem.SerialNumbers);
                    }
                    dbContext.CompletedTransferItems.Remove(existingItem);
                }
                existingCompletedTransfer.Items.Clear();
            }
            else
            {
                existingCompletedTransfer.Items = new List<CompletedTransferItems>();
            }


            // Add new or updated items and their serial numbers
            if (updateCompletedTransferDto.Items != null)
            {
                foreach (var itemDto in updateCompletedTransferDto.Items)
                {
                    var newItem = new CompletedTransferItems
                    {
                        ReceiverPricelistId = itemDto.ReceiverPricelistId,
                        PricelistId = itemDto.PricelistId,
                        Quantity = itemDto.Quantity,
                        SerialNumbers = itemDto.SerialNumbers?.Select(serialDto => new CompletedTransferSerial
                        {
                            SerialNumberId = serialDto.SerialNumberId,
                            Status = serialDto.Status
                        }).ToList()
                    };
                    existingCompletedTransfer.Items.Add(newItem);
                }
            }

            await dbContext.SaveChangesAsync();

            return Ok(existingCompletedTransfer);
        }


        [HttpPut("{completedTransferId}/items/{completedTransferItemId}/serials/{completedTransferSerialId}/receive")]
        public async Task<IActionResult> UpdateSerialDataOnReceive(
int completedTransferId,          // From URL: ID of the CompletedTransfer
int completedTransferItemId,      // From URL: ID of the CompletedTransferItem within the transfer
int completedTransferSerialId,    // From URL: ID of the CompletedTransferSerial to update status
[FromBody] UpdateSerialOnReceiveRequest dto) // From Request Body: contains SerialNumberValue
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // --- Part 1: Update the status of the CompletedTransferSerial record ---
            var completedTransfer = await dbContext.CompletedTransfers
                .Include(ct => ct.Items!) // Ensure Items is not null
                    .ThenInclude(cti => cti.SerialNumbers!) // Ensure SerialNumbers is not null for each item
                .FirstOrDefaultAsync(ct => ct.Id == completedTransferId);

            if (completedTransfer == null)
            {
                return NotFound(new { message = $"Completed Transfer with ID {completedTransferId} not found." });
            }

            var completedItem = completedTransfer.Items?
                .FirstOrDefault(i => i.Id == completedTransferItemId);

            if (completedItem == null)
            {
                return NotFound(new { message = $"Completed Transfer Item with ID {completedTransferItemId} in Transfer {completedTransferId} not found." });
            }

            // Find the specific CompletedTransferSerial by its PK (completedTransferSerialId)
            // and also verify its linked SerialNumberId matches the one from the DTO (dto.SerialNumberValue).
            // This assumes CompletedTransferSerial has an integer property 'SerialNumberId' linking to the main SerialNumber.
            var completedSerialToUpdate = completedItem.SerialNumbers?
                .FirstOrDefault(s => s.Id == completedTransferSerialId && s.SerialNumberId == dto.SerialNumberValue);

            if (completedSerialToUpdate == null)
            {
                return NotFound(new { message = $"CompletedTransferSerial with ID {completedTransferSerialId} and associated main SerialNumber ID {dto.SerialNumberValue} not found within Item {completedTransferItemId}." });
            }

            completedSerialToUpdate.Status = "Received"; // Update status as requested

            // --- Part 2: Find the main SerialNumber entity and update its IsSold status ---
            // 'dto.SerialNumberValue' is the PK of this main SerialNumber entity.
            // This assumes 'SerialNumbers' is the DbSet for your main serial number entities.
            var mainSerialNumberEntity = await dbContext.SerialNumbers
                .FirstOrDefaultAsync(s => s.Id == dto.SerialNumberValue);

            if (mainSerialNumberEntity == null)
            {
                // This implies an inconsistency if CompletedTransferSerial references a non-existent main SerialNumber.
                return NotFound(new { message = $"Main SerialNumber entity with ID '{dto.SerialNumberValue}' not found. Cannot update IsSold status." });
            }

            mainSerialNumberEntity.IsSold = false; // Update IsSold status as requested

            // --- Save Changes to the Database ---
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log the error and return a server error status
                Console.WriteLine($"Concurrency error: {ex.Message}"); // Replace with proper logging
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A concurrency error occurred while saving changes. Please try again." });
            }
            catch (Exception ex)
            {
                // Log the error and return a server error status
                Console.WriteLine($"Error saving changes: {ex.Message}"); // Replace with proper logging
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while saving changes to the database.", details = ex.Message });
            }

            return Ok(new
            {
                message = $"Successfully updated status for CompletedTransferSerial ID {completedTransferSerialId} to 'Received', and set IsSold to false for main SerialNumber ID {dto.SerialNumberValue}.",
                updatedCompletedTransferSerial = new { completedSerialToUpdate.Id, completedSerialToUpdate.SerialNumberId, completedSerialToUpdate.Status },
                updatedMainSerialNumber = new { mainSerialNumberEntity.Id, mainSerialNumberEntity.SerialName, mainSerialNumberEntity.IsSold }
            });
        }

        [HttpPost("{completedTransferId}/items/{completedTransferItemId}/serials/markFoundInToLocation")]
        public async Task<IActionResult> MarkSerialAsFoundInToLocation(
       int completedTransferId,
       int completedTransferItemId,
       [FromBody] MarkSerialFoundInToLocation dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // --- Part 1: Find and Update CompletedTransferSerial ---
            var completedTransfer = await dbContext.CompletedTransfers
                .Include(ct => ct.Items!)
                    .ThenInclude(item => item.SerialNumbers!) // Load CompletedTransferSerials
                .FirstOrDefaultAsync(ct => ct.Id == completedTransferId);

            if (completedTransfer == null)
            {
                return NotFound(new { message = $"Completed Transfer with ID {completedTransferId} not found." });
            }

            var completedItem = completedTransfer.Items?
                .FirstOrDefault(i => i.Id == completedTransferItemId);

            if (completedItem == null)
            {
                return NotFound(new { message = $"Completed Transfer Item with ID {completedTransferItemId} in Transfer {completedTransferId} not found." });
            }

            // Find the specific CompletedTransferSerial by its PK (dto.CompletedTransferSerialId)
   
            var completedSerialToUpdate = completedItem.SerialNumbers?
                .FirstOrDefault(s => s.Id == dto.CompletedTransferSerialId && s.SerialNumberId == dto.SerialNumberValue);

            if (completedSerialToUpdate == null)
            {
                return NotFound(new { message = $"CompletedTransferSerial with ID {dto.CompletedTransferSerialId} and associated main SerialNumber ID {dto.SerialNumberValue} not found within Item {completedTransferItemId}." });
            }

            if (completedSerialToUpdate.Status == "Received")
            {
                return BadRequest(new { message = $"Serial (Main ID: {dto.SerialNumberValue}) is already marked as 'Received'." });
            }

            // --- Part 2: Fetch Main SerialNumber details for Batch creation ---
            // dto.SerialNumberValue is the PK of the main SerialNumber entity.
            var mainSerialNumberEntity = await dbContext.SerialNumbers
                .FirstOrDefaultAsync(s => s.Id == dto.SerialNumberValue);

            if (mainSerialNumberEntity == null)
            {
                return NotFound(new { message = $"Main SerialNumber entity with ID '{dto.SerialNumberValue}' not found. Cannot create batch entry." });
            }
            // This is the SerialName we need, similar to serialDto.SerialName in your AddCompletedTransfer
            string serialNameForNewBatchEntry = mainSerialNumberEntity.SerialName;

            // Update status of the CompletedTransferSerial
            completedSerialToUpdate.Status = "Received";

            // --- Part 3: Create a new Batch and SerialNumber entry (imitating POST logic) ---
            if (completedItem.ReceiverPricelistId == null)
            {
                // ReceiverPricelistId is needed for the new Batch.
                return BadRequest(new { message = $"ReceiverPricelistId is missing for item ID {completedItem.Id}. Cannot create batch." });
            }

            var batchEntity = new Batch
            {
                BatchDate = completedTransfer.RecievedDate, // Or DateTime.UtcNow if preferred for "found" date
                NumberOfItems = 1, // For this single serial being marked as found
                PricelistId = completedItem.ReceiverPricelistId.Value,
                HasSerial = true, // This is a serialized item
                SerialNumbers = new List<SerialNumber>
        {
            new SerialNumber // Create a new SerialNumber record for this batch
            {
                SerialName = serialNameForNewBatchEntry, // Use the fetched name
                IsSold = false                           // As per requirement
                // If your SerialNumber entity has an ID that should be auto-generated,
                // ensure it's configured correctly in your model.
            }
        }
            };
            dbContext.Batches.Add(batchEntity);

            // --- Save Changes ---
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Concurrency error: {ex.Message}"); // Replace with proper logging
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A concurrency error occurred." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}"); // Replace with proper logging
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while saving changes." });
            }

            return Ok(new
            {
                message = $"Serial (Main ID: {dto.SerialNumberValue}, Name: '{serialNameForNewBatchEntry}') status updated to 'Received'. New batch created with ID {batchEntity.Id}.",
                updatedCompletedSerial = new { completedSerialToUpdate.Id, completedSerialToUpdate.SerialNumberId, completedSerialToUpdate.Status },
                newBatchDetails = new { batchEntity.Id, batchEntity.PricelistId, SerialNameInBatch = serialNameForNewBatchEntry }
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCompletedTransfer(int id)
        {
            var completedTransfer = await dbContext.CompletedTransfers
                .Include(ct => ct.Items!)
                .ThenInclude(cti => cti.SerialNumbers!)
                .FirstOrDefaultAsync(ct => ct.Id == id);

            if (completedTransfer == null)
            {
                return NotFound();
            }

            // Safely remove children first if they exist
            if (completedTransfer.Items != null)
            {
                dbContext.CompletedTransferSerials.RemoveRange(
                    completedTransfer.Items.Where(item => item.SerialNumbers != null)
                                            .SelectMany(item => item.SerialNumbers!));
                dbContext.CompletedTransferItems.RemoveRange(completedTransfer.Items);
            }

            dbContext.CompletedTransfers.Remove(completedTransfer);
            await dbContext.SaveChangesAsync();

            return Ok(completedTransfer);
        }
    }
}