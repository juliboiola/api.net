namespace ClientsAPI.Models
{
    public class UpdateClientDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required decimal Amount { get; set; }
    }
}
