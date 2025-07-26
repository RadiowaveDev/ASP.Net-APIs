using CarRental.Models;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Dto
{
    public class CarDto
    {
        //Clase para mover datos de un coche entre la capa de presentación y la capa de datos
        public int Id { get; set; } // Requerido para verbo HttpPut
        [Required]
        public string Brand { get; set; } = string.Empty;
        [Required]
        public string Model { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; } //price rental per day
        [Required]
        public int Year { get; set; }   // Year of manufacture
    }
}
