using Microsoft.EntityFrameworkCore;
using Students.Domain.Schools;
using Students.Domain.StudentPayments;

namespace Students.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<School> Schols { get; set; }
    public DbSet<Domain.Students.Student> Students { get; set; }
    public DbSet<StudentPayment> StudentPayments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}