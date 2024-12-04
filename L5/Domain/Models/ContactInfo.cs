namespace Domain.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; } 
        public virtual User User { get; set; }
    }
}