namespace CarRental.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Navigation property for rentals
        public ICollection<Rental> Rentals { get; set; } //Uno a muchos
    }
}
