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
    public class ReceivedTransfersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ReceivedTransfersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReceivedTransfers()
        {
            var receivedTransfers = await dbContext.ReceivedTransfers
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.SerialNumbers!)
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.Pricelist!)
                        .ThenInclude(pl => pl.Product!)
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.Pricelist!)
                        .ThenInclude(pl => pl.Location!) // ✅ Use Pricelist.Location
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
                        item.ItemCode,
                        item.Quantity,
                        item.ProductName,
                        SerialNumbers = item.SerialNumbers!.Select(sn => new
                        {
                            sn.Id,
                            sn.SerialNumberId,
                            sn.Status,
                            SerialName = sn.SerialName
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
                            // ✅ Location from Pricelist
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

            return Ok(receivedTransfers);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetReceivedTransferById(int id)
        {
            var receivedTransfer = await dbContext.ReceivedTransfers
                .Where(ct => ct.Id == id)
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.SerialNumbers!)
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.Pricelist!)
                        .ThenInclude(pl => pl.Product!)
                .Include(ct => ct.Items!)
                    .ThenInclude(cti => cti.Pricelist!)
                        .ThenInclude(pl => pl.Location!) // ✅ Use Pricelist.Location
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
                        item.ItemCode,
                        item.Quantity,
                        item.ProductName,
                        SerialNumbers = item.SerialNumbers!.Select(sn => new
                        {
                            sn.Id,
                            sn.SerialNumberId,
                            sn.Status,
                            SerialName = sn.SerialName
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
                            // ✅ Location from Pricelist
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

            if (receivedTransfer == null)
            {
                return NotFound();
            }

            return Ok(receivedTransfer);
        }


        [HttpPost]
        public async Task<IActionResult> AddReceivedTransfer([FromBody] AddReceivedTransferDto addReceivedTransferDto)
        {
            // 1. Create the ReceivedTransfer entity
            var receivedTransferEntity = new ReceivedTransfer
            {
                TransferId = addReceivedTransferDto.TransferId,
                FromLocation = addReceivedTransferDto.FromLocation,
                ToLocation = addReceivedTransferDto.ToLocation,
                Status = addReceivedTransferDto.Status,
                ReleaseBy = addReceivedTransferDto.ReleaseBy,
                ReceiveBy = addReceivedTransferDto.ReceiveBy,
                TransferredDate = addReceivedTransferDto.TransferredDate,
                RecievedDate = addReceivedTransferDto.RecievedDate,
                Items = addReceivedTransferDto.Items?.Select(itemDto => new ReceivedTransferItems
                {
                    ReceiverPricelistId = itemDto.ReceiverPricelistId,
                    PricelistId = itemDto.PricelistId == 0 ? (int?)null : itemDto.PricelistId,
                    ItemCode = itemDto.ItemCode,
                    ProductName = itemDto.ProductName,
                    Quantity = itemDto.Quantity,
                    SerialNumbers = itemDto.SerialNumbers?.Select(serialDto => new ReceivedTransferSerial
                    {
                        Status = itemDto.ReceiverPricelistId == 0 ? "Unrecorded" : serialDto.Status,
                        SerialName = serialDto.SerialName
                    }).ToList()
                }).ToList()
            };

            await dbContext.ReceivedTransfers.AddAsync(receivedTransferEntity);

            // 2. Create corresponding Batches for *only "Received" items/serials*
            //    AND where ReceiverPricelistId is NOT 0.
            foreach (var itemDto in addReceivedTransferDto.Items)
            {
                // Only proceed to create a batch if ReceiverPricelistId is NOT 0
                if (itemDto.ReceiverPricelistId != 0)
                {
                    // For serializable items
                    if (itemDto.SerialNumbers != null && itemDto.SerialNumbers.Any())
                    {
                        foreach (var serialDto in itemDto.SerialNumbers)
                        {
                            // Create batch only if serial status is "Received"
                            // (The status in the entity might have been changed to "Unrecorded" above,
                            // but for batch creation, we still rely on the original DTO status if it was "Received"
                            // and the pricelist was valid to begin with).
                            // Or, you can check the *actual* status that was mapped to the entity:
                            // We need to find the corresponding serial in the already mapped receivedTransferEntity
                            var mappedItem = receivedTransferEntity.Items.FirstOrDefault(i => i.ItemCode == itemDto.ItemCode && i.PricelistId == itemDto.PricelistId);
                            var mappedSerial = mappedItem?.SerialNumbers.FirstOrDefault(s => s.SerialName == serialDto.SerialName);

                            if (mappedSerial != null && mappedSerial.Status == "Received") // Check the status *after* potential modification
                            {
                                var batchEntity = new Batch
                                {
                                    BatchDate = addReceivedTransferDto.RecievedDate,
                                    NumberOfItems = 1,
                                    PricelistId = itemDto.ReceiverPricelistId ?? 0, // Use the non-nullable int directly, we know it's not 0 here
                                    HasSerial = true,
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
                    }
                    // For non-serializable items
                    // Create batch only if quantity is greater than 0
                    else if (itemDto.Quantity > 0)
                    {
                        var batchEntity = new Batch
                        {
                            BatchDate = addReceivedTransferDto.RecievedDate,
                            NumberOfItems = itemDto.Quantity,
                            PricelistId = itemDto.ReceiverPricelistId ?? 0, // Use the non-nullable int directly, we know it's not 0 here
                            HasSerial = false,
                            SerialNumbers = new List<SerialNumber>()
                        };
                        dbContext.Batches.Add(batchEntity);
                    }
                }
                // If itemDto.ReceiverPricelistId is 0, no batch will be created for this item or its serials.
                // The serial status in the ReceivedTransfer entity will be "Unrecorded".
            }

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while saving data. See server logs for details.", error = ex.Message });
            }
            return CreatedAtAction(nameof(GetReceivedTransferById), new { id = receivedTransferEntity.Id }, receivedTransferEntity);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateReceivedTransfer(int id, [FromBody] UpdateReceivedTransferDto updateReceivedTransferDto)
        {
            var existingReceivedTransfer = await dbContext.ReceivedTransfers
           .Include(ct => ct.Items!)
           .ThenInclude(cti => cti.SerialNumbers!)
           .FirstOrDefaultAsync(ct => ct.Id == id);

            if (existingReceivedTransfer == null)
            {
                return NotFound();
            }
            existingReceivedTransfer.TransferId = updateReceivedTransferDto.TransferId;
            existingReceivedTransfer.FromLocation = updateReceivedTransferDto.FromLocation;
            existingReceivedTransfer.ToLocation = updateReceivedTransferDto.ToLocation;
            existingReceivedTransfer.Status = updateReceivedTransferDto.Status;
            existingReceivedTransfer.ReleaseBy = updateReceivedTransferDto.ReleaseBy;
            existingReceivedTransfer.ReceiveBy = updateReceivedTransferDto.ReceiveBy;
            existingReceivedTransfer.TransferredDate = updateReceivedTransferDto.TransferredDate;
            existingReceivedTransfer.RecievedDate = updateReceivedTransferDto.RecievedDate;

            // Clear existing items and their serial numbers if they exist
            if (existingReceivedTransfer.Items != null)
            {
                foreach (var existingItem in existingReceivedTransfer.Items.ToList())
                {
                    if (existingItem.SerialNumbers != null)
                    {
                        dbContext.ReceivedTransferSerials.RemoveRange(existingItem.SerialNumbers);
                    }
                    dbContext.ReceivedTransferItems.Remove(existingItem);
                }
                existingReceivedTransfer.Items.Clear();
            }
            else
            {
                existingReceivedTransfer.Items = new List<ReceivedTransferItems>();
            }


            // Add new or updated items and their serial numbers
            if (updateReceivedTransferDto.Items != null)
            {
                foreach (var itemDto in updateReceivedTransferDto.Items)
                {
                    var newItem = new ReceivedTransferItems
                    {
                        ReceiverPricelistId = itemDto.ReceiverPricelistId,
                        PricelistId = itemDto.PricelistId,
                        Quantity = itemDto.Quantity,
                        SerialNumbers = itemDto.SerialNumbers?.Select(serialDto => new ReceivedTransferSerial
                        {
                            SerialNumberId = serialDto.SerialNumberId,
                            Status = serialDto.Status
                        }).ToList()
                    };
                    existingReceivedTransfer.Items.Add(newItem);
                }
            }

            await dbContext.SaveChangesAsync();

            return Ok(existingReceivedTransfer);
        }


        [HttpPut("{receivedTransferId}/items/{receivedTransferItemId}/serials/{receivedTransferSerialId}/receive")]
        public async Task<IActionResult> UpdateSerialDataOnReceive(
int receivedTransferId,        // From URL: ID of the ReceivedTransfer
int receivedTransferItemId,    // From URL: ID of the ReceivedTransferItem within the transfer
int receivedTransferSerialId,  // From URL: ID of the ReceivedTransferSerial to update status
[FromBody] UpdateSerialOnReceiveRequest dto) // From Request Body: contains SerialNumberValue
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // --- Part 1: Update the status of the ReceivedTransferSerial record ---
            var receivedTransfer = await dbContext.ReceivedTransfers
                .Include(ct => ct.Items!) // Ensure Items is not null
                    .ThenInclude(cti => cti.SerialNumbers!) // Ensure SerialNumbers is not null for each item
                .FirstOrDefaultAsync(ct => ct.Id == receivedTransferId);

            if (receivedTransfer == null)
            {
                return NotFound(new { message = $"Received Transfer with ID {receivedTransferId} not found." });
            }

            var receivedItem = receivedTransfer.Items?
                .FirstOrDefault(i => i.Id == receivedTransferItemId);

            if (receivedItem == null)
            {
                return NotFound(new { message = $"Received Transfer Item with ID {receivedTransferItemId} in Transfer {receivedTransferId} not found." });
            }

            // Find the specific ReceivedTransferSerial by its PK (receivedTransferSerialId)
            // and also verify its linked SerialNumberId matches the one from the DTO (dto.SerialNumberValue).
            // This assumes ReceivedTransferSerial has an integer property 'SerialNumberId' linking to the main SerialNumber.
            var receivedSerialToUpdate = receivedItem.SerialNumbers?
                .FirstOrDefault(s => s.Id == receivedTransferSerialId && s.SerialNumberId == dto.SerialNumberValue);

            if (receivedSerialToUpdate == null)
            {
                return NotFound(new { message = $"ReceivedTransferSerial with ID {receivedTransferSerialId} and associated main SerialNumber ID {dto.SerialNumberValue} not found within Item {receivedTransferItemId}." });
            }

            receivedSerialToUpdate.Status = "Received"; // Update status as requested

            // --- Part 2: Find the main SerialNumber entity and update its IsSold status ---
            // 'dto.SerialNumberValue' is the PK of this main SerialNumber entity.
            // This assumes 'SerialNumbers' is the DbSet for your main serial number entities.
            var mainSerialNumberEntity = await dbContext.SerialNumbers
                .FirstOrDefaultAsync(s => s.Id == dto.SerialNumberValue);

            if (mainSerialNumberEntity == null)
            {
                // This implies an inconsistency if ReceivedTransferSerial references a non-existent main SerialNumber.
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
                message = $"Successfully updated status for ReceivedTransferSerial ID {receivedTransferSerialId} to 'Received', and set IsSold to false for main SerialNumber ID {dto.SerialNumberValue}.",
                updatedReceivedTransferSerial = new { receivedSerialToUpdate.Id, receivedSerialToUpdate.SerialNumberId, receivedSerialToUpdate.Status },
                updatedMainSerialNumber = new { mainSerialNumberEntity.Id, mainSerialNumberEntity.SerialName, mainSerialNumberEntity.IsSold }
            });
        }

        [HttpPost("{receivedTransferId}/items/{receivedTransferItemId}/serials/markFoundInToLocation")]
        public async Task<IActionResult> MarkSerialAsFoundInToLocation(
        int receivedTransferId,
        int receivedTransferItemId,
        [FromBody] MarkSerialFoundInToLocation dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // --- Part 1: Find and Update ReceivedTransferSerial ---
            var receivedTransfer = await dbContext.ReceivedTransfers
                .Include(ct => ct.Items!)
                    .ThenInclude(item => item.SerialNumbers!) // Load ReceivedTransferSerials
                .FirstOrDefaultAsync(ct => ct.Id == receivedTransferId);

            if (receivedTransfer == null)
            {
                return NotFound(new { message = $"Received Transfer with ID {receivedTransferId} not found." });
            }

            var receivedItem = receivedTransfer.Items?
                .FirstOrDefault(i => i.Id == receivedTransferItemId);

            if (receivedItem == null)
            {
                return NotFound(new { message = $"Received Transfer Item with ID {receivedTransferItemId} in Transfer {receivedTransferId} not found." });
            }

            // Find the specific ReceivedTransferSerial by its PK (dto.ReceivedTransferSerialId)

            var receivedSerialToUpdate = receivedItem.SerialNumbers?
                .FirstOrDefault(s => s.Id == dto.CompletedTransferSerialId && s.SerialNumberId == dto.SerialNumberValue);

            if (receivedSerialToUpdate == null)
            {
                return NotFound(new { message = $"ReceivedTransferSerial with ID {dto.CompletedTransferSerialId} and associated main SerialNumber ID {dto.SerialNumberValue} not found within Item {receivedTransferItemId}." });
            }

            if (receivedSerialToUpdate.Status == "Received")
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
            // This is the SerialName we need, similar to serialDto.SerialName in your AddReceivedTransfer
            string serialNameForNewBatchEntry = mainSerialNumberEntity.SerialName;

            // Update status of the ReceivedTransferSerial
            receivedSerialToUpdate.Status = "Received";

            // --- Part 3: Create a new Batch and SerialNumber entry (imitating POST logic) ---
            if (receivedItem.ReceiverPricelistId == null)
            {
                // ReceiverPricelistId is needed for the new Batch.
                return BadRequest(new { message = $"ReceiverPricelistId is missing for item ID {receivedItem.Id}. Cannot create batch." });
            }

            var batchEntity = new Batch
            {
                BatchDate = receivedTransfer.RecievedDate, // Or DateTime.UtcNow if preferred for "found" date
                NumberOfItems = 1, // For this single serial being marked as found
                PricelistId = receivedItem.ReceiverPricelistId.Value,
                HasSerial = true, // This is a serialized item
                SerialNumbers = new List<SerialNumber>
                {
                    new SerialNumber // Create a new SerialNumber record for this batch
                    {
                        SerialName = serialNameForNewBatchEntry, // Use the fetched name
                        IsSold = false                              // As per requirement
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
                updatedReceivedSerial = new { receivedSerialToUpdate.Id, receivedSerialToUpdate.SerialNumberId, receivedSerialToUpdate.Status },
                newBatchDetails = new { batchEntity.Id, batchEntity.PricelistId, SerialNameInBatch = serialNameForNewBatchEntry }
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteReceivedTransfer(int id)
        {
            var receivedTransfer = await dbContext.ReceivedTransfers
                .Include(ct => ct.Items!)
                .ThenInclude(cti => cti.SerialNumbers!)
                .FirstOrDefaultAsync(ct => ct.Id == id);

            if (receivedTransfer == null)
            {
                return NotFound();
            }

            // Safely remove children first if they exist
            if (receivedTransfer.Items != null)
            {
                dbContext.ReceivedTransferSerials.RemoveRange(
                    receivedTransfer.Items.Where(item => item.SerialNumbers != null)
                                            .SelectMany(item => item.SerialNumbers!));
                dbContext.ReceivedTransferItems.RemoveRange(receivedTransfer.Items);
            }

            dbContext.ReceivedTransfers.Remove(receivedTransfer);
            await dbContext.SaveChangesAsync();

            return Ok(receivedTransfer);
        }
    }
}