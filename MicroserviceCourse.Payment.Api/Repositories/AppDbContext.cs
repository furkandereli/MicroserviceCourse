using Microsoft.EntityFrameworkCore;

namespace MicroserviceCourse.Payment.Api.Repositories;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedNever();
            entity.Property(x => x.UserId).IsRequired();
            entity.Property(x => x.OrderCode).IsRequired().HasMaxLength(10);
            entity.Property(x => x.Created).IsRequired();
            entity.Property(x => x.Amount).IsRequired().HasPrecision(18,2);
            entity.Property(x => x.Status).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
