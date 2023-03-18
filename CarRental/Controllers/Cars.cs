using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cars : ControllerBase
    {
        public static List<Car> cars = new List<Car> {
            new Car { Id="1", Model="Lamborgini", Year="1999"},
            new Car { Id="2", Model="BMW", Year="2010"}
        };

        // GET api/Car (if not specified, this will be the default one)
        // GET api/Car/GetAll
        [HttpGet]
        [Route("")]
        [Route("GetAll")]
        public IEnumerable<Car> Get()
        {
            return ReturnAllCars();
        }

        // GET api/Car/Get/2
        [HttpGet]
        [Route("Get/{id:int}")]
        public IActionResult Get(string id)
        {
            Car car = ReturnCarById(id);

            if (car == null)
            {
                return NotFound("Car with that id does not exist!");
            }
            return Ok(car);
        }

        // POST api/car/Create
        [HttpPost]
        [Route("Create")]
        public IActionResult Post([FromBody] Car car)
        {

            if (cars.Exists(s => s.Id == car.Id))
            {
                return Conflict("Car with that id already exist!");
            }
            cars.Add(car);

            //adds new object location to the url
            return Created(new Uri($"api/car/Get/{car.Id}", UriKind.Relative), car.Id);

            //return Created($"Car with id {car.Id} is created", car.Id);
        }

        // PUT api/car/Update/2
        [HttpPut]
        [Route("Update/{id:int}")]
        public IActionResult Put(string id, [FromBody] Car updatedCar)
        {
            Car car = ReturnCarById(id);
            if (car == null)
            {
                return NotFound("Car with that id does not exist!");
            }
            car.Model = updatedCar.Model;
            car.Year = updatedCar.Year;
            return Ok($"Car with id {updatedCar.Id} is sucessfully updated");
        }

        // DELETE api/car/Delete/2
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult Delete(string id)
        {
            Car car = ReturnCarById(id);
            if (car == null)
            {
                return NotFound("Car with that id does not exist");
            }
            cars.Remove(car);
            return Ok($"Car with id {car.Id} is sucessfully deleted");
        }

        private IEnumerable<Car> ReturnAllCars()
        {
            return cars;
        }

        private Car ReturnCarById(string id)
        {
            Car car = cars.FirstOrDefault(s => s.Id == id);
            return car;
        }
    }
}
