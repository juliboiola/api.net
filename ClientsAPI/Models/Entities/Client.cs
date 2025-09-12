namespace ClientsAPI.Models.Entities
{
    public class Client
    { //columnas que tendra la tabla
        public Guid Id { get; set; }             //genera solo
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;       //sirve but buscar alternativas

    }
}
