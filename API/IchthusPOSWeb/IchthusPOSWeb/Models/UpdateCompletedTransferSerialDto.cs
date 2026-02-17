using IchthusPOSWeb.Models.Entities;

namespace IchthusPOSWeb.Models
{
    public class UpdateCompletedTransferSerialDto
    {
        public int? SerialNumberId { get; set; }
        public string? SerialName { get; set; }
        public string? Status { get; set; }
    }
}
