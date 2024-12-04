namespace Domain.Models
{
        public class Product {
                public int Id { get; set; }
                public string Name { get; set; }
                public decimal Price { get; set; }

                public int CategoryId { get; set; }
                public virtual Category Category { get; set; }
                public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        }
}
