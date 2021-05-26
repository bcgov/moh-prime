using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class ProvinceConfiguration : SeededTable<Province>
    {
        public override IEnumerable<Province> SeedData
        {
            get
            {
                return new[] {
                    new Province { Code = "AB", CountryCode = "CA", Name = "Alberta"                              },
                    new Province { Code = "BC", CountryCode = "CA", Name = "British Columbia"                     },
                    new Province { Code = "MB", CountryCode = "CA", Name = "Manitoba"                             },
                    new Province { Code = "NB", CountryCode = "CA", Name = "New Brunswick"                        },
                    new Province { Code = "NL", CountryCode = "CA", Name = "Newfoundland and Labrador"            },
                    new Province { Code = "NS", CountryCode = "CA", Name = "Nova Scotia"                          },
                    new Province { Code = "ON", CountryCode = "CA", Name = "Ontario"                              },
                    new Province { Code = "PE", CountryCode = "CA", Name = "Prince Edward Island"                 },
                    new Province { Code = "QC", CountryCode = "CA", Name = "Quebec"                               },
                    new Province { Code = "SK", CountryCode = "CA", Name = "Saskatchewan"                         },
                    new Province { Code = "NT", CountryCode = "CA", Name = "Northwest Territories"                },
                    new Province { Code = "NU", CountryCode = "CA", Name = "Nunavut"                              },
                    new Province { Code = "YT", CountryCode = "CA", Name = "Yukon"                                },
                    new Province { Code = "AL", CountryCode = "US", Name = "Alabama"                              },
                    new Province { Code = "AK", CountryCode = "US", Name = "Alaska"                               },
                    new Province { Code = "AS", CountryCode = "US", Name = "American Samoa"                       },
                    new Province { Code = "AZ", CountryCode = "US", Name = "Arizona"                              },
                    new Province { Code = "AR", CountryCode = "US", Name = "Arkansas"                             },
                    new Province { Code = "CA", CountryCode = "US", Name = "California"                           },
                    new Province { Code = "CO", CountryCode = "US", Name = "Colorado"                             },
                    new Province { Code = "CT", CountryCode = "US", Name = "Connecticut"                          },
                    new Province { Code = "DE", CountryCode = "US", Name = "Delaware"                             },
                    new Province { Code = "DC", CountryCode = "US", Name = "District of Columbia"                 },
                    new Province { Code = "FL", CountryCode = "US", Name = "Florida"                              },
                    new Province { Code = "GA", CountryCode = "US", Name = "Georgia"                              },
                    new Province { Code = "GU", CountryCode = "US", Name = "Guam"                                 },
                    new Province { Code = "HI", CountryCode = "US", Name = "Hawaii"                               },
                    new Province { Code = "ID", CountryCode = "US", Name = "Idaho"                                },
                    new Province { Code = "IL", CountryCode = "US", Name = "Illinois"                             },
                    new Province { Code = "IN", CountryCode = "US", Name = "Indiana"                              },
                    new Province { Code = "IA", CountryCode = "US", Name = "Iowa"                                 },
                    new Province { Code = "KS", CountryCode = "US", Name = "Kansas"                               },
                    new Province { Code = "KY", CountryCode = "US", Name = "Kentucky"                             },
                    new Province { Code = "LA", CountryCode = "US", Name = "Louisiana"                            },
                    new Province { Code = "ME", CountryCode = "US", Name = "Maine"                                },
                    new Province { Code = "MD", CountryCode = "US", Name = "Maryland"                             },
                    new Province { Code = "MA", CountryCode = "US", Name = "Massachusetts"                        },
                    new Province { Code = "MI", CountryCode = "US", Name = "Michigan"                             },
                    new Province { Code = "MN", CountryCode = "US", Name = "Minnesota"                            },
                    new Province { Code = "MS", CountryCode = "US", Name = "Mississippi"                          },
                    new Province { Code = "MO", CountryCode = "US", Name = "Missouri"                             },
                    new Province { Code = "MT", CountryCode = "US", Name = "Montana"                              },
                    new Province { Code = "NE", CountryCode = "US", Name = "Nebraska"                             },
                    new Province { Code = "NV", CountryCode = "US", Name = "Nevada"                               },
                    new Province { Code = "NH", CountryCode = "US", Name = "New Hampshire"                        },
                    new Province { Code = "NJ", CountryCode = "US", Name = "New Jersey"                           },
                    new Province { Code = "NM", CountryCode = "US", Name = "New Mexico"                           },
                    new Province { Code = "NY", CountryCode = "US", Name = "New York"                             },
                    new Province { Code = "NC", CountryCode = "US", Name = "North Carolina"                       },
                    new Province { Code = "ND", CountryCode = "US", Name = "North Dakota"                         },
                    new Province { Code = "MP", CountryCode = "US", Name = "Northern Mariana Islands"             },
                    new Province { Code = "OH", CountryCode = "US", Name = "Ohio"                                 },
                    new Province { Code = "OK", CountryCode = "US", Name = "Oklahoma"                             },
                    new Province { Code = "OR", CountryCode = "US", Name = "Oregon"                               },
                    new Province { Code = "PA", CountryCode = "US", Name = "Pennsylvania"                         },
                    new Province { Code = "PR", CountryCode = "US", Name = "Puerto Rico"                          },
                    new Province { Code = "RI", CountryCode = "US", Name = "Rhode Island"                         },
                    new Province { Code = "SC", CountryCode = "US", Name = "South Carolina"                       },
                    new Province { Code = "SD", CountryCode = "US", Name = "South Dakota"                         },
                    new Province { Code = "TN", CountryCode = "US", Name = "Tennessee"                            },
                    new Province { Code = "TX", CountryCode = "US", Name = "Texas"                                },
                    new Province { Code = "UM", CountryCode = "US", Name = "United States Minor Outlying Islands" },
                    new Province { Code = "UT", CountryCode = "US", Name = "Utah"                                 },
                    new Province { Code = "VT", CountryCode = "US", Name = "Vermont"                              },
                    new Province { Code = "VI", CountryCode = "US", Name = "Virgin Islands, U.S."                 },
                    new Province { Code = "VA", CountryCode = "US", Name = "Virginia"                             },
                    new Province { Code = "WA", CountryCode = "US", Name = "Washington"                           },
                    new Province { Code = "WV", CountryCode = "US", Name = "West Virginia"                        },
                    new Province { Code = "WI", CountryCode = "US", Name = "Wisconsin"                            },
                    new Province { Code = "WY", CountryCode = "US", Name = "Wyoming"                              }
                };
            }
        }
    }
}
