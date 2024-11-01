using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityController : Controller
    {
        private readonly ICityRepository _repository;
        public CityController(ICityRepository repository)
        {
            _repository = repository;
        }
        
        // 2. Desenvolva o endpoint GET /city
        [HttpGet]
        public IActionResult GetCities(){
            return Ok(_repository.GetCities());
        }

        // 3. Desenvolva o endpoint POST /city
        [HttpPost]
        public IActionResult PostCity([FromBody] City city)
        {
            var createdCity = _repository.AddCity(city);
            return CreatedAtAction(nameof(GetCities), new { id = createdCity.CityId }, createdCity);
        }
        // 3. Desenvolva o endpoint PUT /city
        [HttpPut]
        public IActionResult PutCity([FromBody] City city){
            var updatedCity = _repository.UpdateCity(city);
            return Ok(updatedCity);
        }
    }
}