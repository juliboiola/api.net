using ClientsAPI.Models;
using ClientsAPI.Models.Entities;
using ClientsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RandomUserController : Controller
    {
        private readonly RandomUserService _service;
        public RandomUserController(RandomUserService service)
        {
            _service = service;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateRandomUser()
        {
            var client = await _service.GetRandomUserAsync();
            if (client == null)
            {
                return BadRequest("Failed to create a random usder");
            }
            //mapeo a dto -mejor q devolver entidad-
            var dto = new RandomUserDto
            {
                Name = client.Name,
                Email = client.Email,
                Username = client.Username,
                Country = client.Location
            };

            return Ok(dto);

        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var clients = _service.GetAllClients();

            var list = clients.Select(c => new RandomUserDto
            {
                Name = c.Name,
                Email = c.Email,
                Username = c.Username,
                Country = c.Location
            }).ToList();

            return Ok(list);
        }
    }
}
