using HerGuardian.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<TrustedContact> TrustedContacts { get; set; }
    public DbSet<SOSAlert> SOSAlerts { get; set; }
    public DbSet<LocationLog> LocationLogs { get; set; }
}