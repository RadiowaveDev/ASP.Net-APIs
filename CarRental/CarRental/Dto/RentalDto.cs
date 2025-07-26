using CarRental.Models;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Dto
{
    public class RentalDto
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CarId { get; set; }
        [Required]
        public DateTime StartRentalDate { get; set; }
        [Required]
        public DateTime EndRentalDate { get; set; }
    }
}
