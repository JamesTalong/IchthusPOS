using IchthusPOSWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IchthusPOSWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CategoryTwo> CategoriesTwo { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Pricelist> Pricelists { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<SerialNumber> SerialNumbers { get; set; }
        public DbSet<SerialTemp> SerialTemps { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerTemp> CustomerTemps { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PurchasedProduct> PurchasedProducts { get; set; }
        public DbSet<CategoryThree> CategoriesThree { get; set; }
        public DbSet<CategoryFour> CategoriesFour { get; set; }
        public DbSet<CategoryFive> CategoriesFive { get; set; }
        public DbSet<SerialMain> SerialMains { get; set; }
        public DbSet<BatchStaging> BatchStagings { get; set; }
        public DbSet<SerialStaging> SerialStagings { get; set; }
        public DbSet<JobRole> JobRoles { get; set; }
        public DbSet<BatchStaginghistory> BatchStagingsHistory { get; set; }
        public DbSet<SerialStagingsHistory> SerialStagingsHistory { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferItem> TransferItems { get; set; }
        public DbSet<CompletedTransfer> CompletedTransfers { get; set; }
        public DbSet<CompletedTransferItems> CompletedTransferItems { get; set; }
        public DbSet<CompletedTransferSerial> CompletedTransferSerials { get; set; }
        public DbSet<ReceivedTransfer> ReceivedTransfers { get; set; }
        public DbSet<ReceivedTransferItems> ReceivedTransferItems { get; set; }
        public DbSet<ReceivedTransferSerial> ReceivedTransferSerials { get; set; }

    }
}
