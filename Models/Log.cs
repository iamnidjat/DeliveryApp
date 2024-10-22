namespace DeliveryApp.Models
{
    public class Log
    {
        public Guid Id { get; set; }
        public string? IpAddress { get; set; }
        public DateTime AccessTime { get; set; }
    }
}
