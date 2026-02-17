namespace IchthusPOSWeb.Models.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public Boolean? IsVoid { get; set; }
        public string? VoidBy { get; set; }
        public string? Terms { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string? PreparedBy { get; set; }
        public string? CheckedBy { get; set; }
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public int? Payment { get; set; }
        public string? PaymentType { get; set; }
        public string? DiscountType { get; set; }
        public int? DiscountAmount { get; set; }
        public int? TotalItems { get; set; }
        public int? TotalAmount { get; set; }
        public int? Change { get; set; }
      
        public int? LocationId { get; set; } // Added LocationId
        public Location? Location { get; set; } // Added Location
        public List<PurchasedProduct> PurchasedProducts { get; set; } = new List<PurchasedProduct>();

    }
}
