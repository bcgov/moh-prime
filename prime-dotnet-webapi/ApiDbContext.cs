using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Prime.Models;

namespace Prime
{
    public class ApiDbContext : DbContext
    {
        private readonly Guid SYSTEM_USER = Guid.Empty;

        private readonly IHttpContextAccessor _context;

        public ApiDbContext(DbContextOptions<ApiDbContext> options, IHttpContextAccessor context)
            : base(options)
        {
            _context = context;
        }

        public DbSet<Enrolment> Enrolments { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Enrollee> Enrollees { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAudits();

            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.ApplyAudits();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAudits()
        {
            ChangeTracker.DetectChanges();

            var created = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            var modified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            var currentUser = PrimeUtils.PrimeUserId(_context?.HttpContext?.User);  //note: defaults to Guid.Empty if there is no user
            var currentDateTime = DateTime.Now;

            foreach (var item in created)
            {
                if (item.Entity is IAuditable entity)
                {
                    item.CurrentValues[nameof(IAuditable.CreatedUserId)] = currentUser;
                    item.CurrentValues[nameof(IAuditable.CreatedTimeStamp)] = currentDateTime;
                    item.CurrentValues[nameof(IAuditable.UpdatedUserId)] = currentUser;
                    item.CurrentValues[nameof(IAuditable.UpdatedTimeStamp)] = currentDateTime;
                }
            }

            foreach (var item in modified)
            {
                if (item.Entity is IAuditable entity)
                {
                    item.CurrentValues[nameof(IAuditable.UpdatedUserId)] = currentUser;
                    item.CurrentValues[nameof(IAuditable.UpdatedTimeStamp)] = currentDateTime;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Discriminators
            modelBuilder.Entity<Address>()
                .HasDiscriminator<AddressType>("AddressType")
                .HasValue<PhysicalAddress>(AddressType.Physical)
                .HasValue<MailingAddress>(AddressType.Mailing);
            #endregion

            #region IAuditable
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditable).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.FindProperty(nameof(IAuditable.CreatedUserId))
                        .AfterSaveBehavior = PropertySaveBehavior.Ignore;
                    entityType.FindProperty(nameof(IAuditable.CreatedTimeStamp))
                        .AfterSaveBehavior = PropertySaveBehavior.Ignore;
                }
            }
            #endregion

            #region CollegeSeed
            modelBuilder.Entity<College>().HasData(
                new College { Code = 1, Name = "College of Physicians and Surgeons of BC (CPSBC)", Prefix = "91", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new College { Code = 2, Name = "College of Pharmacists of BC (CPBC)", Prefix = "P1", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new College { Code = 3, Name = "College of Registered Nurses of BC (CRNBC)", Prefix = "96", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new College { Code = 4, Name = "None", Prefix = null, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now }
            );
            #endregion

            #region LicenseSeed
            modelBuilder.Entity<License>().HasData(
                new License { Code = 1, Name = "Full - General", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new License { Code = 2, Name = "Full - Pharmacist", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new License { Code = 3, Name = "Full - Specialty", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new License { Code = 4, Name = "Registered Nurse", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new License { Code = 5, Name = "Temporary Registered Nurse", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now }
                );
            #endregion

            #region CollegeLicenseSeed
            modelBuilder.Entity<CollegeLicense>().HasData(
                new CollegeLicense { CollegeCode = 1, LicenseCode = 2, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 3, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new CollegeLicense { CollegeCode = 2, LicenseCode = 4, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new CollegeLicense { CollegeCode = 2, LicenseCode = 5, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 1, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 5, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now }
                );
            #endregion

            #region PracticeSeed
            modelBuilder.Entity<Practice>().HasData(
                new Practice { Code = 1, Name = "Remote Practice", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new Practice { Code = 2, Name = "Reproductive Care", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new Practice { Code = 3, Name = "Sexually Transmitted Infections (STI)", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new Practice { Code = 4, Name = "None", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now }
                );
            #endregion

            #region JobNameSeed
            modelBuilder.Entity<JobName>().HasData(
                new JobName { Code = 1, Name = "Medical Office Assistant", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new JobName { Code = 2, Name = "Midwife", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new JobName { Code = 3, Name = "Nurse (not nurse practitioner)", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new JobName { Code = 4, Name = "Pharmacy Assistant", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new JobName { Code = 5, Name = "Pharmacy Technician", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new JobName { Code = 6, Name = "Registration Clerk", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new JobName { Code = 7, Name = "Ward Clerk", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new JobName { Code = 8, Name = "Other", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now }
                );
            #endregion

            #region OrganizationNameSeed
            modelBuilder.Entity<OrganizationName>().HasData(
                new OrganizationName { Code = 1, Name = "Vancouver Island Health", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new OrganizationName { Code = 2, Name = "Shoppers Drug Mart", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now }
                );
            #endregion

            #region OrganizationTypeSeed
            modelBuilder.Entity<OrganizationType>().HasData(
                new OrganizationType { Code = 1, Name = "Health Authority", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now },
                new OrganizationType { Code = 2, Name = "Pharmacy", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = DateTime.Now, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = DateTime.Now }
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
            modelBuilder.Entity<CollegeLicense>()
                .HasKey(cl => new { cl.CollegeCode, cl.LicenseCode });
            modelBuilder.Entity<CollegeLicense>()
                .HasOne(cl => cl.College)
                .WithMany(c => c.CollegeLicenses)
                .HasForeignKey(cl => cl.CollegeCode);
            modelBuilder.Entity<CollegeLicense>()
                .HasOne(cl => cl.License)
                .WithMany(l => l.CollegeLicenses)
                .HasForeignKey(cl => cl.LicenseCode);
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

        public static IQueryable<T> Set<T>(this DbContext context, params Expression<Func<T, object>>[] includes) where T : class
        {
            // Get the generic type definition 
            MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);

            // Build a method with the specific type argument you're interested in 
            method = method.MakeGenericMethod(typeof(T));

            var query = method.Invoke(context, null) as IQueryable<T>;

            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
