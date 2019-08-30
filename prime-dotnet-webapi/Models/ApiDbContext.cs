using Microsoft.EntityFrameworkCore;

namespace prime.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Application> Application { get; set; }
        //public DbSet<PharmacistRegistrationNumber> PharmacistRegistrationNumber { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Application>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            //modelBuilder.Entity<PharmacistRegistrationNumber>()
            //    .Property(p => p.Id)
            //    .ValueGeneratedOnAdd();

            //modelBuilder.Entity<PharmacistRegistrationNumber>().HasData(new PharmacistRegistrationNumber { Id = 1, Number = "A0000" });
            //modelBuilder.Entity<PharmacistRegistrationNumber>().HasData(new PharmacistRegistrationNumber { Id = 2, Number = "A0001" });
            //modelBuilder.Entity<PharmacistRegistrationNumber>().HasData(new PharmacistRegistrationNumber { Id = 3, Number = "B0000" });
            //modelBuilder.Entity<PharmacistRegistrationNumber>().HasData(new PharmacistRegistrationNumber { Id = 4, Number = "B0001" });

            base.OnModelCreating(modelBuilder);
        }
    }
}