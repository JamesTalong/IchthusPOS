namespace IchthusPOSWeb.Models.Entities
{
    public class SerialNumber
    {
        public int Id { get; set; }
        public  string? SerialName { get; set; }
        public bool IsSold { get; set; }
        public int? BatchId { get; set; }

    }
}
