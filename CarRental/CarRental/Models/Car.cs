namespace CarRental.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public decimal Price { get; set; } //price rental per day
        public int Year { get; set; }   // Year of manufacture

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    }
}
