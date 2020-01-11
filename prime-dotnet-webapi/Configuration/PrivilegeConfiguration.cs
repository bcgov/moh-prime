/*
 * Ministry of Health PRIME Project
 * Approved for Ministry of Health use only.
 */
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;
namespace Prime.Configuration
{
    public class PrivilegeConfiguration : IEntityTypeConfiguration<Privilege>
    {
        public void Configure(EntityTypeBuilder<Privilege> builder)
        {
            builder.HasOne(p => p.PrivilegeGroup)
                    .WithMany(pg => pg.Privileges)
                    .HasForeignKey(p => p.PrivilegeGroupId);

            builder.HasData(
                new Privilege { Id = 1, PrivilegeGroupId = 1, TransactionType = "TAC", Description = "Update Claims History", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 2, PrivilegeGroupId = 1, TransactionType = "TDT", Description = "Query Claims History", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 3, PrivilegeGroupId = 1, TransactionType = "TPM", Description = "Pt Profile Mail Request", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 4, PrivilegeGroupId = 1, TransactionType = "TCP", Description = "Maintain Pt Keyword", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 5, PrivilegeGroupId = 2, TransactionType = "TPH", Description = "New PHN", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 6, PrivilegeGroupId = 2, TransactionType = "TPA", Description = "Address Update", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 7, PrivilegeGroupId = 2, TransactionType = "TMU", Description = "Medication Update", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 8, PrivilegeGroupId = 3, TransactionType = "TDR", Description = "Drug Monograph", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 9, PrivilegeGroupId = 3, TransactionType = "TID", Description = "Patient Details", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 10, PrivilegeGroupId = 3, TransactionType = "TIL", Description = "Location Details", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 11, PrivilegeGroupId = 3, TransactionType = "TIP", Description = "Prescriber Details", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 12, PrivilegeGroupId = 3, TransactionType = "TPN", Description = "Name Search", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 13, PrivilegeGroupId = 3, TransactionType = "TRP", Description = "Pt Profile Request", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 14, PrivilegeGroupId = 3, TransactionType = "TBR", Description = "Most Recent Profile", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 15, PrivilegeGroupId = 3, TransactionType = "TRS", Description = "Filled Elsewhere Profile", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 16, PrivilegeGroupId = 3, TransactionType = "TDU", Description = "DUE Inquiry", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 17, PrivilegeGroupId = 4, TransactionType = "RU", Description = "Can be RU (OBO)", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Privilege { Id = 18, PrivilegeGroupId = 5, TransactionType = "OBO", Description = "Can be OBO (RU)", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE }
            );
        }
    }
}
