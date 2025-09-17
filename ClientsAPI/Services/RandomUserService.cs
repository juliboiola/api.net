using System.Linq;
using Microsoft.EntityFrameworkCore;
using ClientsAPI.Data;
using ClientsAPI.Models.Entities;
using ClientsAPI.Models;

// logica oara llamar a la api
namespace ClientsAPI.Services
{
    public class RandomUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public RandomUserService(HttpClient httpClient, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        // traer user random y guardarlo como client
        public async Task<Client> GetRandomUserAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<RandomUserResponse>("https://randomuser.me/api/");     //consumir api
            if (response?.Results == null || response.Results.Length == 0)
                return null;

            var user = response.Results[0];

        //mapear user random a client 
            var client = new Client
            {
                Id = Guid.NewGuid(),
                Name = $"{user.Name.First} {user.Name.Last}",
                Email = user.Email,
                Username = user.Login.Username,
                Location = user.Location.Country,
                Amount = new Random().Next(1000, 10000),
                CreatedAt = DateTime.UtcNow,
            };
        // guardar en db as client
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public List<Client> GetAllClients()
        {
            return _context.Clients.ToList();
        }

    }
}
