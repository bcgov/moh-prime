/*
 * Ministry of Health PRIME Project
 * Approved for Ministry of Health use only.
 */
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class DefaultPrivilegeConfiguration : SeededTable<DefaultPrivilege>
    {
        public override ICollection<DefaultPrivilege> SeedData
        {
            get
            {
                return new[] {
                    // Full Pharmacist
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 1,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 2,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 3,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 4,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Limited Pharmacist
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 1,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 2,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 3,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 4,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Temporary Pharmacist
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 1,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 2,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 3,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 4,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Student Pharmacist
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 1,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 2,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 3,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 4,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Full - family
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Full - specialty
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Special
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Osteopathic
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Provisional - family
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Provisional - specialty
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Academic
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Conditional - practice limitations
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Conditional - practice setting
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Conditional - disciplined
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Educational - postgraduate resident
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Educational - postgraduate resident elective
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Educational - postgraduate fellow
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Educational - postgraduate trainee
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Emergency - family
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Emergency - specialty
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Assessment
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Visitor
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Surgical assistant
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Practicing Nurse Practitioner
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Provisional Nurse Practitioner
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Temporary Nurse Practitioner (special event)
                    new DefaultPrivilege { LicenseCode = 50, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 50, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 50, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 50, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 50, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 50, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 50, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 50, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 50, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 50, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 50, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 50, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Temporary Nurse Practitioner (emergency)
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Temporary Nurse Practitioner (time-limited)
                    new DefaultPrivilege { LicenseCode = 58, PrivilegeId = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 58, PrivilegeId = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 58, PrivilegeId = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 58, PrivilegeId = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 58, PrivilegeId = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 58, PrivilegeId = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 58, PrivilegeId = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 58, PrivilegeId = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 58, PrivilegeId = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 58, PrivilegeId = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 58, PrivilegeId = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new DefaultPrivilege { LicenseCode = 58, PrivilegeId = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                };
            }
        }

        public override void Configure(EntityTypeBuilder<DefaultPrivilege> builder)
        {
            builder.HasKey(dp => new { dp.PrivilegeId, dp.LicenseCode });
            builder
                .HasOne<Privilege>(dp => dp.Privilege)
                .WithMany(p => p.DefaultPrivileges)
                .HasForeignKey(dp => dp.PrivilegeId);
            builder
                .HasOne<License>(dp => dp.License)
                .WithMany(l => l.DefaultPrivileges)
                .HasForeignKey(dp => dp.LicenseCode);

            builder.HasData(SeedData);
        }
    }
}
