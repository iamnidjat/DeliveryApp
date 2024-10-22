namespace DeliveryApp.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public double Weight { get; set; }
        public string? CityDistrict { get; set; }
        public DateTime DeliveryTime { get; set; }
    }
}
