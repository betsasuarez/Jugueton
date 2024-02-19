namespace Jugueton.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
       
        public List<Order> Orders { get; set; }
    }
}