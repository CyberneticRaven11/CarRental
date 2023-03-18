using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static List<string> listOfCars = new List<string> { "Mustang", "Ferrari", "DeleteMe" };


        // GET: api/values/Get
        [HttpGet]
        [Route("Get")]
        public IEnumerable<string> Get()
        {
            return listOfCars;
        }

        // GET api/values/Get/5
        [HttpGet]
        [Route("Get/{id:int}")]
        public string Get(int id)
        {
            return listOfCars[id];
        }

        // POST api/values/Create
        [HttpPost]
        [Route("Create")]
        public IActionResult Post([FromBody] string value)
        {
            listOfCars.Add(value);
            var i = listOfCars.Count - 1;

            return Created(new Uri($"api/values/Get/{i}", UriKind.Relative), value);
        }

        // PUT api/values/Modify/{id:int}
        [HttpPut]
        [Route("Modify/{id:int}")]
        public void Put(int id, [FromBody] string value)
        {
            listOfCars[id] = value;
        }

        // DELETE api/values/Remove/5
        [HttpDelete]
        [Route("Remove/{id:int}")]
        public IActionResult Delete(int id)
        {
            listOfCars.RemoveAt(id);

            return Ok(true);
        }
    }
}
