using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models;
using IchthusPOSWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            var allSerialNumbers = _dbContext.SerialNumbers.ToList();
            var transactions = _dbContext.Transactions
               .Include(t => t.PurchasedProducts)
               .ThenInclude(p => p.SerialNumbers)
               .Include(t => t.Customer)
               .Include(t => t.Location) // Include Location
               .AsEnumerable() // ✅ Force EF to switch to in-memory processing
               .Select(t => new
               {
                   t.Id,
                   t.Date,
                   t.IsVoid,
                   t.VoidBy,
                   t.Terms,
                   t.PreparedBy,
                   t.CheckedBy,
                   t.UserId,
                   t.FullName,
                   t.Payment,
                   t.PaymentType,
                   t.DiscountType,
                   t.DiscountAmount,
                   t.TotalItems,
                   t.TotalAmount,
                   t.Change,
                   LocationId = t.LocationId,
                   Location = t.Location.LocationName,
                   Customer = new
                   {
                       Id = t.Customer.Id,
                       t.Customer.CustomerName,
                       t.Customer.Address,
                       t.Customer.TinNumber,
                       t.Customer.MobileNumber,
                       t.Customer.BusinessStyle,
                       t.Customer.RFID,
                       t.Customer.CustomerType
                   },
                   PurchasedProducts = t.PurchasedProducts.Select(p => new
                   {
                       p.Id,
                       p.ProductId,
                       p.Quantity,
                       p.Price,
                       p.Subtotal,
                       p.VatType,
                       p.DiscountValue,
                       SerialNumbers = p.SerialNumbers
                   .SelectMany(s => allSerialNumbers
                       .Where(sn => s.SerialNumbers.Contains(sn.Id)) // Now processed in memory
                       .Select(sn => new
                       {
                           Id = sn.Id,
                           SerialName = sn.SerialName,
                           IsSold = sn.IsSold,
                           BatchId = sn.BatchId
                       })
                   ).ToList(),

                       // Fetch product details from Pricelist and Product
                       Pricelist = _dbContext.Pricelists
                           .Where(pl => pl.Id == p.ProductId)
                           .Select(pl => new
                           {
                               BarCode = pl.Product.BarCode,
                               ItemCode = pl.Product.ItemCode,
                               ProductName = pl.Product.ProductName,
                           })
                           .FirstOrDefault()
                   }).ToList()
               })
               .OrderByDescending(p => p.Id)
                .ToList();

            return Ok(transactions);
        }

        [HttpGet("UserId/{userId}")] // New endpoint for specific UserId
        public IActionResult GetTransactionsByUserId(string userId)
        {
            // Start with the base query, similar to GetAllTransactions
            var allSerialNumbers = _dbContext.SerialNumbers.ToList();
            var transactions = _dbContext.Transactions
               .Include(t => t.PurchasedProducts)
               .ThenInclude(p => p.SerialNumbers)
               .Include(t => t.Customer)
               .Include(t => t.Location) // Include Location
               .Where(t => t.UserId == userId) // <--- IMPORTANT: Filter by UserId here
               .AsEnumerable() // ✅ Force EF to switch to in-memory processing
               .Select(t => new
               {
                   t.Id,
                   t.Date,
                   t.IsVoid,
                   t.VoidBy,
                   t.Terms,
                   t.PreparedBy,
                   t.CheckedBy,
                   t.UserId,
                   t.FullName,
                   t.Payment,
                   t.PaymentType,
                   t.DiscountType,
                   t.DiscountAmount,
                   t.TotalItems,
                   t.TotalAmount,
                   t.Change,
                   LocationId = t.LocationId,
                   Location = t.Location.LocationName,
                   Customer = new
                   {
                       Id = t.Customer.Id,
                       t.Customer.CustomerName,
                       t.Customer.Address,
                       t.Customer.TinNumber,
                       t.Customer.MobileNumber,
                       t.Customer.BusinessStyle,
                       t.Customer.RFID,
                       t.Customer.CustomerType
                   },
                   PurchasedProducts = t.PurchasedProducts.Select(p => new
                   {
                       p.Id,
                       p.ProductId,
                       p.Quantity,
                       p.Price,
                       p.Subtotal,
                       p.VatType,
                       p.DiscountValue,
                       SerialNumbers = p.SerialNumbers
                       .SelectMany(s => allSerialNumbers
                           .Where(sn => s.SerialNumbers.Contains(sn.Id)) // Now processed in memory
                           .Select(sn => new
                           {
                               Id = sn.Id,
                               SerialName = sn.SerialName,
                               IsSold = sn.IsSold,
                               BatchId = sn.BatchId
                           })
                       ).ToList(),

                       // Fetch product details from Pricelist and Product
                       Pricelist = _dbContext.Pricelists
                           .Where(pl => pl.Id == p.ProductId)
                           .Select(pl => new
                           {
                               BarCode = pl.Product.BarCode,
                               ItemCode = pl.Product.ItemCode,
                               ProductName = pl.Product.ProductName,
                           })
                           .FirstOrDefault()
                   }).ToList()
               })
                .OrderByDescending(p => p.Id)
                .ToList();

            if (!transactions.Any())
            {
                return NotFound($"No transactions found for UserId: {userId}");
            }

            return Ok(transactions);
        }
    

    [HttpGet("ByLocation/{locationId}")]
        public IActionResult GetTransactionsByLocation(int locationId)
        {
            var allSerialNumbers = _dbContext.SerialNumbers.ToList();
            var transactions = _dbContext.Transactions
                .Where(t => t.LocationId == locationId) // Filter by LocationId
                .Include(t => t.PurchasedProducts)
                    .ThenInclude(p => p.SerialNumbers)
                .Include(t => t.Customer)
                .Include(t => t.Location) // Include Location
                .AsEnumerable() // ✅ Force EF to switch to in-memory processing
                .Select(t => new
                {
                    t.Id,
                    t.Date,
                    t.IsVoid,
                    t.VoidBy,
                    t.Terms,
                    t.PreparedBy,
                    t.CheckedBy,
                    t.UserId,
                    t.FullName,
                    t.Payment,
                    t.PaymentType,
                    t.DiscountType,
                    t.DiscountAmount,
                    t.TotalItems,
                    t.TotalAmount,
                    t.Change,
                    LocationId = t.LocationId,
                    Location = t.Location.LocationName,
                    Customer = new
                    {
                        Id = t.Customer.Id,
                        t.Customer.CustomerName,
                        t.Customer.Address,
                        t.Customer.TinNumber,
                        t.Customer.MobileNumber,
                        t.Customer.BusinessStyle,
                        t.Customer.RFID,
                        t.Customer.CustomerType
                    },
                    PurchasedProducts = t.PurchasedProducts.Select(p => new
                    {
                        p.Id,
                        p.ProductId,
                        p.Quantity,
                        p.Price,
                        p.Subtotal,
                        p.VatType,
                        p.DiscountValue,
                        SerialNumbers = p.SerialNumbers
                    .SelectMany(s => allSerialNumbers
                        .Where(sn => s.SerialNumbers.Contains(sn.Id)) // Now processed in memory
                        .Select(sn => new
                        {
                            Id = sn.Id,
                            SerialName = sn.SerialName,
                            IsSold = sn.IsSold,
                            BatchId = sn.BatchId
                        })
                    ).ToList(),

                        // Fetch product details from Pricelist and Product
                        Pricelist = _dbContext.Pricelists
                            .Where(pl => pl.Id == p.ProductId)
                            .Select(pl => new
                            {
                                BarCode = pl.Product.BarCode,
                                ItemCode = pl.Product.ItemCode,
                                ProductName = pl.Product.ProductName,
                            })
                            .FirstOrDefault()
                    }).ToList()
                })
                .OrderByDescending(p => p.Id)
                 .ToList();

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public IActionResult GetTransactionById(int id)
        {
            var transaction = _dbContext.Transactions
                .Include(t => t.PurchasedProducts)
                .ThenInclude(p => p.SerialNumbers)
                .Include(t => t.Customer)
                .Include(t => t.Location)
                .FirstOrDefault(t => t.Id == id);

            if (transaction == null)
            {
                return NotFound(); // Return 404 if the transaction is not found
            }

            var allSerialNumbers = _dbContext.SerialNumbers.ToList(); // Fetch all serial numbers once

            var result = new
            {
                transaction.Id,
                transaction.Date,
                transaction.IsVoid,
                transaction.VoidBy,
                transaction.Terms,
                transaction.PreparedBy,
                transaction.CheckedBy,
                transaction.UserId,
                transaction.FullName,
                transaction.Payment,
                transaction.PaymentType,
                transaction.DiscountType,
                transaction.DiscountAmount,
                transaction.TotalItems,
                transaction.TotalAmount,
                transaction.Change,
                LocationId = transaction.LocationId,
                Location = transaction.Location.LocationName,
                Customer = new
                {
                    Id = transaction.Customer.Id,
                    transaction.Customer.CustomerName,
                    transaction.Customer.Address,
                    transaction.Customer.TinNumber,
                    transaction.Customer.MobileNumber,
                    transaction.Customer.BusinessStyle,
                    transaction.Customer.RFID,
                    transaction.Customer.CustomerType
                },
                PurchasedProducts = transaction.PurchasedProducts.Select(p => new
                {
                    p.Id,
                    p.ProductId,
                    p.Quantity,
                    p.Price,
                    p.Subtotal,
                    p.VatType,
                    p.DiscountValue,
                    SerialNumbers = p.SerialNumbers
                        .SelectMany(s => allSerialNumbers
                            .Where(sn => s.SerialNumbers.Contains(sn.Id))
                            .Select(sn => new
                            {
                                Id = sn.Id,
                                SerialName = sn.SerialName,
                                IsSold = sn.IsSold,
                                BatchId = sn.BatchId
                            })
                        ).ToList(),
                    Pricelist = _dbContext.Pricelists
                        .Where(pl => pl.Id == p.ProductId)
                        .Select(pl => new
                        {
                            ProductImage = pl.Product.ProductImage,
                            BarCode = pl.Product.BarCode,
                            ItemCode = pl.Product.ItemCode,
                            ProductName = pl.Product.ProductName,
                            Location = pl.Location != null ? pl.Location.LocationName : null,
                            Color = pl.Color != null ? pl.Color.ColorName : null,
                            BrandName = pl.Product.Brand != null ? pl.Product.Brand.BrandName : null,
                            CategoryName = pl.Product.Category != null ? pl.Product.Category.CategoryName : null,
                            CategoryTwoName = pl.Product.CategoryTwo != null ? pl.Product.CategoryTwo.CategoryTwoName : null,
                            CategoryThreeName = pl.Product.CategoryThree != null ? pl.Product.CategoryThree.CategoryThreeName : null,
                            CategoryFourName = pl.Product.CategoryFour != null ? pl.Product.CategoryFour.CategoryFourName : null,
                            CategoryFiveName = pl.Product.CategoryFive != null ? pl.Product.CategoryFive.CategoryFiveName : null
                        })
                        .FirstOrDefault()
                }).ToList()
            };

            return Ok(result);
        }

        [HttpGet("top-products")]
        public IActionResult GetTopProducts()
        {
            var topProducts = (from p in _dbContext.PurchasedProducts
                               join pl in _dbContext.Pricelists on p.ProductId equals pl.Id
                               join product in _dbContext.Products on pl.ProductId equals product.Id
                               group p by new
                               {
                                   ProductId = product.Id,
                                   product.ProductName,
                                   product.ProductImage
                               } into g
                               orderby g.Sum(p => p.Quantity) descending
                               select new
                               {
                                   ProductId = g.Key.ProductId,
                                   ProductName = g.Key.ProductName,
                                   ProductImage = g.Key.ProductImage,
                                   TotalQuantity = g.Sum(p => p.Quantity)
                               })
                              .Take(10)
                              .ToList();

            return Ok(topProducts);
        }
        [HttpPost("revert/{id}")]
        public IActionResult RevertTransaction(int id, [FromBody] AddRevertTransactionDto dto)
        {
            var transaction = _dbContext.Transactions
                .Include(t => t.PurchasedProducts)
                    .ThenInclude(p => p.SerialNumbers)
                .FirstOrDefault(t => t.Id == id);

            if (transaction == null)
            {
                return NotFound(new { message = "Transaction not found" });
            }

            // Ensure that the transaction isn't already voided
            if (transaction.IsVoid == true)
            {
                return BadRequest(new { message = "Transaction is already voided" });
            }

            // Create a list to store serial numbers that were marked unsold
            List<int> unsoldSerials = new List<int>();

            // Create execution strategy to handle retries
            var strategy = _dbContext.Database.CreateExecutionStrategy();

            // Variable to hold the final result to return
            IActionResult result = null;

            // Execute the transaction logic inside the retry strategy
            strategy.Execute(() =>
            {
                using var transactionDb = _dbContext.Database.BeginTransaction();

                try
                {
                    // Loop through each purchased product to mark its serial numbers as unsold
                    foreach (var purchasedProduct in transaction.PurchasedProducts)
                    {
                        foreach (var serial in purchasedProduct.SerialNumbers[0].SerialNumbers)
                        {
                            var serialNumber = _dbContext.SerialNumbers
                                .FirstOrDefault(s => s.Id == serial);

                            if (serialNumber != null && serialNumber.IsSold)
                            {
                                // Mark the serial number as unsold (IsSold = false)
                                serialNumber.IsSold = false;
                                unsoldSerials.Add(serialNumber.Id); // Track this serial

                                // Attach the entity to the context and mark it as modified
                                _dbContext.SerialNumbers.Attach(serialNumber);
                                _dbContext.Entry(serialNumber).State = EntityState.Modified;
                            }
                        }
                    }

                    // Mark the transaction as void
                    transaction.IsVoid = true;
                    transaction.VoidBy = dto.VoidBy;
            

                    // Attach and mark the transaction as modified
                    _dbContext.Transactions.Attach(transaction);
                    _dbContext.Entry(transaction).State = EntityState.Modified;

                    // Commit changes to the database
                    _dbContext.SaveChanges();

                    // Commit the transaction
                    transactionDb.Commit();

                    // Store the successful result
                    result = Ok(new { message = "Transaction successfully reverted", unsoldSerials });
                }
                catch (Exception ex)
                {
                    // Rollback the database transaction in case of error
                    transactionDb.Rollback();
                    result = BadRequest(new { message = "Error during revert", error = ex.Message });
                }
            });

            // Return the result after the strategy execution
            return result;
        }







        [HttpPost]
        public IActionResult AddTransaction(AddTransactionDto addTransactionDto)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            return strategy.Execute(() => ExecuteTransaction(addTransactionDto));
        }

        private IActionResult ExecuteTransaction(AddTransactionDto addTransactionDto)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var transactionEntity = new Transaction
                {
                    Date = addTransactionDto.Date,
                    IsVoid = addTransactionDto.IsVoid,
                    VoidBy = addTransactionDto.VoidBy,
                    Terms = addTransactionDto.Terms,
                    CustomerId = addTransactionDto.CustomerId,
                    PreparedBy = addTransactionDto.PreparedBy,
                    CheckedBy = addTransactionDto.CheckedBy,
                    UserId = addTransactionDto.UserId,
                    FullName = addTransactionDto.FullName,
                    Payment = addTransactionDto.Payment,
                    PaymentType = addTransactionDto.PaymentType,
                    DiscountType = addTransactionDto.DiscountType,
                    DiscountAmount = addTransactionDto.DiscountAmount,
                    TotalItems = addTransactionDto.TotalItems,
                    TotalAmount = addTransactionDto.TotalAmount,
                    Change = addTransactionDto.Change,
                    LocationId = addTransactionDto.LocationId,
                    PurchasedProducts = new List<PurchasedProduct>()
                };

                List<int> alreadySoldSerials = new();
                List<int> insufficientStockWarnings = new();

                foreach (var purchasedProduct in addTransactionDto.PurchasedProducts)
                {
                    var purchasedProductEntity = new PurchasedProduct
                    {
                        ProductId = purchasedProduct.ProductId,
                        Quantity = purchasedProduct.Quantity,
                        Price = purchasedProduct.Price,
                        Subtotal = purchasedProduct.Quantity * purchasedProduct.Price,
                        VatType = purchasedProduct.VatType,
                        DiscountValue = purchasedProduct.DiscountValue,
                        SerialNumbers = new List<SerialMain>()
                    };

                    var pricelistBatch = _dbContext.Pricelists
                        .Include(p => p.Batches)
                        .ThenInclude(b => b.SerialNumbers)
                        .FirstOrDefault(p => p.Id == purchasedProduct.ProductId);

                    if (pricelistBatch != null && pricelistBatch.Batches != null)
                    {
                        int quantityToMarkAsSold = purchasedProduct.Quantity ?? 0;
                        List<SerialNumber> soldSerials = new();

                        var serialBatches = pricelistBatch.Batches
                            .Where(b => b.HasSerial == true)
                            .OrderBy(b => b.BatchDate)
                            .ToList();

                        var nonSerialBatches = pricelistBatch.Batches
                            .Where(b => !b.HasSerial.GetValueOrDefault())
                            .OrderBy(b => b.BatchDate)
                            .ToList();

                        // Step 1: Use user-provided serials
                        if (purchasedProduct.SerialNumbers != null && purchasedProduct.SerialNumbers.Any())
                        {
                            foreach (var serialData in purchasedProduct.SerialNumbers)
                            {
                                foreach (var serialId in serialData.SerialNumbers)
                                {
                                    var serial = serialBatches
                                        .SelectMany(b => b.SerialNumbers)
                                        .FirstOrDefault(s => s.Id == serialId);

                                    if (serial != null)
                                    {
                                        if (serial.IsSold)
                                            alreadySoldSerials.Add(serial.Id);
                                        else
                                        {
                                            serial.IsSold = true;
                                            soldSerials.Add(serial);
                                            quantityToMarkAsSold--;
                                        }
                                    }
                                }
                            }
                        }

                        // Step 2: Auto-assign from serial batches
                        if (quantityToMarkAsSold > 0)
                        {
                            foreach (var batch in serialBatches)
                            {
                                foreach (var serial in batch.SerialNumbers.Where(s => !s.IsSold).Take(quantityToMarkAsSold))
                                {
                                    serial.IsSold = true;
                                    soldSerials.Add(serial);
                                    quantityToMarkAsSold--;
                                    if (quantityToMarkAsSold <= 0) break;
                                }
                                if (quantityToMarkAsSold <= 0) break;
                            }
                        }

                        // Step 3: Fallback to non-serial batches
                        if (quantityToMarkAsSold > 0)
                        {
                            foreach (var batch in nonSerialBatches)
                            {
                                foreach (var serial in batch.SerialNumbers.Where(s => !s.IsSold).Take(quantityToMarkAsSold))
                                {
                                    serial.IsSold = true;
                                    soldSerials.Add(serial);
                                    quantityToMarkAsSold--;
                                    if (quantityToMarkAsSold <= 0) break;
                                }
                                if (quantityToMarkAsSold <= 0) break;
                            }
                        }

                        if (quantityToMarkAsSold > 0)
                        {
                            insufficientStockWarnings.Add(purchasedProduct.ProductId ?? 0);
                        }

                        if (soldSerials.Any())
                        {
                            purchasedProductEntity.SerialNumbers.Add(new SerialMain
                            {
                                PricelistId = pricelistBatch.Id,
                                SerialNumbers = soldSerials.Select(s => s.Id).ToList()
                            });
                        }
                    }

                    transactionEntity.PurchasedProducts.Add(purchasedProductEntity);
                }

                if (alreadySoldSerials.Any())
                {
                    return BadRequest(new
                    {
                        message = "Transaction failed. Some serial numbers are already sold.",
                        alreadySoldSerials
                    });
                }

                if (insufficientStockWarnings.Any())
                {
                    return BadRequest(new
                    {
                        message = "Transaction warning. Not enough available stock for some products.",
                        insufficientStockWarnings
                    });
                }

                _dbContext.Transactions.Add(transactionEntity);
                _dbContext.SaveChanges();
                transaction.Commit();

                return Ok(transactionEntity);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return BadRequest(new { message = "Transaction failed", error = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateTransaction(int id, UpdateTransactionDto updateTransactionDto)
        {
            var transaction = _dbContext.Transactions
                .Include(t => t.PurchasedProducts)
                    .ThenInclude(p => p.SerialNumbers)
                .FirstOrDefault(t => t.Id == id);

            if (transaction == null)
            {
                return NotFound(new { message = "Transaction not found" });
            }

            transaction.Date = updateTransactionDto.Date;
            transaction.IsVoid = updateTransactionDto.IsVoid;
            transaction.VoidBy =    updateTransactionDto.VoidBy;
            transaction.Terms = updateTransactionDto.Terms;
            transaction.PreparedBy = updateTransactionDto.PreparedBy;
            transaction.CheckedBy = updateTransactionDto.CheckedBy;
            transaction.UserId = updateTransactionDto.UserId;
            transaction.FullName = updateTransactionDto.FullName;
            transaction.Payment = updateTransactionDto.Payment;
            transaction.PaymentType = updateTransactionDto.PaymentType;
            transaction.DiscountType = updateTransactionDto.DiscountType;
            transaction.DiscountAmount = updateTransactionDto.DiscountAmount;
            transaction.TotalItems = updateTransactionDto.TotalItems;
            transaction.TotalAmount = updateTransactionDto.TotalAmount;
            transaction.Change = updateTransactionDto.Change;
            transaction.LocationId = updateTransactionDto.LocationId;

            _dbContext.PurchasedProducts.RemoveRange(transaction.PurchasedProducts);

            transaction.PurchasedProducts = updateTransactionDto.PurchasedProducts.Select(p => new PurchasedProduct
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                Price = p.Price,
                Subtotal = p.Subtotal,
                VatType = p.VatType,    
                DiscountValue = p.DiscountValue,
                SerialNumbers = p.SerialNumbers.Select(s => new SerialMain
                {
                    PricelistId = s.PricelistId,
                    SerialNumbers = s.SerialNumbers
                }).ToList()
            }).ToList();

            _dbContext.SaveChanges();
            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            var transaction = _dbContext.Transactions
                .Include(t => t.PurchasedProducts)
                    .ThenInclude(p => p.SerialNumbers)
                .FirstOrDefault(t => t.Id == id);

            if (transaction == null)
            {
                return NotFound(new { message = "Transaction not found" });
            }

            // Delete SerialMain records associated with PurchasedProducts
            foreach (var purchasedProduct in transaction.PurchasedProducts)
            {
                if (purchasedProduct.SerialNumbers != null)
                {
                    _dbContext.SerialMains.RemoveRange(purchasedProduct.SerialNumbers);
                }
            }

            // Delete PurchasedProducts
            _dbContext.PurchasedProducts.RemoveRange(transaction.PurchasedProducts);

            // Delete Transaction
            _dbContext.Transactions.Remove(transaction);

            _dbContext.SaveChanges();

            return Ok(new { message = "Transaction and associated serial numbers deleted successfully" });
        }
    }
}