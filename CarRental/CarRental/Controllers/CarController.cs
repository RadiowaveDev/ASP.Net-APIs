using CarRental.Data;
using CarRental.Dto;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CarController(AppDbContext context)
        {
            _context = context;
        }

        private bool IsValidYear(int year)
        {
            var currentYear = DateTime.Now.Year;
            return year >= currentYear - 5 && year <= currentYear;
        }

        private CarDto ToDto(Car car) => new CarDto
        {
            Id = car.Id,
            Brand = car.Brand,
            Model = car.Model,
            Price = car.Price,
            Year = car.Year
        };
  
        private Car ToEntity(CarDto carDto) => new Car
        {
            Id = carDto.Id,
            Brand = carDto.Brand,
            Model = carDto.Model,
            Price = carDto.Price,
            Year = carDto.Year
        };

        //endpoint to get all cars
        [HttpPost]
        public async Task<ActionResult<CarDto>> CreateCar(CarDto carDto)
        {
            if(!IsValidYear(carDto.Year))
                return BadRequest("The car cannot be older than 5 years.");

            var car = ToEntity(carDto);
            _context.Cars.Add(car); //context represents the database
            await _context.SaveChangesAsync(); //saves changes to the database

            carDto.Id = car.Id; // Set the Id from the database to the DTO
            return CreatedAtAction(nameof(GetCar), new { id = carDto.Id }, carDto);
        }

        //endpoint to show one car by id
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return ToDto(car);
        }

        //endpoint to get all cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCars()
        {
            var cars = await _context.Cars.ToListAsync();
            return cars.Select(ToDto).ToList();
        }

        //PUT: api/car/5
        [HttpPut]
        public async Task<ActionResult> UpdateCar(int id, CarDto carDto)
        {
            if(id != carDto.Id)
                return BadRequest();
            
            if (!IsValidYear(carDto.Year))
                return BadRequest("The car cannot be older than 5 years");

            var car = await _context.Cars.FindAsync(id);
            if(car==null)
                return NotFound();
            
            car.Brand = carDto.Brand;
            car.Model = carDto.Model;
            car.Price = carDto.Price;
            car.Year = carDto.Year;

            await _context.SaveChangesAsync();
            return NoContent(); 
        }

        //DELETE: api/car/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
