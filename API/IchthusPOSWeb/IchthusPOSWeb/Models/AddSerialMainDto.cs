using IchthusPOSWeb.Models.Entities;

namespace IchthusPOSWeb.Models
{
    public class AddSerialMainDto
    {
        public required int PricelistId { get; set; } // Foreign Key or related column
        public List<int> SerialNumbers { get; set; } // A collection to hold serial numbers
    }
}