namespace Jugueton.Models
{
    public class Product
    {
        public Product()
        {
            OrderProducts = new List<OrderProduct>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}