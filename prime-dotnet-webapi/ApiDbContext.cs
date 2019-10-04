using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

using Prime.Models;

namespace Prime
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Enrolment> Enrolments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Discriminators
            modelBuilder.Entity<Address>()
                .HasDiscriminator<AddressType>("AddressType")
                .HasValue<PhysicalAddress>(AddressType.Physical)
                .HasValue<MailingAddress>(AddressType.Mailing);
            #endregion

            #region CollegeSeed
            modelBuilder.Entity<College>().HasData(
                new College { Code = 1, Name = "College of Physicians and Surgeons of BC (CPSBC)" },
                new College { Code = 2, Name = "College of Pharmacists of BC (CPBC)" },
                new College { Code = 3, Name = "College of Registered Nurses of BC (CRNBC)" },
                new College { Code = 4, Name = "None" }
            );
            #endregion

            #region LicenseSeed
            modelBuilder.Entity<License>().HasData(
                new License { Code = 1, Name = "Full - General" },
                new License { Code = 2, Name = "Full - Pharmacist" },
                new License { Code = 3, Name = "Full - Specialty" },
                new License { Code = 4, Name = "Registered Nurse" },
                new License { Code = 5, Name = "Temporary Registered Nurse" }
                );
            #endregion

            #region PracticeSeed
            modelBuilder.Entity<Practice>().HasData(
                new Practice { Code = 1, Name = "Remote Practice" },
                new Practice { Code = 2, Name = "Reproductive Care" },
                new Practice { Code = 3, Name = "Sexually Transmitted Infections (STI)" },
                new Practice { Code = 4, Name = "None" }
                );
            #endregion

            #region JobNameSeed
            modelBuilder.Entity<JobName>().HasData(
                new JobName { Code = 1, Name = "Medical Office Assistant" },
                new JobName { Code = 2, Name = "Midwife" },
                new JobName { Code = 3, Name = "Nurse (not nurse practitioner)" },
                new JobName { Code = 4, Name = "Pharmacy Assistant" },
                new JobName { Code = 5, Name = "Pharmacy Technician" },
                new JobName { Code = 6, Name = "Registration Clerk" },
                new JobName { Code = 7, Name = "Ward Clerk" },
                new JobName { Code = 8, Name = "Other" }
                );
            #endregion

            #region OrganizationNameSeed
            modelBuilder.Entity<OrganizationName>().HasData(
                new OrganizationName { Code = 1, Name = "Vancouver Island Health" },
                new OrganizationName { Code = 2, Name = "Shoppers Drug Mart" }
                );
            #endregion

            #region OrganizationTypeSeed
            modelBuilder.Entity<OrganizationType>().HasData(
                new OrganizationType { Code = 1, Name = "Health Authority" },
                new OrganizationType { Code = 2, Name = "Pharmacy" }
                );
            #endregion

            #region Indexes
            modelBuilder.Entity<MailingAddress>()
                .HasIndex(a => a.EnrolleeId)
                .HasName("IX_Address_EnrolleeId")
                .IsUnique(false);

            modelBuilder.Entity<PhysicalAddress>()
                .HasIndex(a => a.EnrolleeId)
                .HasName("IX_Address_EnrolleeId")
                .IsUnique(false);

            modelBuilder.Entity<Address>()
                .HasIndex("EnrolleeId", "AddressType")
                .HasName("IX_EnrolleeId_AddressType")
                .IsUnique(true);
            #endregion

            #region Relationships
            // modelBuilder.Entity<MailingAddress>(entity =>
            // {
            //     entity.HasOne(e => e.Enrollee)
            //         .WithMany();
            // });

            // modelBuilder.Entity<Enrolment>(entity =>
            // {
            //     entity.HasOne(e => e.Enrollee)
            //         .WithMany()
            //         .HasForeignKey("EnrolleeId");
            // });

            // modelBuilder.Entity<Enrollee>(entity =>
            // {
            //     entity.HasOne(e => e.PhysicalAddress)
            //         .WithMany()
            //         .HasForeignKey("EnrolleeId");
            // });
            // modelBuilder.Entity<Enrollee>(entity =>
            // {
            //     entity.HasOne(e => e.MailingAddress)
            //         .WithMany()
            //         .HasForeignKey("EnrolleeId");
            // });

            // modelBuilder.Entity<Certification>(entity =>
            // {
            //     entity.HasOne(c => c.Enrolment)
            //         .WithMany()
            //         .HasForeignKey("EnrolmentId");
            // });
            // modelBuilder.Entity<Certification>(entity =>
            // {
            //     entity.HasOne(c => c.College)
            //         .WithMany()
            //         .HasForeignKey("CollegeCode");
            // });
            // modelBuilder.Entity<Certification>(entity =>
            // {
            //     entity.HasOne(c => c.License)
            //         .WithMany()
            //         .HasForeignKey("LicenseCode");
            // });
            // modelBuilder.Entity<Certification>(entity =>
            // {
            //     entity.HasOne(c => c.Practice)
            //         .WithMany()
            //         .HasForeignKey("PracticeCode");
            // });


            // modelBuilder.Entity<Job>(entity =>
            // {
            //     entity.HasOne(c => c.Enrolment)
            //         .WithMany()
            //         .HasForeignKey("EnrolmentId");
            // });

            // modelBuilder.Entity<Organization>(entity =>
            // {
            //     entity.HasOne(o => o.Enrolment)
            //         .WithMany()
            //         .HasForeignKey("EnrolmentId");
            // });
            // modelBuilder.Entity<Organization>(entity =>
            // {
            //     entity.HasOne(o => o.OrganizationType)
            //         .WithMany()
            //         .HasForeignKey("OrganizationTypeCode");
            // });
            #endregion
        }

    }

    public static class ApiDbContextExtensions
    {
        public static IQueryable Set(this DbContext context, Type T)
        {
            // Get the generic type definition
            MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);

            // Build a method with the specific type argument you're interested in
            method = method.MakeGenericMethod(T);

            return method.Invoke(context, null) as IQueryable;
        }

        public static IQueryable<T> Set<T>(this DbContext context)
        {
            // Get the generic type definition 
            MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);

            // Build a method with the specific type argument you're interested in 
            method = method.MakeGenericMethod(typeof(T));

            return method.Invoke(context, null) as IQueryable<T>;
        }
    }
}