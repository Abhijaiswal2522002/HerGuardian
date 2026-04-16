namespace HerGuardian.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; } // later hash it

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public List<TrustedContact> TrustedContacts { get; set; }
        public List<SOSAlert> SOSAlerts { get; set; }
        public List<LocationLog> LocationLogs { get; set; }
    }
}
