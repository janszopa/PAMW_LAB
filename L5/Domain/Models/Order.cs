namespace Domain.Models
{
    public class Order
    {
        public int Id { get; set; } 
        public DateTime OrderDate { get; set; } 

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
