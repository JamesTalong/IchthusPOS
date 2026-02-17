using IchthusPOSWeb.Data;
using IchthusPOSWeb.Migrations;
using IchthusPOSWeb.Models;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Security.Cryptography.Xml;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TransfersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET all transfers
        [HttpGet]
        public IActionResult GetAllTransfers()
        {
            var allTransfers = dbContext.Transfers
        .Include(t => t.Items)
            .ThenInclude(ti => ti.Pricelist)
                .ThenInclude(pl => pl.Product)
        .Include(t => t.Items)
            .ThenInclude(ti => ti.Pricelist)
                .ThenInclude(pl => pl.Location) // ✅ use Pricelist.Location instead of Product.Location
        .Include(t => t.Items)
            .ThenInclude(ti => ti.Pricelist)
                .ThenInclude(pl => pl.Color)
        .Include(t => t.Items)
            .ThenInclude(ti => ti.Pricelist)
                .ThenInclude(pl => pl.Batches)
                    .ThenInclude(b => b.SerialNumbers)
        .OrderByDescending(t => t.TransferredDate)
        .AsEnumerable()
        .Select(t => new
        {
                    t.Id, 
                    t.FromLocation,
                    t.ToLocation,   
                    t.TransferredDate,
                    t.Status,
                    t.ReceiveBy,
                    t.ReleaseBy,

                    Items = t.Items.Select(item => new
                    {
                        item.Id,
                        item.PricelistId,
                        item.Quantity,
                        item.ReceiverPricelistId,
                      
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
                                item.Pricelist.LocationId,
                                LocationName = item.Pricelist.Location != null ? item.Pricelist.Location.LocationName : null,
                            } : null,
               

                            Color = item.Pricelist.Color != null ? new 
                            {
                                item.Pricelist.Color.Id,
                                item.Pricelist.Color.ColorName,
                               
                            } : null,

                            item.Pricelist.VatEx,
                            item.Pricelist.VatInc,
                            item.Pricelist.Reseller,
                            SerialNumbers = item.Pricelist.Batches
                                                .SelectMany(b => b.SerialNumbers)
                                                .Where(sn => item.SerialNumberIds != null && item.SerialNumberIds.Contains(sn.Id)) 
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
                .ToList(); 

            return Ok(allTransfers);
        }

        // GET transfer by ID
        [HttpGet("{id}")]
        public IActionResult GetTransferById(int id)
        {
            var transfer = dbContext.Transfers
                .Include(t => t.Items)
                .FirstOrDefault(t => t.Id == id);

            if (transfer == null)
            {
                return NotFound();
            }

            return Ok(transfer);
        }

        [HttpPost("revert/{id}")]
        public async Task<IActionResult> RevertTransfer(int id)
        {
            var transfer = await dbContext.Transfers
                .Include(t => t.Items)  // Make sure to include the related items
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transfer == null)
            {
                return NotFound(new { message = $"Transfer with ID {id} not found." });
            }

            // Get all serial number IDs related to the transfer
            var serialIdsToRevert = transfer.Items
                .Where(item => item.SerialNumberIds != null && item.SerialNumberIds.Any())
                .SelectMany(item => item.SerialNumberIds)
                .Distinct() // Add Distinct() to prevent duplicate updates.
                .ToList();

            // Fetch the serial numbers directly by their IDs
            var serials = dbContext.SerialNumbers
                .AsEnumerable()
                .Where(i => serialIdsToRevert.Contains(i.Id))
                .ToList();

            // Update IsSold
            foreach (var serial in serials)
            {
                serial.IsSold = false;
            }

            try
            {
                // Save the changes to mark serials as not sold
                await dbContext.SaveChangesAsync();

                // Now, manually delete all related TransferItems using the 'Items' collection from the 'transfer'
                foreach (var item in transfer.Items)
                {
                    dbContext.TransferItems.Remove(item);  // Remove each item individually (if DbSet is available for TransferItem)
                }

                // Now, delete the transfer after marking serials as not sold
                dbContext.Transfers.Remove(transfer);
                await dbContext.SaveChangesAsync();

                return Ok(new { message = $"Transfer ID {id} successfully reverted and deleted. All related serials are now marked as not sold." });
            }
            catch (Exception ex)
            {
                // Log the exception details, especially the inner exception and the SQL command if possible.
                Console.WriteLine($"Error reverting transfer: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { message = "Failed to revert transfer.", error = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SubmitTransfer(AddTransferDto addTransferDto)
        {
            IExecutionStrategy strategy = dbContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using IDbContextTransaction dbTransaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    var transferEntity = new Transfer()
                    {
                        ReleaseBy = addTransferDto.ReleaseBy,
                        Status = addTransferDto.Status,
                        FromLocation = addTransferDto.FromLocation,
                        ToLocation = addTransferDto.ToLocation,
                        TransferredDate = addTransferDto.TransferredDate,
                        ReceiveBy = addTransferDto.ReceiveBy,
                        Items = new List<TransferItem>()
                    };

                    var errorDetails = new List<object>();

                    foreach (var itemDto in addTransferDto.Items)
                    {
                        // --- VALIDATE QUANTITY ---
                        if (!itemDto.Quantity.HasValue || itemDto.Quantity.Value <= 0)
                        {
                            errorDetails.Add(new { PricelistId = itemDto.PricelistId, Message = "Item quantity must be a positive number." });
                            continue; // Skip this item if quantity is invalid
                        }
                        int currentItemQuantity = itemDto.Quantity.Value; // Use this non-nullable int moving forward

                        var pricelist = await dbContext.Pricelists
                            .Include(p => p.Batches)
                                .ThenInclude(b => b.SerialNumbers)
                            .FirstOrDefaultAsync(p => p.Id == itemDto.PricelistId);

                        if (pricelist == null)
                        {
                            errorDetails.Add(new { PricelistId = itemDto.PricelistId, Message = "Pricelist not found." });
                            continue;
                        }

                        var transferItemEntity = new TransferItem()
                        {
                            PricelistId = itemDto.PricelistId,
                            ReceiverPricelistId = itemDto.ReceiverPricelistId, // Add this line
                            ProductId = itemDto.ProductId,
                            Quantity = currentItemQuantity, // Assign validated non-nullable quantity
                            SerialNumberIds = new List<int>()
                        };

                        List<SerialNumber> serialsToMarkAsSold = new List<SerialNumber>();
                        bool currentItemHasErrors = false;

                        if (itemDto.SerialNumberIds != null && itemDto.SerialNumberIds.Any())
                        {
                            if (itemDto.SerialNumberIds.Count != currentItemQuantity)
                            {
                                errorDetails.Add(new
                                {
                                    PricelistId = itemDto.PricelistId,
                                    Message = $"Mismatch: Quantity ({currentItemQuantity}) does not match the number of provided SerialNumberIds ({itemDto.SerialNumberIds.Count})."
                                });
                                currentItemHasErrors = true;
                            }
                            else // Counts match, proceed to check individual serials
                            {
                                foreach (var serialId in itemDto.SerialNumberIds)
                                {
                                    var serial = pricelist.Batches
                                        .SelectMany(b => b.SerialNumbers)
                                        .FirstOrDefault(s => s.Id == serialId);

                                    if (serial == null)
                                    {
                                        errorDetails.Add(new { PricelistId = itemDto.PricelistId, SerialId = serialId, Message = "Serial number not found for this pricelist." });
                                        currentItemHasErrors = true;
                                    }
                                    else if (serial.IsSold)
                                    {
                                        errorDetails.Add(new { PricelistId = itemDto.PricelistId, SerialId = serialId, SerialName = serial.SerialName, Message = "Serial number is already sold." });
                                        currentItemHasErrors = true;
                                    }
                                    else
                                    {
                                        serialsToMarkAsSold.Add(serial); // Add valid serials
                                    }
                                }
                                // If any error occurred during the loop, serialsToMarkAsSold count might not match currentItemQuantity
                                if (serialsToMarkAsSold.Count != currentItemQuantity && !currentItemHasErrors)
                                {
                                    // This case implies some serials were fine, but not all. Error should have been flagged.
                                    // This check might be redundant if currentItemHasErrors is set correctly above.
                                    currentItemHasErrors = true;
                                }
                            }
                        }
                        else // Auto-select random unsold serials
                        {
                            var availableUnsoldSerials = pricelist.Batches
                                .SelectMany(b => b.SerialNumbers)
                                .Where(sn => !sn.IsSold)
                                .ToList();

                            if (availableUnsoldSerials.Count < currentItemQuantity)
                            {
                                errorDetails.Add(new
                                {
                                    PricelistId = itemDto.PricelistId,
                                    Message = $"Insufficient stock. Required: {currentItemQuantity}, Available Unsold: {availableUnsoldSerials.Count}."
                                });
                                currentItemHasErrors = true;
                            }
                            else
                            {
                                serialsToMarkAsSold.AddRange(
                                    availableUnsoldSerials
                                        .OrderBy(x => Guid.NewGuid())
                                        .Take(currentItemQuantity) // Use non-nullable quantity
                                        .ToList()
                                );
                            }
                        }

                        if (!currentItemHasErrors && serialsToMarkAsSold.Count == currentItemQuantity)
                        {
                            foreach (var serial in serialsToMarkAsSold)
                            {
                                serial.IsSold = true;
                                transferItemEntity.SerialNumberIds.Add(serial.Id);
                            }
                            transferEntity.Items.Add(transferItemEntity);
                        }
                        // If currentItemHasErrors is true, or counts don't match, this item is skipped,
                        // and errors are already in errorDetails.
                    }

                    if (errorDetails.Any())
                    {
                        await dbTransaction.RollbackAsync();
                        return BadRequest(new { message = "Transfer submission failed with errors.", errors = errorDetails });
                    }

                    if (!transferEntity.Items.Any() && addTransferDto.Items.Any())
                    {
                        await dbTransaction.RollbackAsync();
                        return BadRequest(new { message = "No valid items to transfer due to errors.", errors = errorDetails.Any() ? errorDetails : new List<object> { "All items had processing errors." } });
                    }

                    dbContext.Transfers.Add(transferEntity);
                    await dbContext.SaveChangesAsync();
                    await dbTransaction.CommitAsync();

                    return Ok(new { message = "Transfer submitted successfully.", transferId = transferEntity.Id });
                }
                catch (Exception ex)
                {
                    await dbTransaction.RollbackAsync();
                    return StatusCode(500, new { message = "An unexpected error occurred during transfer submission.", error = ex.Message });
                }
            });
        }


        // PUT to update an existing transfer
        [HttpPut("{id}")]
        public IActionResult UpdateTransfer(int id, UpdateTransferDto updateTransferDto)
        {
            var transfer = dbContext.Transfers
                .Include(t => t.Items)
                .FirstOrDefault(t => t.Id == id);

            if (transfer == null)
            {
                return NotFound();
            }

            transfer.ReceiveBy = updateTransferDto.ReceiveBy;
            transfer.ReleaseBy = updateTransferDto.ReleaseBy;
            transfer.Status = updateTransferDto.Status;
            transfer.FromLocation = updateTransferDto.FromLocation;
            transfer.ToLocation = updateTransferDto.ToLocation;
            transfer.TransferredDate = updateTransferDto.TransferredDate;

            // Update items
            transfer.Items.Clear();
            transfer.Items.AddRange(updateTransferDto.Items.Select(item => new TransferItem()
            {
                PricelistId = item.PricelistId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                SerialNumberIds = item.SerialNumberIds
            }).ToList());

            dbContext.SaveChanges();

            return Ok(transfer);
        }

        // DELETE transfer by ID
        [HttpDelete("{id}")]
        public IActionResult DeleteTransfer(int id)
        {
            var transfer = dbContext.Transfers
                .Include(t => t.Items)
                .FirstOrDefault(t => t.Id == id);

            if (transfer == null)
            {
                return NotFound();
            }

            dbContext.Transfers.Remove(transfer);
            dbContext.SaveChanges();

            return Ok(transfer);
        }
    }
}
