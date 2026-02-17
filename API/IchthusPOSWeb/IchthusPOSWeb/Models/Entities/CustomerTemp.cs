namespace IchthusPOSWeb.Models.Entities
{
    public class CustomerTemp
    {
        public int Id { get; set; }
        public required int CustomerId { get; set; }
        public required string CustomerName { get; set; }
    }
}
