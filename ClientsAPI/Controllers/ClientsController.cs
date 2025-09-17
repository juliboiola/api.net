using ClientsAPI.Data;
using ClientsAPI.Models;
using ClientsAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ClientsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllClients() 
        {
            var AllClients = dbContext.Clients.ToList();
            return Ok(AllClients);
        }

        [HttpGet]
        [Route("{id:guid}")]

        public IActionResult GetClientById(Guid id)
        {
            var client = dbContext.Clients.Find(id);

            if (client == null)
            { 
                return NotFound();
            } return Ok(client);
        }

        [HttpPost]
        public IActionResult AddClient(AddClientDto addClientDto)
        {
            var clientEntity = new Client()
            {
                Name = addClientDto.Name,
                Email = addClientDto.Email,
                Amount = addClientDto.Amount,
                CreatedAt = addClientDto.CreatedAt,
                Username = addClientDto.Username, 
                Location = addClientDto.Location  
            };

            dbContext.Clients.Add(clientEntity);
            dbContext.SaveChanges();
            return Ok(clientEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateClient(Guid id, UpdateClientDto updateClientDto)
        {
            var client = dbContext.Clients.Find(id);
            if(client == null)
            {
                return NotFound();
            }
            client.Name = updateClientDto.Name;
            client.Email = updateClientDto.Email;
            client.Amount = updateClientDto.Amount;

            dbContext.SaveChanges();

            return Ok(client);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public IActionResult DeleteClient (Guid id)
        {
            var client = dbContext.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            dbContext.Clients.Remove(client);
            dbContext.SaveChanges();

            return Ok(client);
        }

    }
    
}
