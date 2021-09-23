using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using DelegateDecompiler;
using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolleeViewModel : IUserBoundModel
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string GPID { get; set; }

        public string HPDID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GivenNames { get; set; }

        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public MailingAddress MailingAddress { get; set; }

        public VerifiedAddress VerifiedAddress { get; set; }

        public string Email { get; set; }

        public string SmsPhone { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public string DeviceProviderNumber { get; set; }

        public bool? IsInsulinPumpProvider { get; set; }


        public int? AdjudicatorId { get; set; }

        // TODO currently derived on web client, but currently used on backend
        public string AdjudicatorIdir { get; set; }

        public Admin Adjudicator { get; set; }

        public bool ProfileCompleted { get; set; }

        public bool AlwaysManual { get; set; }

        public ICollection<EnrolleeRemoteUser> EnrolleeRemoteUsers { get; set; }

        public ICollection<RemoteAccessLocation> RemoteAccessLocations { get; set; }

        public ICollection<RemoteAccessSite> RemoteAccessSites { get; set; }

        public EnrolmentStatus CurrentStatus { get; set; }

        public EnrolmentStatus PreviousStatus { get; set; }

        public DateTimeOffset? AppliedDate { get; set; }

        public DateTimeOffset? ApprovedDate { get; set; }

        public int? CurrentAgreementId { get; set; }

        public DateTimeOffset? ExpiryDate { get; set; }

        public int DisplayId { get; set; }

        // TODO currently derived on web client, but needed on backend for now
        public int CurrentStatusCode { get; set; }

        public bool HasNewestAgreement { get; set; }

        // TODO not currently used in web client, but needed on backend for now
        public bool IsRegulatedUser { get; set; }

        public AgreementType? AssignedTOAType { get; set; }

        public bool RequiresConfirmation { get; set; }

        public bool Confirmed { get; set; }

        public string CurrentTOAStatus
        {
            get
            {
                switch ((StatusType)CurrentStatusCode)
                {
                    case StatusType.UnderReview:
                        return "";
                    case StatusType.Locked:
                    case StatusType.Declined:
                        return "N/A";
                    case StatusType.RequiresToa:
                        return "Pending";
                    case StatusType.Editable:
                        if (ExpiryDate == null || DateTimeOffset.Now >= ExpiryDate)
                        {
                            return "";
                        }
                        else
                        {
                            return HasNewestAgreement ? "Yes" : "No";
                        }
                    default:
                        return null;
                }
            }
        }
    }
}

//  ---- Removed Properties: ----

// public ICollection<Certification> Certifications { get; set; }
// public ICollection<OboSite> OboSites { get; set; }
// public ICollection<EnrolleeCareSetting> EnrolleeCareSettings { get; set; }
// public ICollection<EnrolleeHealthAuthority> EnrolleeHealthAuthorities { get; set; }
// public AccessAgreementNote AccessAgreementNote { get; set; }
// public ICollection<SelfDeclaration> SelfDeclarations { get; set; }
// public ICollection<SelfDeclarationDocument> SelfDeclarationDocuments { get; set; }


//  ---- Removed with notes ----

// TODO: is this being used on the FE? Ideally remove entireley
// public ICollection<Job> Jobs { get; set; }

// TODO: Remove this entireley? BC eID is not used.
// public ICollection<IdentificationDocument> IdentificationDocuments { get; set; }

// TODO: already in extended controller
// public ICollection<EnrolmentStatus> EnrolmentStatuses { get; set; }

// TODO: there is already an endpoint for this
// public ICollection<EnrolleeNote> AdjudicatorNotes { get; set; }

// TODO: dont think we need this at all
// public int? CredentialId { get; set; }

// TODO: already an endpoint
// public string Base64QRCode { get; set; }
