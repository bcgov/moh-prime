using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Prime.Configuration.Environment;
using Prime.Models;
using Prime.Models.HealthAuthorities;
using Prime.Models.VerifiableCredentials;

namespace Prime
{
    // Allow for design time creation of the ApiDbContext
    public class ApiDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
    {
        public ApiDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Add(new PrimeEnvironmentVariablesConfigurationSource())
                .Build();

            var connectionString = config.GetConnectionString("PrimeDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);

            return new ApiDbContext(optionsBuilder.Options, null);
        }
    }

    public class ApiDbContext : DbContext
    {
        private readonly IHttpContextAccessor _context;

        public ApiDbContext(
            DbContextOptions<ApiDbContext> options,
            IHttpContextAccessor context
            ) : base(options)
        {
            _context = context;
        }

        public DbSet<ClientLog> ClientLogs { get; set; }

        // Enrollee Jobs have been migrated onto OboSites, but we are not dropping the old table for the time being
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Enrollee> Enrollees { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<EnrolmentStatus> EnrolmentStatuses { get; set; }
        public DbSet<EnrolmentCertificateAccessToken> EnrolmentCertificateAccessTokens { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<DefaultPrivilege> DefaultPrivileges { get; set; }
        public DbSet<AssignedPrivilege> AssignedPrivileges { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<EnrolleeNote> EnrolleeNotes { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<SiteRegistrationNote> SiteRegistrationNotes { get; set; }

        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<AgreementVersion> AgreementVersions { get; set; }

        public DbSet<BusinessEvent> BusinessEvents { get; set; }
        public DbSet<EnrolleeRemoteUser> EnrolleeRemoteUsers { get; set; }
        public DbSet<RemoteAccessSite> RemoteAccessSites { get; set; }
        public DbSet<EnrolleeAbsence> EnrolleeAbsences { get; set; }

        // Site Registration
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<CommunitySite> CommunitySites { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<RemoteUser> RemoteUsers { get; set; }
        public DbSet<RemoteUserCertification> RemoteUserCertifications { get; set; }
        public DbSet<EnrolmentStatusReference> EnrolmentStatusReference { get; set; }
        public DbSet<BusinessLicenceDocument> BusinessLicenceDocuments { get; set; }
        public DbSet<BusinessLicence> BusinessLicences { get; set; }
        public DbSet<SignedAgreementDocument> SignedAgreementDocuments { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<OrganizationClaim> OrganizationClaims { get; set; }

        // Health Authorities
        public DbSet<V2HealthAuthoritySite> V2HealthAuthoritySites { get; set; }
        public DbSet<HealthAuthorityOrganization> HealthAuthorities { get; set; }
        public DbSet<HealthAuthorityCareType> HealthAuthorityCareTypes { get; set; }
        public DbSet<HealthAuthorityContact> HealthAuthorityContacts { get; set; }
        public DbSet<HealthAuthorityVendor> HealthAuthorityVendors { get; set; }
        public DbSet<PrivacyOffice> PrivacyOffices { get; set; }
        public DbSet<HealthAuthoritySite> HealthAuthoritySites { get; set; }


        public DbSet<SelfDeclarationDocument> SelfDeclarationDocuments { get; set; }
        public DbSet<IdentificationDocument> IdentificationDocuments { get; set; }
        public DbSet<EnrolleeAdjudicationDocument> EnrolleeAdjudicationDocuments { get; set; }
        public DbSet<SiteAdjudicationDocument> SiteAdjudicationDocuments { get; set; }
        public DbSet<SiteRegistrationReviewDocument> SiteRegistrationReviewDocuments { get; set; }
        public DbSet<DocumentAccessToken> DocumentAccessTokens { get; set; }

        public DbSet<PreApprovedRegistration> PreApprovedRegistrations { get; set; }
        public DbSet<EnrolleeNotification> EnrolleeNotifications { get; set; }
        public DbSet<SiteNotification> SiteNotifications { get; set; }
        public DbSet<AuthorizedUser> AuthorizedUsers { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Banner> Banners { get; set; }

        // Parties
        public DbSet<Party> Parties { get; set; }
        public DbSet<PartySubmission> PartySubmissions { get; set; }
        public DbSet<PartyCertification> PartyCertifications { get; set; }

        // PLR Integration
        public DbSet<PlrProvider> PlrProviders { get; set; }


        // GIS
        public DbSet<GisEnrolment> GisEnrolments { get; set; }
        public DbSet<EnrolleeLinkedEnrolment> EnrolleeLinkedEnrolments { get; set; }

        public override int SaveChanges()
        {
            ApplyAudits();

            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAudits();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAudits()
        {
            ChangeTracker.DetectChanges();
            var updated = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseAuditable
                    && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUser = _context?.HttpContext?.User.GetPrimeUserId() ?? Guid.Empty;
            var currentTime = DateTimeOffset.Now;

            foreach (var entry in updated)
            {
                entry.CurrentValues[nameof(BaseAuditable.UpdatedUserId)] = currentUser;
                entry.CurrentValues[nameof(BaseAuditable.UpdatedTimeStamp)] = currentTime;

                if (entry.State == EntityState.Added)
                {
                    entry.CurrentValues[nameof(BaseAuditable.CreatedUserId)] = currentUser;
                    entry.CurrentValues[nameof(BaseAuditable.CreatedTimeStamp)] = currentTime;
                }
                else
                {
                    entry.Property(nameof(BaseAuditable.CreatedUserId)).IsModified = false;
                    entry.Property(nameof(BaseAuditable.CreatedTimeStamp)).IsModified = false;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
        }

        // Uncomment for DB logging
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //     => optionsBuilder.UseLoggerFactory(DbLoggerFactory);

        // public static readonly Microsoft.Extensions.Logging.ILoggerFactory DbLoggerFactory
        //     = new Microsoft.Extensions.Logging.LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });
    }
}
