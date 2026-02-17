namespace IchthusPOSWeb.Models.Entities
{
    public class SerialStaging
    {
        public int Id { get; set; }
        public string? SerialName { get; set; }
        public bool IsSold { get; set; }
        public int? BatchStagingId { get; set; }

    }
}
