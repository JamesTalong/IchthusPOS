
using IchthusPOSWeb.Models.Entities;

public class UpdateSerialTempDto
{
    public required int PricelistId { get; set; } // Foreign Key or related column
    public List<int> SerialNumbers { get; set; } // A collection to hold serial numbers
}