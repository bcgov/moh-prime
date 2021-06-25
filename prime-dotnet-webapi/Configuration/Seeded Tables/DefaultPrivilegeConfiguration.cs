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
        public override IEnumerable<DefaultPrivilege> SeedData
        {
            get
            {
                return new[] {
                    // Full Pharmacist
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 1 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 2 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 3 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 4 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 19 },

                    // Limited Pharmacist
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 1 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 2 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 3 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 4 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 26, PrivilegeId = 19 },

                    // Temporary Pharmacist
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 1 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 2 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 3 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 4 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 27, PrivilegeId = 19 },

                    // Student Pharmacist
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 1 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 2 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 3 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 4 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 28, PrivilegeId = 16 },

                    // Full - family
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 1, PrivilegeId = 19 },

                    // Full - specialty
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 2, PrivilegeId = 19 },

                    // Special
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 3, PrivilegeId = 19 },

                    // Osteopathic
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 4, PrivilegeId = 19 },

                    // Provisional - family
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 5, PrivilegeId = 19 },

                    // Provisional - specialty
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 6, PrivilegeId = 19 },

                    // Academic
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 7, PrivilegeId = 16 },

                    // Conditional - practice limitations
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 8, PrivilegeId = 16 },

                    // Conditional - practice setting
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 9, PrivilegeId = 19 },

                    // Conditional - disciplined
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 10, PrivilegeId = 16 },

                    // Educational - postgraduate resident
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 12, PrivilegeId = 19 },

                    // Educational - postgraduate resident elective
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 13, PrivilegeId = 19 },

                    // Educational - postgraduate fellow
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 14, PrivilegeId = 19 },

                    // Educational - postgraduate trainee
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 15, PrivilegeId = 16 },

                    // Emergency - family
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 18, PrivilegeId = 19 },

                    // Emergency - specialty
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 19, PrivilegeId = 19 },

                    // Assessment
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 24, PrivilegeId = 16 },

                    // Visitor
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 17, PrivilegeId = 19 },

                    // Surgical assistant
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 22, PrivilegeId = 16 },

                    // Practicing Nurse Practitioner
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 47, PrivilegeId = 19 },

                    // Provisional Nurse Practitioner
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 48, PrivilegeId = 19 },

                    // Temporary Nurse Practitioner (emergency)
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 5 },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 6 },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 7 },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 8 },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 9 },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 10 },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 11 },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 12 },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 13 },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 14 },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 15 },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 16 },
                    new DefaultPrivilege { LicenseCode = 51, PrivilegeId = 19 },
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
