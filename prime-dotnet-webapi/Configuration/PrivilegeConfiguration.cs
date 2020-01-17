/*
 * Ministry of Health PRIME Project
 * Approved for Ministry of Health use only.
 */
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class PrivilegeConfiguration : SeededTable<Privilege>
    {
        public override ICollection<Privilege> SeedData
        {
            get
            {
                return new[] {
                    new Privilege { Id = 1,  PrivilegeGroupCode = 1, TransactionType = "TAC", Description = "Update Claims History", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 2,  PrivilegeGroupCode = 1, TransactionType = "TDT", Description = "Query Claims History", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 3,  PrivilegeGroupCode = 1, TransactionType = "TPM", Description = "Pt Profile Mail Request", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 4,  PrivilegeGroupCode = 1, TransactionType = "TCP", Description = "Maintain Pt Keyword", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 5,  PrivilegeGroupCode = 2, TransactionType = "TPH", Description = "New PHN", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 6,  PrivilegeGroupCode = 2, TransactionType = "TPA", Description = "Address Update", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 7,  PrivilegeGroupCode = 2, TransactionType = "TMU", Description = "Medication Update", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 8,  PrivilegeGroupCode = 3, TransactionType = "TDR", Description = "Drug Monograph", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 9,  PrivilegeGroupCode = 3, TransactionType = "TID", Description = "Patient Details", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 10, PrivilegeGroupCode = 3, TransactionType = "TIL", Description = "Location Details", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 11, PrivilegeGroupCode = 3, TransactionType = "TIP", Description = "Prescriber Details", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 12, PrivilegeGroupCode = 3, TransactionType = "TPN", Description = "Name Search", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 13, PrivilegeGroupCode = 3, TransactionType = "TRP", Description = "Pt Profile Request", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 14, PrivilegeGroupCode = 3, TransactionType = "TBR", Description = "Most Recent Profile", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 15, PrivilegeGroupCode = 3, TransactionType = "TRS", Description = "Filled Elsewhere Profile", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 16, PrivilegeGroupCode = 3, TransactionType = "TDU", Description = "DUE Inquiry", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 17, PrivilegeGroupCode = 4, TransactionType = "RU",  Description = "Registered User", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 18, PrivilegeGroupCode = 4, TransactionType = "OBO", Description = "On Behalf of User", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Privilege { Id = 19, PrivilegeGroupCode = 5, TransactionType = "RU with OBO's", Description = "Registered User that can have OBO's", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }

        public override void Configure(EntityTypeBuilder<Privilege> builder)
        {
            builder.HasOne(p => p.PrivilegeGroup)
                    .WithMany(pg => pg.Privileges)
                    .HasForeignKey(p => p.PrivilegeGroupCode);

            builder.HasData(SeedData);
        }
    }
}
