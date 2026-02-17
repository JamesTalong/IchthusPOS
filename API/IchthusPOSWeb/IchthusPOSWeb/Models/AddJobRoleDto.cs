
namespace IchthusPOSWeb.Models
{
    public class AddJobRoleDto
    {
        public required string RoleName { get; set; }

        public bool Dashboard { get; set; }
        public bool Users { get; set; }
        public bool UserRestriction { get; set; }
        public bool Transfer { get; set; }
        public bool InventoryCost { get; set; } // <--- ADD THIS
        public bool TransferItems { get; set; } // <--- ADD THIS
        public bool Categories { get; set; }
        public bool Categories2 { get; set; }
        public bool Categories3 { get; set; }
        public bool Categories4 { get; set; }
        public bool Categories5 { get; set; }
        public bool Brands { get; set; }
        public bool Colors { get; set; }
        public bool Locations { get; set; }
        public bool ProductList { get; set; }
        public bool Pricelists { get; set; }
        public bool Batches { get; set; }
        public bool SerialNumbers { get; set; }
        public bool Customers { get; set; }
        public bool Inventory { get; set; }
        public bool InventoryStaging { get; set; }
        public bool Transactions { get; set; }
        public bool POS { get; set; }
    }
}
