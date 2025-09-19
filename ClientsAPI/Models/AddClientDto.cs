namespace ClientsAPI.Models
{
    public class AddClientDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Username { get; set; }
        public string Location { get; set; }
    }
}