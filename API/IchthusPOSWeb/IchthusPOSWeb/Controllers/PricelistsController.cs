using IchthusPOSWeb.Data;
using IchthusPOSWeb.Models.Entities;
using IchthusPOSWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IchthusPOSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricelistsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PricelistsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllPricelists()
        {
            // BEST PRACTICE: Use .Select() Projection. 
            // EF Core automatically generates the JOINs without needing .Include()

            var pricelists = dbContext.Pricelists
             .AsNoTracking()
             .AsSplitQuery() // Keeps the Batches query separate from the Product query
             .OrderByDescending(p => p.Id)
             .Select(i => new
             {
                 Id = i.Id,
                 ProductId = i.ProductId,
                 // REMOVED ProductImage for speed as requested
                 ProductImage = (byte[])null, // Sending null so frontend uses default image
                 ItemCode = i.Product.ItemCode,
                 BarCode = i.Product.BarCode,
                 Product = i.Product.ProductName,
                 LocationId = i.LocationId,
                 Location = i.Location.LocationName,
                 ColorId = i.ColorId,
                 Color = i.Color.ColorName,
                 BrandId = i.Product.BrandId,
                 Brand = i.Product.Brand,
                 CategoryId = i.Product.CategoryId,
                 Category = i.Product.Category,
                 CategoryTwoId = i.Product.CategoryTwoId,
                 CategoryTwo = i.Product.CategoryTwo,
                 CategoryThreeId = i.Product.CategoryThreeId,
                 CategoryThree = i.Product.CategoryThree,
                 CategoryFourId = i.Product.CategoryFourId,
                 CategoryFour = i.Product.CategoryFour,
                 CategoryFiveId = i.Product.CategoryFiveId,
                 CategoryFive = i.Product.CategoryFive,
                 HasSerial = i.Product.HasSerial,
                 VatEx = i.VatEx,
                 VatInc = i.VatInc,
                 Reseller = i.Reseller,
                 ZeroRated = i.ZeroRated,

                 // FAST STOCK CALCULATION (Done in SQL)
                 Stock = i.Batches.SelectMany(b => b.SerialNumbers).Count(sn => !sn.IsSold),

                 // ONLY UNSOLD SERIALS
                 Batches = i.Batches.Select(s => new
                 {
                     Id = s.Id,
                     BatchDate = s.BatchDate,
                     NumberOfItems = s.NumberOfItems,
                     PricelistId = s.PricelistId,
                     HasSerial = i.Product.HasSerial,
                     SerialNumbers = s.SerialNumbers
                        .Where(y => !y.IsSold) // Database filter
                        .Select(y => new { y.Id, y.IsSold, y.SerialName }) // Select only needed columns
                        .ToList()
                 }).ToList()
             })
             .ToList();

            return Ok(pricelists);
        }

        [HttpGet("by-product/{productName}")]
        public IActionResult GetPricelistsByProductName(string productName)
        {
            var normalizedInput = productName.ToLower().Replace(" ", "");

            var pricelist = dbContext.Pricelists
                .AsNoTracking()
                .AsSplitQuery()
                .Where(i => i.Product.ProductName.ToLower().Replace(" ", "") == normalizedInput)
                .Select(i => new
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductImage = (byte[])null, // Keep null for speed
                    ItemCode = i.Product.ItemCode,
                    BarCode = i.Product.BarCode,
                    Product = i.Product.ProductName,
                    LocationId = i.LocationId,
                    Location = i.Location.LocationName,
                    ColorId = i.ColorId,
                    Color = i.Color.ColorName,
                    BrandId = i.Product.BrandId,
                    Brand = i.Product.Brand,
                    CategoryId = i.Product.CategoryId,
                    Category = i.Product.Category,
                    CategoryTwoId = i.Product.CategoryTwoId,
                    CategoryTwo = i.Product.CategoryTwo,
                    CategoryThreeId = i.Product.CategoryThreeId,
                    CategoryThree = i.Product.CategoryThree,
                    CategoryFourId = i.Product.CategoryFourId,
                    CategoryFour = i.Product.CategoryFour,
                    CategoryFiveId = i.Product.CategoryFiveId,
                    CategoryFive = i.Product.CategoryFive,
                    HasSerial = i.Product.HasSerial,
                    VatEx = i.VatEx,
                    VatInc = i.VatInc,
                    Reseller = i.Reseller,
                    ZeroRated = i.ZeroRated,

                    Stock = i.Batches.SelectMany(b => b.SerialNumbers).Count(sn => !sn.IsSold),

                    Batches = i.Batches.Select(s => new
                    {
                        Id = s.Id,
                        BatchDate = s.BatchDate,
                        NumberOfItems = s.NumberOfItems,
                        PricelistId = s.PricelistId,
                        HasSerial = i.Product.HasSerial,
                        SerialNumbers = s.SerialNumbers
                            .Where(y => !y.IsSold)
                            .Select(y => new { y.Id, y.IsSold, y.SerialName })
                            .ToList()
                    }).ToList()
                })
                .ToList();

            if (!pricelist.Any()) return NotFound();
            return Ok(pricelist);
        }

        [HttpGet("by-itemcode/{itemCode}")]
        public IActionResult GetPricelistsByItemCode(string itemCode)
        {
            var normalizedInput = itemCode.ToLower().Replace(" ", "");

            var pricelist = dbContext.Pricelists
                .AsNoTracking()
                .AsSplitQuery()
                .Where(i => i.Product.ItemCode.ToLower().Replace(" ", "") == normalizedInput)
                .Select(i => new
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductImage = (byte[])null,
                    ItemCode = i.Product.ItemCode,
                    BarCode = i.Product.BarCode,
                    Product = i.Product.ProductName,
                    LocationId = i.LocationId,
                    Location = i.Location.LocationName,
                    ColorId = i.ColorId,
                    Color = i.Color.ColorName,
                    BrandId = i.Product.BrandId,
                    Brand = i.Product.Brand,
                    CategoryId = i.Product.CategoryId,
                    Category = i.Product.Category,
                    CategoryTwoId = i.Product.CategoryTwoId,
                    CategoryTwo = i.Product.CategoryTwo,
                    CategoryThreeId = i.Product.CategoryThreeId,
                    CategoryThree = i.Product.CategoryThree,
                    CategoryFourId = i.Product.CategoryFourId,
                    CategoryFour = i.Product.CategoryFour,
                    CategoryFiveId = i.Product.CategoryFiveId,
                    CategoryFive = i.Product.CategoryFive,
                    HasSerial = i.Product.HasSerial,
                    VatEx = i.VatEx,
                    VatInc = i.VatInc,
                    Reseller = i.Reseller,
                    ZeroRated = i.ZeroRated,

                    Stock = i.Batches.SelectMany(b => b.SerialNumbers).Count(sn => !sn.IsSold),

                    Batches = i.Batches.Select(s => new
                    {
                        Id = s.Id,
                        BatchDate = s.BatchDate,
                        NumberOfItems = s.NumberOfItems,
                        PricelistId = s.PricelistId,
                        HasSerial = i.Product.HasSerial,
                        SerialNumbers = s.SerialNumbers
                            .Where(y => !y.IsSold)
                            .Select(y => new { y.Id, y.IsSold, y.SerialName })
                            .ToList()
                    }).ToList()
                })
                .ToList();

            if (!pricelist.Any()) return NotFound();
            return Ok(pricelist);
        }

        // NOTE: In the Single ID view, we usually DO want the image.
        // If you want to remove it here too, change i.Product.ProductImage to (byte[])null
        [HttpGet("{id}")]
        public IActionResult GetPricelistById(int id)
        {
            var pricelist = dbContext.Pricelists
                .AsNoTracking()
                .AsSplitQuery()
                .Where(i => i.Id == id)
                .Select(i => new
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    // We might want the image here since it's just ONE item
                    ProductImage = i.Product.ProductImage,
                    ItemCode = i.Product.ItemCode,
                    BarCode = i.Product.BarCode,
                    Product = i.Product.ProductName,
                    LocationId = i.LocationId,
                    Location = i.Location.LocationName,
                    ColorId = i.ColorId,
                    Color = i.Color.ColorName,
                    BrandId = i.Product.BrandId,
                    Brand = i.Product.Brand,
                    CategoryId = i.Product.CategoryId,
                    Category = i.Product.Category,
                    CategoryTwoId = i.Product.CategoryTwoId,
                    CategoryTwo = i.Product.CategoryTwo,
                    CategoryThreeId = i.Product.CategoryThreeId,
                    CategoryThree = i.Product.CategoryThree,
                    CategoryFourId = i.Product.CategoryFourId,
                    CategoryFour = i.Product.CategoryFour,
                    CategoryFiveId = i.Product.CategoryFiveId,
                    CategoryFive = i.Product.CategoryFive,
                    HasSerial = i.Product.HasSerial,
                    VatEx = i.VatEx,
                    VatInc = i.VatInc,
                    Reseller = i.Reseller,
                    ZeroRated = i.ZeroRated,

                    Stock = i.Batches.SelectMany(b => b.SerialNumbers).Count(sn => !sn.IsSold),

                    Batches = i.Batches.Select(s => new
                    {
                        Id = s.Id,
                        BatchDate = s.BatchDate,
                        NumberOfItems = s.NumberOfItems,
                        PricelistId = s.PricelistId,
                        HasSerial = i.Product.HasSerial,
                        SerialNumbers = s.SerialNumbers
                            .Where(y => !y.IsSold)
                            .Select(y => new { y.Id, y.IsSold, y.SerialName })
                            .ToList()
                    }).ToList()
                })
                .FirstOrDefault();

            if (pricelist == null) return NotFound();
            return Ok(pricelist);
        }

        [HttpPost]
        public IActionResult AddPricelist(AddPricelistDto addPricelistDto)
        {
            var pricelist = new IchthusPOSWeb.Models.Entities.Pricelist
            {
                ProductId = addPricelistDto.ProductId,
                LocationId = addPricelistDto.LocationId,
                ColorId = addPricelistDto.ColorId,
                VatEx = addPricelistDto.VatEx,
                VatInc = addPricelistDto.VatInc,
                Reseller = addPricelistDto.Reseller,
                ZeroRated = addPricelistDto.ZeroRated
            };

            dbContext.Pricelists.Add(pricelist);
            dbContext.SaveChanges();
            return Ok(pricelist);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePricelist(int id, UpdatePricelistDto updatePricelistDto)
        {
            var pricelist = dbContext.Pricelists.Find(id);
            if (pricelist == null) return NotFound();

            pricelist.ProductId = updatePricelistDto.ProductId ?? pricelist.ProductId;
            pricelist.LocationId = updatePricelistDto.LocationId ?? pricelist.LocationId;
            pricelist.ColorId = updatePricelistDto.ColorId ?? pricelist.ColorId;
            pricelist.VatEx = updatePricelistDto.VatEx ?? pricelist.VatEx;
            pricelist.VatInc = updatePricelistDto.VatInc ?? pricelist.VatInc;
            pricelist.Reseller = updatePricelistDto.Reseller ?? pricelist.Reseller;
            pricelist.ZeroRated = updatePricelistDto.ZeroRated ?? pricelist.ZeroRated;

            dbContext.SaveChanges();
            return Ok(pricelist);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePricelist(int id)
        {
            var pricelist = dbContext.Pricelists
                .Include(p => p.Batches)
                .ThenInclude(b => b.SerialNumbers)
                .FirstOrDefault(p => p.Id == id);

            if (pricelist == null) return NotFound();

            foreach (var batch in pricelist.Batches)
            {
                dbContext.SerialNumbers.RemoveRange(batch.SerialNumbers);
            }
            dbContext.Batches.RemoveRange(pricelist.Batches);
            dbContext.Pricelists.Remove(pricelist);

            dbContext.SaveChanges();
            return Ok();
        }
    }
}