namespace HerGuardian.Models
{
    public class TrustedContact
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Relation { get; set; } 

        // Foreign Key
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
