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

        private readonly DateTime SEEDING_DATE = DateTime.Now;

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
        public DbSet<EnrolmentStatus> EnrolmentStatuses { get; set; }
        public DbSet<EnrolmentStatusReason> EnrolmentStatusReasons { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAudits();

            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
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
            var currentDateTime = SEEDING_DATE;

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
                new College { Code = 1, Name = "College of Physicians and Surgeons of BC (CPSBC)", Prefix = "91", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new College { Code = 2, Name = "College of Pharmacists of BC (CPBC)", Prefix = "P1", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new College { Code = 3, Name = "College of Registered Nurses of BC (CRNBC)", Prefix = "96", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
            );
            #endregion

            #region LicenseSeed
            modelBuilder.Entity<License>().HasData(
                new License { Code = 1, Name = "Full - General", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new License { Code = 2, Name = "Full - Pharmacist", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new License { Code = 3, Name = "Full - Specialty", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new License { Code = 4, Name = "Registered Nurse", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new License { Code = 5, Name = "Temporary Registered Nurse", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
                );
            #endregion

            #region CollegeLicenseSeed
            modelBuilder.Entity<CollegeLicense>().HasData(
                new CollegeLicense { CollegeCode = 1, LicenseCode = 2, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 3, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new CollegeLicense { CollegeCode = 2, LicenseCode = 4, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new CollegeLicense { CollegeCode = 2, LicenseCode = 5, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 1, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 5, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
                );
            #endregion

            #region PracticeSeed
            modelBuilder.Entity<Practice>().HasData(
                new Practice { Code = 1, Name = "Remote Practice", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Practice { Code = 2, Name = "Reproductive Care", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Practice { Code = 3, Name = "Sexually Transmitted Infections (STI)", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
                // new Practice { Code = 4, Name = "None", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
                );
            #endregion

            #region CollegePracticeSeed
            modelBuilder.Entity<CollegePractice>().HasData(
                new CollegePractice { CollegeCode = 1, PracticeCode = 1, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new CollegePractice { CollegeCode = 1, PracticeCode = 2, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new CollegePractice { CollegeCode = 1, PracticeCode = 3, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                // new CollegePractice { CollegeCode = 1, PracticeCode = 4, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new CollegePractice { CollegeCode = 2, PracticeCode = 1, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new CollegePractice { CollegeCode = 2, PracticeCode = 2, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new CollegePractice { CollegeCode = 2, PracticeCode = 3, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                // new CollegePractice { CollegeCode = 2, PracticeCode = 4, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new CollegePractice { CollegeCode = 3, PracticeCode = 1, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new CollegePractice { CollegeCode = 3, PracticeCode = 2, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new CollegePractice { CollegeCode = 3, PracticeCode = 3, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
                // new CollegePractice { CollegeCode = 3, PracticeCode = 4, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
                );
            #endregion

            #region JobNameSeed
            modelBuilder.Entity<JobName>().HasData(
                new JobName { Code = 1, Name = "Medical Office Assistant", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new JobName { Code = 2, Name = "Pharmacy Assistant", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new JobName { Code = 3, Name = "Registration Clerk", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new JobName { Code = 4, Name = "Ward Clerk", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }                );
            #endregion

            #region OrganizationNameSeed
            modelBuilder.Entity<OrganizationName>().HasData(
                new OrganizationName { Code = 1, Name = "Vancouver Island Health", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new OrganizationName { Code = 2, Name = "Shoppers Drug Mart", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
                );
            #endregion

            #region OrganizationTypeSeed
            modelBuilder.Entity<OrganizationType>().HasData(
                new OrganizationType { Code = 1, Name = "Health Authority", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new OrganizationType { Code = 2, Name = "Pharmacy", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
                );
            #endregion

            #region StatusSeed
            modelBuilder.Entity<Status>().HasData(
                new Status { Code = 1, Name = "In Progress", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Status { Code = 2, Name = "Submitted", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Status { Code = 3, Name = "Adjudicated/Approved", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Status { Code = 4, Name = "Declined", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Status { Code = 5, Name = "Accepted Access Agreement", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Status { Code = 6, Name = "Declined Access Agreement", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
                );
            #endregion

            #region StatusReasonSeed
            modelBuilder.Entity<StatusReason>().HasData(
                new StatusReason { Code = 1, Name = "Automatic", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 2, Name = "Manual", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 3, Name = "Name Discrepancy", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 4, Name = "Not in PharmaNet", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 5, Name = "Insulin Pump Provider", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 6, Name = "Licence Class", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 7, Name = "Self Declaration", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 8, Name = "Contact address or Identity Address Out of British Columbia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
                 );
            #endregion
 
            #region CountrySeed
            modelBuilder.Entity<Country>().HasData(
                new Country { Code = "CA", Name = "Canada", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Country { Code = "US", Name = "United States", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
                );
            #endregion

            #region ProvinceSeed
            modelBuilder.Entity<Province>().HasData(
                new Province { Code = "AB", CountryCode = "CA", Name = "Alberta", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "BC", CountryCode = "CA", Name = "British Columbia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MB", CountryCode = "CA", Name = "Manitoba", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NB", CountryCode = "CA", Name = "New Brunswick", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NL", CountryCode = "CA", Name = "Newfoundland and Labrador", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NS", CountryCode = "CA", Name = "Nova Scotia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "ON", CountryCode = "CA", Name = "Ontario", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "PE", CountryCode = "CA", Name = "Prince Edward Island", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "QC", CountryCode = "CA", Name = "Quebec", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "SK", CountryCode = "CA", Name = "Saskatchewan", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NT", CountryCode = "CA", Name = "Northwest Territories", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NU", CountryCode = "CA", Name = "Nunavut", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "YT", CountryCode = "CA", Name = "Yukon", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "AL", CountryCode = "US", Name = "Alabama", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "AK", CountryCode = "US", Name = "Alaska", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "AS", CountryCode = "US", Name = "American Samoa", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "AZ", CountryCode = "US", Name = "Arizona", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "AR", CountryCode = "US", Name = "Arkansas", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "CA", CountryCode = "US", Name = "California", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "CO", CountryCode = "US", Name = "Colorado", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "CT", CountryCode = "US", Name = "Connecticut", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "DE", CountryCode = "US", Name = "Delaware", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "DC", CountryCode = "US", Name = "District of Columbia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "FL", CountryCode = "US", Name = "Florida", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "GA", CountryCode = "US", Name = "Georgia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "GU", CountryCode = "US", Name = "Guam", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "HI", CountryCode = "US", Name = "Hawaii", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "ID", CountryCode = "US", Name = "Idaho", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "IL", CountryCode = "US", Name = "Illinois", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "IN", CountryCode = "US", Name = "Indiana", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "IA", CountryCode = "US", Name = "Iowa", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "KS", CountryCode = "US", Name = "Kansas", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "KY", CountryCode = "US", Name = "Kentucky", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "LA", CountryCode = "US", Name = "Louisiana", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "ME", CountryCode = "US", Name = "Maine", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MD", CountryCode = "US", Name = "Maryland", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MA", CountryCode = "US", Name = "Massachusetts", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MI", CountryCode = "US", Name = "Michigan", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MN", CountryCode = "US", Name = "Minnesota", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MS", CountryCode = "US", Name = "Mississippi", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MO", CountryCode = "US", Name = "Missouri", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MT", CountryCode = "US", Name = "Montana", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NE", CountryCode = "US", Name = "Nebraska", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NV", CountryCode = "US", Name = "Nevada", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NH", CountryCode = "US", Name = "New Hampshire", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NJ", CountryCode = "US", Name = "New Jersey", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NM", CountryCode = "US", Name = "New Mexico", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NY", CountryCode = "US", Name = "New York", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NC", CountryCode = "US", Name = "North Carolina", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "ND", CountryCode = "US", Name = "North Dakota", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MP", CountryCode = "US", Name = "Northern Mariana Islands", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "OH", CountryCode = "US", Name = "Ohio", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "OK", CountryCode = "US", Name = "Oklahoma", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "OR", CountryCode = "US", Name = "Oregon", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "PA", CountryCode = "US", Name = "Pennsylvania", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "PR", CountryCode = "US", Name = "Puerto Rico", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "RI", CountryCode = "US", Name = "Rhode Island", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "SC", CountryCode = "US", Name = "South Carolina", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "SD", CountryCode = "US", Name = "South Dakota", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "TN", CountryCode = "US", Name = "Tennessee", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "TX", CountryCode = "US", Name = "Texas", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "UM", CountryCode = "US", Name = "United States Minor Outlying Islands", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "UT", CountryCode = "US", Name = "Utah", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "VT", CountryCode = "US", Name = "Vermont", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "VI", CountryCode = "US", Name = "Virgin Islands, U.S.", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "VA", CountryCode = "US", Name = "Virginia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "WA", CountryCode = "US", Name = "Washington", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "WV", CountryCode = "US", Name = "West Virginia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "WI", CountryCode = "US", Name = "Wisconsin", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "WY", CountryCode = "US", Name = "Wyoming", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
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
                .IsUnique();

            modelBuilder.Entity<Enrollee>()
                .HasIndex("UserId")
                .IsUnique();

            modelBuilder.Entity<Enrolment>()
                .HasIndex("EnrolleeId")
                .IsUnique();
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

            modelBuilder.Entity<CollegePractice>()
                .HasKey(cp => new { cp.CollegeCode, cp.PracticeCode });
            modelBuilder.Entity<CollegePractice>()
                .HasOne(cp => cp.College)
                .WithMany(c => c.CollegePractices)
                .HasForeignKey(cp => cp.CollegeCode);
            modelBuilder.Entity<CollegePractice>()
                .HasOne(cp => cp.Practice)
                .WithMany(p => p.CollegePractices)
                .HasForeignKey(cp => cp.PracticeCode);

            modelBuilder.Entity<EnrolmentStatus>()
                .HasKey(es => new { es.EnrolmentId, es.StatusCode });
            modelBuilder.Entity<EnrolmentStatus>()
                .HasOne(es => es.Enrolment)
                .WithMany(e => e.EnrolmentStatuses)
                .HasForeignKey(es => es.EnrolmentId);
            modelBuilder.Entity<EnrolmentStatus>()
                .HasOne(es => es.Status)
                .WithMany(s => s.EnrolmentStatuses)
                .HasForeignKey(es => es.StatusCode);

            modelBuilder.Entity<EnrolmentStatusReason>()
                .HasKey(esr => new { esr.EnrolmentId, esr.StatusCode, esr.StatusReasonCode });
            modelBuilder.Entity<EnrolmentStatusReason>()
                .HasOne(esr => esr.EnrolmentStatus)
                .WithMany(es => es.EnrolmentStatusReasons)
                .HasForeignKey(esr => new { esr.EnrolmentId, esr.StatusCode });
            modelBuilder.Entity<EnrolmentStatusReason>()
                .HasOne(esr => esr.StatusReason)
                .WithMany(sr => sr.EnrolmentStatusReasons)
                .HasForeignKey(esr => esr.StatusReasonCode);
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
