using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class RemoteAccessTypeConfiguration : SeededTable<RemoteAccessType>
    {
        public override IEnumerable<RemoteAccessType> SeedData
        {
            get
            {
                return new[] {
                    new RemoteAccessType { Code = 1, Name = "Private Community Health Practice",   },
                    new RemoteAccessType { Code = 2, Name = "FNHA",                                },
                    new RemoteAccessType { Code = 3, Name = "FNHA Clinic",                                },
                };
            }
        }
    }
}
