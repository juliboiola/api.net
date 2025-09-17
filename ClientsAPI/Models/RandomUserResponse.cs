using System.Collections.Generic;

namespace ClientsAPI.Models
{
    public class RandomUserResponse
    {
        public UserResult[] Results { get; set; }
        }
    public class UserResult
    {
        public Name Name { get; set; }
        public string Email { get; set; }
        public Login Login { get; set; }
        public Location Location { get; set; }
    }
    public class Name
    {
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class Login
    {
        public string Username { get; set; }
    }
    public class Location
    {
        public string Country { get; set; }
    }
}
