namespace IchthusPOSWeb.Models.Entities
{
    public class SerialTemp
    {
        public int Id { get; set; } 
        public required int PricelistId { get; set; } 
        public List<int> SerialNumbers { get; set; } 
    }
}

