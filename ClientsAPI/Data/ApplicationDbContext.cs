using ClientsAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientsAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
    }
}
