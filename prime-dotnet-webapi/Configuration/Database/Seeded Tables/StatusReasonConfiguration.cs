using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class StatusReasonConfiguration : SeededTable<StatusReason>
    {
        public override IEnumerable<StatusReason> SeedData
        {
            get
            {
                return new[] {
                    new StatusReason { Code = (int)StatusReasonType.Automatic,                    Name = "Automatically Adjudicated"                                    },
                    new StatusReason { Code = (int)StatusReasonType.Manual,                       Name = "Manually Adjudicated"                                         },
                    new StatusReason { Code = (int)StatusReasonType.PharmanetError,               Name = "PharmaNet Error, License could not be validated"              },
                    new StatusReason { Code = (int)StatusReasonType.NotInPharmanet,               Name = "College License or Practitioner ID not in PharmaNet table"    },
                    new StatusReason { Code = (int)StatusReasonType.BirthdateDiscrepancy,         Name = "Birthdate discrepancy in PharmaNet practitioner table"        },
                    new StatusReason { Code = (int)StatusReasonType.NameDiscrepancy,              Name = "Name discrepancy in PharmaNet practitioner table"             },
                    new StatusReason { Code = (int)StatusReasonType.Practicing,                   Name = "Listed as Non-Practicing in PharmaNet practitioner table"     },
                    new StatusReason { Code = (int)StatusReasonType.PumpProvider,                 Name = "Insulin Pump Provider"                                        },
                    new StatusReason { Code = (int)StatusReasonType.LicenceClass,                 Name = "Licence Class requires manual adjudication"                   },
                    new StatusReason { Code = (int)StatusReasonType.SelfDeclaration,              Name = "Answered one or more Self Declaration questions \"Yes\""      },
                    new StatusReason { Code = (int)StatusReasonType.Address,                      Name = "Contact Address or Identity Address not in British Columbia"  },
                    new StatusReason { Code = (int)StatusReasonType.AlwaysManual,                 Name = "Admin has flagged the applicant for manual adjudication"      },
                    new StatusReason { Code = (int)StatusReasonType.AssuranceLevel,               Name = "User does not have high enough identity assurance level"      },
                    new StatusReason { Code = (int)StatusReasonType.IdentityProvider,             Name = "User authenticated with a method other than BC Services Card" },
                    new StatusReason { Code = (int)StatusReasonType.RequestingRemoteAccess,       Name = "User has Requested Remote Access"                             },
                    new StatusReason { Code = (int)StatusReasonType.NoAssignedAgreement,          Name = "Terms of Access to be determined by an Adjudicator"           },
                    new StatusReason { Code = (int)StatusReasonType.NoVerifiedAddress,            Name = "No address from BCSC. Enrollee entered address."              },
                    new StatusReason { Code = (int)StatusReasonType.PaperEnrollee,                Name = "Manually entered paper enrolment"                             },
                    new StatusReason { Code = (int)StatusReasonType.PaperEnrolmentMismatch,       Name = "PRIME enrolment does not match paper enrollee record"         },
                    new StatusReason { Code = (int)StatusReasonType.PossiblePaperEnrolmentMatch,  Name = "Possible match with paper enrolment"                          },
                    new StatusReason { Code = (int)StatusReasonType.UnableToLinkToPaperEnrolment, Name = "Unable to link enrollee to paper enrolment"                   },
                };
            }
        }
    }
}
