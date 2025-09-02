using Microsoft.EntityFrameworkCore;
using PharmaSky.Domain.Entities;



namespace Pharmacy.Persistence.Context
{
    public class PharmaSkyDbContext: DbContext
    {
       
        public PharmaSkyDbContext(DbContextOptions<PharmaSkyDbContext> options) : base(options)
        {
        }


        public DbSet<Weather> Weathers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OnCallPharmacy> OnCallPharmacies { get; set; }
        public DbSet<County> Counties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Weather>()
                .Property(h => h.Day)
                .HasConversion<int>(); // Enum'u DB'de int olarak tutar

           
            modelBuilder.Entity<OnCallPharmacy>()
                .HasKey(p => p.PharmacyId);

            // İlçe ilişkisi
            modelBuilder.Entity<OnCallPharmacy>()
                .HasOne(p => p.County)
                .WithMany(c => c.OnCallPharmacies)
                .HasForeignKey(p => p.CountyId);
        }
    }
}
