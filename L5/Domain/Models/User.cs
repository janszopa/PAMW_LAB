namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }
    }
}