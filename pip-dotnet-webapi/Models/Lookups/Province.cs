using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pidp.Models.Lookups
{
    public enum ProvinceCode
    {
        Alberta = 1,
        BritishColumbia,
        Manitoba,
        NewBrunswick,
        NewfoundlandLabrador,
        NovaScotia,
        Ontario,
        PrinceEdwardIsland,
        Quebec,
        Saskatchewan,
        NorthwestTerritories,
        Nunavut,
        Yukon,
        Alabama,
        Alaska,
        AmericanSamoa,
        Arizona,
        Arkansas,
        California,
        Colorado,
        Connecticut,
        Delaware,
        DistrictOfColumbia,
        Florida,
        Georgia,
        Guam,
        Hawaii,
        Idaho,
        Illinois,
        Indiana,
        Iowa,
        Kansas,
        Kentucky,
        Louisiana,
        Maine,
        Maryland,
        Massachusetts,
        Michigan,
        Minnesota,
        Mississippi,
        Missouri,
        Montana,
        Nebraska,
        Nevada,
        NewHampshire,
        NewJersey,
        NewMexico,
        NewYork,
        NorthCarolina,
        NorthDakota,
        NorthernMarianaIslands,
        Ohio,
        Oklahoma,
        Oregon,
        Pennsylvania,
        PuertoRico,
        RhodeIsland,
        SouthCarolina,
        SouthDakota,
        Tennessee,
        Texas,
        UnitedStatesMinorOutlyingIslands,
        Utah,
        Vermont,
        VirginIslands,
        Virginia,
        Washington,
        WestVirginia,
        Wisconsin,
        Wyoming
    }

    [Table("ProvinceLookup")]
    public class Province
    {
        [Key]
        public ProvinceCode Code { get; set; }

        public CountryCode CountryCode { get; set; }

        public string Name { get; set; } = "";
    }

    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public virtual void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasData(new[]
            {
                new Province { Code = ProvinceCode.Alberta,                          CountryCode = CountryCode.Canada,       Name = "Alberta"                              },
                new Province { Code = ProvinceCode.BritishColumbia,                  CountryCode = CountryCode.Canada,       Name = "British Columbia"                     },
                new Province { Code = ProvinceCode.Manitoba,                         CountryCode = CountryCode.Canada,       Name = "Manitoba"                             },
                new Province { Code = ProvinceCode.NewBrunswick,                     CountryCode = CountryCode.Canada,       Name = "New Brunswick"                        },
                new Province { Code = ProvinceCode.NewfoundlandLabrador,             CountryCode = CountryCode.Canada,       Name = "Newfoundland and Labrador"            },
                new Province { Code = ProvinceCode.NovaScotia,                       CountryCode = CountryCode.Canada,       Name = "Nova Scotia"                          },
                new Province { Code = ProvinceCode.Ontario,                          CountryCode = CountryCode.Canada,       Name = "Ontario"                              },
                new Province { Code = ProvinceCode.PrinceEdwardIsland,               CountryCode = CountryCode.Canada,       Name = "Prince Edward Island"                 },
                new Province { Code = ProvinceCode.Quebec,                           CountryCode = CountryCode.Canada,       Name = "Quebec"                               },
                new Province { Code = ProvinceCode.Saskatchewan,                     CountryCode = CountryCode.Canada,       Name = "Saskatchewan"                         },
                new Province { Code = ProvinceCode.NorthwestTerritories,             CountryCode = CountryCode.Canada,       Name = "Northwest Territories"                },
                new Province { Code = ProvinceCode.Nunavut,                          CountryCode = CountryCode.Canada,       Name = "Nunavut"                              },
                new Province { Code = ProvinceCode.Yukon,                            CountryCode = CountryCode.Canada,       Name = "Yukon"                                },
                new Province { Code = ProvinceCode.Alabama,                          CountryCode = CountryCode.UnitedStates, Name = "Alabama"                              },
                new Province { Code = ProvinceCode.Alaska,                           CountryCode = CountryCode.UnitedStates, Name = "Alaska"                               },
                new Province { Code = ProvinceCode.AmericanSamoa,                    CountryCode = CountryCode.UnitedStates, Name = "American Samoa"                       },
                new Province { Code = ProvinceCode.Arizona,                          CountryCode = CountryCode.UnitedStates, Name = "Arizona"                              },
                new Province { Code = ProvinceCode.Arkansas,                         CountryCode = CountryCode.UnitedStates, Name = "Arkansas"                             },
                new Province { Code = ProvinceCode.California,                       CountryCode = CountryCode.UnitedStates, Name = "California"                           },
                new Province { Code = ProvinceCode.Colorado,                         CountryCode = CountryCode.UnitedStates, Name = "Colorado"                             },
                new Province { Code = ProvinceCode.Connecticut,                      CountryCode = CountryCode.UnitedStates, Name = "Connecticut"                          },
                new Province { Code = ProvinceCode.Delaware,                         CountryCode = CountryCode.UnitedStates, Name = "Delaware"                             },
                new Province { Code = ProvinceCode.DistrictOfColumbia,               CountryCode = CountryCode.UnitedStates, Name = "District of Columbia"                 },
                new Province { Code = ProvinceCode.Florida,                          CountryCode = CountryCode.UnitedStates, Name = "Florida"                              },
                new Province { Code = ProvinceCode.Georgia,                          CountryCode = CountryCode.UnitedStates, Name = "Georgia"                              },
                new Province { Code = ProvinceCode.Guam,                             CountryCode = CountryCode.UnitedStates, Name = "Guam"                                 },
                new Province { Code = ProvinceCode.Hawaii,                           CountryCode = CountryCode.UnitedStates, Name = "Hawaii"                               },
                new Province { Code = ProvinceCode.Idaho,                            CountryCode = CountryCode.UnitedStates, Name = "Idaho"                                },
                new Province { Code = ProvinceCode.Illinois,                         CountryCode = CountryCode.UnitedStates, Name = "Illinois"                             },
                new Province { Code = ProvinceCode.Indiana,                          CountryCode = CountryCode.UnitedStates, Name = "Indiana"                              },
                new Province { Code = ProvinceCode.Iowa,                             CountryCode = CountryCode.UnitedStates, Name = "Iowa"                                 },
                new Province { Code = ProvinceCode.Kansas,                           CountryCode = CountryCode.UnitedStates, Name = "Kansas"                               },
                new Province { Code = ProvinceCode.Kentucky,                         CountryCode = CountryCode.UnitedStates, Name = "Kentucky"                             },
                new Province { Code = ProvinceCode.Louisiana,                        CountryCode = CountryCode.UnitedStates, Name = "Louisiana"                            },
                new Province { Code = ProvinceCode.Maine,                            CountryCode = CountryCode.UnitedStates, Name = "Maine"                                },
                new Province { Code = ProvinceCode.Maryland,                         CountryCode = CountryCode.UnitedStates, Name = "Maryland"                             },
                new Province { Code = ProvinceCode.Massachusetts,                    CountryCode = CountryCode.UnitedStates, Name = "Massachusetts"                        },
                new Province { Code = ProvinceCode.Michigan,                         CountryCode = CountryCode.UnitedStates, Name = "Michigan"                             },
                new Province { Code = ProvinceCode.Minnesota,                        CountryCode = CountryCode.UnitedStates, Name = "Minnesota"                            },
                new Province { Code = ProvinceCode.Mississippi,                      CountryCode = CountryCode.UnitedStates, Name = "Mississippi"                          },
                new Province { Code = ProvinceCode.Missouri,                         CountryCode = CountryCode.UnitedStates, Name = "Missouri"                             },
                new Province { Code = ProvinceCode.Montana,                          CountryCode = CountryCode.UnitedStates, Name = "Montana"                              },
                new Province { Code = ProvinceCode.Nebraska,                         CountryCode = CountryCode.UnitedStates, Name = "Nebraska"                             },
                new Province { Code = ProvinceCode.Nevada,                           CountryCode = CountryCode.UnitedStates, Name = "Nevada"                               },
                new Province { Code = ProvinceCode.NewHampshire,                     CountryCode = CountryCode.UnitedStates, Name = "New Hampshire"                        },
                new Province { Code = ProvinceCode.NewJersey,                        CountryCode = CountryCode.UnitedStates, Name = "New Jersey"                           },
                new Province { Code = ProvinceCode.NewMexico,                        CountryCode = CountryCode.UnitedStates, Name = "New Mexico"                           },
                new Province { Code = ProvinceCode.NewYork,                          CountryCode = CountryCode.UnitedStates, Name = "New York"                             },
                new Province { Code = ProvinceCode.NorthCarolina,                    CountryCode = CountryCode.UnitedStates, Name = "North Carolina"                       },
                new Province { Code = ProvinceCode.NorthDakota,                      CountryCode = CountryCode.UnitedStates, Name = "North Dakota"                         },
                new Province { Code = ProvinceCode.NorthernMarianaIslands,           CountryCode = CountryCode.UnitedStates, Name = "Northern Mariana Islands"             },
                new Province { Code = ProvinceCode.Ohio,                             CountryCode = CountryCode.UnitedStates, Name = "Ohio"                                 },
                new Province { Code = ProvinceCode.Oklahoma,                         CountryCode = CountryCode.UnitedStates, Name = "Oklahoma"                             },
                new Province { Code = ProvinceCode.Oregon,                           CountryCode = CountryCode.UnitedStates, Name = "Oregon"                               },
                new Province { Code = ProvinceCode.Pennsylvania,                     CountryCode = CountryCode.UnitedStates, Name = "Pennsylvania"                         },
                new Province { Code = ProvinceCode.PuertoRico,                       CountryCode = CountryCode.UnitedStates, Name = "Puerto Rico"                          },
                new Province { Code = ProvinceCode.RhodeIsland,                      CountryCode = CountryCode.UnitedStates, Name = "Rhode Island"                         },
                new Province { Code = ProvinceCode.SouthCarolina,                    CountryCode = CountryCode.UnitedStates, Name = "South Carolina"                       },
                new Province { Code = ProvinceCode.SouthDakota,                      CountryCode = CountryCode.UnitedStates, Name = "South Dakota"                         },
                new Province { Code = ProvinceCode.Tennessee,                        CountryCode = CountryCode.UnitedStates, Name = "Tennessee"                            },
                new Province { Code = ProvinceCode.Texas,                            CountryCode = CountryCode.UnitedStates, Name = "Texas"                                },
                new Province { Code = ProvinceCode.UnitedStatesMinorOutlyingIslands, CountryCode = CountryCode.UnitedStates, Name = "United States Minor Outlying Islands" },
                new Province { Code = ProvinceCode.Utah,                             CountryCode = CountryCode.UnitedStates, Name = "Utah"                                 },
                new Province { Code = ProvinceCode.Vermont,                          CountryCode = CountryCode.UnitedStates, Name = "Vermont"                              },
                new Province { Code = ProvinceCode.VirginIslands,                    CountryCode = CountryCode.UnitedStates, Name = "Virgin Islands, U.S."                 },
                new Province { Code = ProvinceCode.Virginia,                         CountryCode = CountryCode.UnitedStates, Name = "Virginia"                             },
                new Province { Code = ProvinceCode.Washington,                       CountryCode = CountryCode.UnitedStates, Name = "Washington"                           },
                new Province { Code = ProvinceCode.WestVirginia,                     CountryCode = CountryCode.UnitedStates, Name = "West Virginia"                        },
                new Province { Code = ProvinceCode.Wisconsin,                        CountryCode = CountryCode.UnitedStates, Name = "Wisconsin"                            },
                new Province { Code = ProvinceCode.Wyoming,                          CountryCode = CountryCode.UnitedStates, Name = "Wyoming"                              }
            });
        }
    }
}