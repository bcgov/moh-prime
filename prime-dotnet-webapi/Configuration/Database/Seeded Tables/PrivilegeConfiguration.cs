/*
 * Ministry of Health PRIME Project
 * Approved for Ministry of Health use only.
 */
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class PrivilegeConfiguration : SeededTable<Privilege>
    {
        public override IEnumerable<Privilege> SeedData
        {
            get
            {
                return new[] {
                    new Privilege { Id = 1,  PrivilegeGroupCode = 1, TransactionType = "TAC",          Description = "Update Claims History"             },
                    new Privilege { Id = 2,  PrivilegeGroupCode = 1, TransactionType = "TDT",          Description = "Query Claims History"              },
                    new Privilege { Id = 3,  PrivilegeGroupCode = 1, TransactionType = "TPM",          Description = "Pt Profile Mail Request"           },
                    new Privilege { Id = 4,  PrivilegeGroupCode = 1, TransactionType = "TCP",          Description = "Maintain Pt Keyword"               },
                    new Privilege { Id = 5,  PrivilegeGroupCode = 2, TransactionType = "TPH",          Description = "New PHN"                           },
                    new Privilege { Id = 6,  PrivilegeGroupCode = 2, TransactionType = "TPA",          Description = "Address Update"                    },
                    new Privilege { Id = 7,  PrivilegeGroupCode = 2, TransactionType = "TMU",          Description = "Medication Update"                 },
                    new Privilege { Id = 8,  PrivilegeGroupCode = 3, TransactionType = "TDR",          Description = "Drug Monograph"                    },
                    new Privilege { Id = 9,  PrivilegeGroupCode = 3, TransactionType = "TID",          Description = "Patient Details"                   },
                    new Privilege { Id = 10, PrivilegeGroupCode = 3, TransactionType = "TIL",          Description = "Location Details"                  },
                    new Privilege { Id = 11, PrivilegeGroupCode = 3, TransactionType = "TIP",          Description = "Prescriber Details"                },
                    new Privilege { Id = 12, PrivilegeGroupCode = 3, TransactionType = "TPN",          Description = "Name Search"                       },
                    new Privilege { Id = 13, PrivilegeGroupCode = 3, TransactionType = "TRP",          Description = "Pt Profile Request"                },
                    new Privilege { Id = 14, PrivilegeGroupCode = 3, TransactionType = "TBR",          Description = "Most Recent Profile"               },
                    new Privilege { Id = 15, PrivilegeGroupCode = 3, TransactionType = "TRS",          Description = "Filled Elsewhere Profile"          },
                    new Privilege { Id = 16, PrivilegeGroupCode = 3, TransactionType = "TDU",          Description = "DUE Inquiry"                       },
                    new Privilege { Id = 19, PrivilegeGroupCode = 5, TransactionType = "RU with OBOs", Description = "Regulated User that can have OBOs" }
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
