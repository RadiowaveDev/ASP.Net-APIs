using CarRental.Data;
using CarRental.Dto;
using CarRental.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RentalController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CreateRental(RentalDto rentalDto)
        {
            if (rentalDto.StartRentalDate >= rentalDto.EndRentalDate)
            {
                return BadRequest("Start date must be before end date.");
            }

            var car = await _context.Cars.FindAsync(rentalDto.CarId);
            if(car ==null)
                return NotFound("Car not found.");

            //check availability
            bool isCarAvailable = !await _context.Rentals.AnyAsync(r =>
            r.CarId == rentalDto.CarId && 
            (
                (rentalDto.StartRentalDate >= r.StartRentalDate && rentalDto.StartRentalDate < r.EndRentalDate) ||
                (rentalDto.EndRentalDate > r.StartRentalDate && rentalDto.EndRentalDate <= r.EndRentalDate) ||
                (rentalDto.StartRentalDate <= r.StartRentalDate && rentalDto.EndRentalDate >= r.EndRentalDate)
            ));

            if (!isCarAvailable)
            {
                return BadRequest("Car is not available for the selected dates.");
            }

            var totalDays = (rentalDto.EndRentalDate - rentalDto.StartRentalDate).Days;
            if(totalDays<=0)
                return BadRequest("Rental duration must be at least one day.");

            var totalPrice = totalDays * car.Price;

            var rental = new Rental
            {
                UserId = rentalDto.UserId,
                CarId = rentalDto.CarId,
                StartRentalDate = rentalDto.StartRentalDate,
                EndRentalDate = rentalDto.EndRentalDate
            };
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                rental.Id,
                TotalPrice = totalPrice,
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCar(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if(rental == null)
            {
                return NotFound();
            }
            return Ok(rental);
        }
        
    }
}
