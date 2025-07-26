namespace CarRental.Models
{
    public class Rental
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = new User(); // Navigation property for the user renting the car

        public int CarId { get; set; }
        public Car Car { get; set; } = new Car();// Navigation property for the car being rented


        public DateTime StartRentalDate { get; set; }
        public DateTime EndRentalDate { get; set; }

       
    }
}
