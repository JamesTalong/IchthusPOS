namespace IchthusPOSWeb.Models
{
    public class AddCustomerDto
    {
        public  string? CustomerName { get; set; }
        public  string? Address { get; set; }
        public  string? TinNumber { get; set; }
        public  string? MobileNumber { get; set; }
        public  string? BusinessStyle { get; set; }
        public string? RFID { get; set; }
        public string? CustomerType { get; set; }
    }
}
