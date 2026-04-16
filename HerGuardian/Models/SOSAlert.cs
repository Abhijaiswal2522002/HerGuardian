namespace HerGuardian.Models
{
    public class SOSAlert
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime TriggeredAt { get; set; } = DateTime.UtcNow;

        public bool IsResolved { get; set; } = false;

        // Foreign Key
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
