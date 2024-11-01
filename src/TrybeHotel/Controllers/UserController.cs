using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult GetUsers()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            var users = _repository.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserDtoInsert user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var userDto = _repository.Add(user);
            if (userDto == null)
            {
                return Conflict(new { message = "User email already exists" });
            }

            return CreatedAtAction(nameof(GetUsers), new { userId = userDto.UserId }, userDto);
        }
    }
}