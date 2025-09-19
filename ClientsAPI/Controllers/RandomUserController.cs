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

        [HttpGet("by-country/{country}")]
        public IActionResult GetByCountry(string country)
        {
            var clients = _service.GetAllClients()
                .Where(c => c.Location == country)
                .ToList();
            return Ok(clients);
        }

        [HttpGet("max-amount")]
        public IActionResult GetClientWithMaxAmount()
        {
            var client = _service.GetAllClients()
                .OrderByDescending(c => c.Amount)
                .FirstOrDefault();

            return Ok(client);
        }

        [HttpGet("group-by-country")]
        public IActionResult GroupByCountry([FromQuery] string country)
        {

            var userInCountry = _service.GetAllClients()
                .Where(c => c.Location == country)
                .ToList();

            if (!userInCountry.Any())
            {
                return NotFound($"No users found in {country}");
            }

            var result = new
            {
                Country = country,
                Quantity = userInCountry.Count,
                Users = userInCountry
            };
            return Ok(result);
        }

        [HttpGet("amount")]
        public IActionResult GetByMinAmount([FromQuery] decimal minAmount)
        {
            var clients = _service.GetAllClients()
                .Where(c => c.Amount > minAmount)
                .ToList();

            if (!clients.Any())
            {
                return NotFound($"No users were found witj purchases greater than {minAmount} ");
            }

            return Ok(clients);
        }
        [HttpGet("email")]
        public IActionResult GetByEmail([FromQuery] string email)
        {
            var clientEmail = _service.GetAllClients()
                .Where(c => c.Email == email)
                .ToList();

            if (!clientEmail.Any())
            {
               return NotFound($"No users were found with email {email} ");
            }
            return Ok(clientEmail);
        }

    }
}
