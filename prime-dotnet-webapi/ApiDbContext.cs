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
using Prime.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace Prime
{
    // Allow for design time creation of the ApiDbContext
    public class ApiDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
    {
        public ApiDbContext CreateDbContext(string[] args)
        {
            // Connect to database
            var connectionString = System.Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if (connectionString == null)
            {
                connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);

            return new ApiDbContext(optionsBuilder.Options, null);
        }
    }

    public class ApiDbContext : DbContext
    {
        private readonly DateTime SEEDING_DATE = DateTime.Now;

        private readonly IHttpContextAccessor _context;

        public ApiDbContext(
            DbContextOptions<ApiDbContext> options,
            IHttpContextAccessor context
            ) : base(options)
        {
            _context = context;
        }

        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationType> OrganizationTypes { get; set; }
        public DbSet<Enrollee> Enrollees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<EnrolmentStatus> EnrolmentStatuses { get; set; }
        public DbSet<EnrolmentStatusReason> EnrolmentStatusReasons { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<EnrolmentCertificateAccessToken> EnrolmentCertificateAccessTokens { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<DefaultPrivilege> DefaultPrivileges { get; set; }
        public DbSet<AssignedPrivilege> AssignedPrivileges { get; set; }
        public DbSet<EnrolleeProfileVersion> EnrolleeProfileVersions { get; set; }

        public DbSet<AdjudicatorNote> AdjudicatorNotes { get; set; }
        public DbSet<AccessAgreementNote> AccessAgreementNotes { get; set; }
        public DbSet<EnrolmentCertificateNote> EnrolmentCertificateNotes { get; set; }

        public DbSet<AccessTerm> AccessTerms { get; set; }
        public DbSet<GlobalClause> GlobalClauses { get; set; }
        public DbSet<UserClause> UserClauses { get; set; }
        public DbSet<LicenseClassClause> LicenseClassClauses { get; set; }
        public DbSet<LimitsConditionsClause> LimitsConditionsClauses { get; set; }

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
            var currentUser = _context?.HttpContext?.User.GetPrimeUserId() ?? Guid.Empty;
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
                        .SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                    entityType.FindProperty(nameof(IAuditable.CreatedTimeStamp))
                        .SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
                }
            }
            #endregion

            modelBuilder.ApplyConfiguration(new CollegeConfiguration());
            modelBuilder.ApplyConfiguration(new LicenseConfiguration());
            modelBuilder.ApplyConfiguration(new CollegeLicenseConfiguration());
            modelBuilder.ApplyConfiguration(new PracticeConfiguration());
            modelBuilder.ApplyConfiguration(new CollegePracticeConfiguration());
            modelBuilder.ApplyConfiguration(new JobNameConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            modelBuilder.ApplyConfiguration(new StatusReasonConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new ProvinceConfiguration());

            modelBuilder.ApplyConfiguration(new PrivilegeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PrivilegeGroupConfiguration());
            modelBuilder.ApplyConfiguration(new PrivilegeConfiguration());
            modelBuilder.ApplyConfiguration(new DefaultPrivilegeConfiguration());
            modelBuilder.ApplyConfiguration(new AssignedPrivilegeConfiguration());
            modelBuilder.ApplyConfiguration(new EnrolleeProfileVersionConfiguration());

            modelBuilder.ApplyConfiguration(new GlobalClauseConfiguration());
            modelBuilder.ApplyConfiguration(new UserClauseConfiguration());
            modelBuilder.ApplyConfiguration(new LicenseClassClauseConfiguration());

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
            #endregion

            #region Relationships
            modelBuilder.Entity<EnrolmentStatus>()
                .HasOne(es => es.Enrollee)
                .WithMany(e => e.EnrolmentStatuses)
                .HasForeignKey(es => es.EnrolleeId);
            modelBuilder.Entity<EnrolmentStatus>()
                .HasOne(es => es.Status)
                .WithMany(s => s.EnrolmentStatuses)
                .HasForeignKey(es => es.StatusCode);

            modelBuilder.Entity<EnrolmentStatusReason>()
                .HasOne(esr => esr.EnrolmentStatus)
                .WithMany(es => es.EnrolmentStatusReasons)
                .HasForeignKey(esr => esr.EnrolmentStatusId);
            modelBuilder.Entity<EnrolmentStatusReason>()
                .HasOne(esr => esr.StatusReason)
                .WithMany(sr => sr.EnrolmentStatusReasons)
                .HasForeignKey(esr => esr.StatusReasonCode);

            modelBuilder.Entity<AdjudicatorNote>()
                .HasOne(an => an.Enrollee)
                .WithMany(e => e.AdjudicatorNotes)
                .HasForeignKey(an => an.EnrolleeId);

            modelBuilder.Entity<AccessTerm>()
                .HasOne(toa => toa.Enrollee)
                .WithMany(e => e.AccessTerms)
                .HasForeignKey(toa => toa.EnrolleeId);

            modelBuilder.Entity<AccessTermLicenseClassClause>()
                .HasKey(tlic => new { tlic.AccessTermId, tlic.LicenseClassClauseId });
            modelBuilder.Entity<AccessTermLicenseClassClause>()
                .HasOne(tlic => tlic.AccessTerm)
                .WithMany(toa => toa.AccessTermLicenseClassClauses)
                .HasForeignKey(tlic => tlic.AccessTermId);
            modelBuilder.Entity<AccessTermLicenseClassClause>()
                .HasOne(tlic => tlic.LicenseClassClause)
                .WithMany(lcc => lcc.AccessTermLicenseClassClauses)
                .HasForeignKey(tlic => tlic.LicenseClassClauseId);
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
