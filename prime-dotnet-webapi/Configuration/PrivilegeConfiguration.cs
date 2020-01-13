/*
 * Ministry of Health PRIME Project
 * Approved for Ministry of Health use only.
 */
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;
namespace Prime.Configuration
{
    public class PrivilegeConfiguration : IEntityTypeConfiguration<Privilege>
    {
        private readonly Guid SYSTEM_USER = Guid.Empty;
        private readonly DateTime SEEDING_DATE = DateTime.Now;

        public void Configure(EntityTypeBuilder<Privilege> builder)
        {
            builder.HasOne(p => p.PrivilegeGroup)
                    .WithMany(pg => pg.Privileges)
                    .HasForeignKey(p => p.PrivilegeGroupCode);

            builder.HasData(
                new Privilege { Id = 1, PrivilegeGroupCode = 1, TransactionType = "TAC", Description = "Update Claims History", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 2, PrivilegeGroupCode = 1, TransactionType = "TDT", Description = "Query Claims History", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 3, PrivilegeGroupCode = 1, TransactionType = "TPM", Description = "Pt Profile Mail Request", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 4, PrivilegeGroupCode = 1, TransactionType = "TCP", Description = "Maintain Pt Keyword", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 5, PrivilegeGroupCode = 2, TransactionType = "TPH", Description = "New PHN", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 6, PrivilegeGroupCode = 2, TransactionType = "TPA", Description = "Address Update", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 7, PrivilegeGroupCode = 2, TransactionType = "TMU", Description = "Medication Update", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 8, PrivilegeGroupCode = 3, TransactionType = "TDR", Description = "Drug Monograph", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 9, PrivilegeGroupCode = 3, TransactionType = "TID", Description = "Patient Details", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 10, PrivilegeGroupCode = 3, TransactionType = "TIL", Description = "Location Details", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 11, PrivilegeGroupCode = 3, TransactionType = "TIP", Description = "Prescriber Details", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 12, PrivilegeGroupCode = 3, TransactionType = "TPN", Description = "Name Search", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 13, PrivilegeGroupCode = 3, TransactionType = "TRP", Description = "Pt Profile Request", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 14, PrivilegeGroupCode = 3, TransactionType = "TBR", Description = "Most Recent Profile", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 15, PrivilegeGroupCode = 3, TransactionType = "TRS", Description = "Filled Elsewhere Profile", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 16, PrivilegeGroupCode = 3, TransactionType = "TDU", Description = "DUE Inquiry", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 17, PrivilegeGroupCode = 4, TransactionType = "RU", Description = "Registered User", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 18, PrivilegeGroupCode = 4, TransactionType = "OBO", Description = "On Behalf Of", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Privilege { Id = 19, PrivilegeGroupCode = 5, TransactionType = "RU with OBOs", Description = "Registered User That Can Have OBO's", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
            );
        }
    }
}
