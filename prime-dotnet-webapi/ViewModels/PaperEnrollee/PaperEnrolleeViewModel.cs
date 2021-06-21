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
    public class PaperEnrolleeViewModel : IUserBoundModel
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string GPID { get; set; }

        public string HPDID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GivenNames { get; set; }

        // public string PreferredFirstName { get; set; }

        // public string PreferredMiddleName { get; set; }

        // public string PreferredLastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        // public MailingAddress MailingAddress { get; set; }

        // public VerifiedAddress VerifiedAddress { get; set; }

        public string Email { get; set; }

        public string SmsPhone { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public ICollection<Certification> Certifications { get; set; }

        public ICollection<Job> Jobs { get; set; }

        public ICollection<OboSite> OboSites { get; set; }

        public ICollection<EnrolleeCareSetting> EnrolleeCareSettings { get; set; }

        public ICollection<EnrolleeHealthAuthority> EnrolleeHealthAuthorities { get; set; }

        public string DeviceProviderNumber { get; set; }

        public bool? IsInsulinPumpProvider { get; set; }

        public ICollection<SelfDeclaration> SelfDeclarations { get; set; }

        public ICollection<SelfDeclarationDocument> SelfDeclarationDocuments { get; set; }

        public ICollection<IdentificationDocument> IdentificationDocuments { get; set; }

        public ICollection<EnrolmentStatus> EnrolmentStatuses { get; set; }

        public int? AdjudicatorId { get; set; }

        // TODO currently derived on web client, but currently used on backend
        public string AdjudicatorIdir { get; set; }

        public Admin Adjudicator { get; set; }

        public bool ProfileCompleted { get; set; }

        public ICollection<EnrolleeNote> AdjudicatorNotes { get; set; }

        public AccessAgreementNote AccessAgreementNote { get; set; }

        public bool AlwaysManual { get; set; }

        public ICollection<EnrolleeRemoteUser> EnrolleeRemoteUsers { get; set; }

        public ICollection<RemoteAccessLocation> RemoteAccessLocations { get; set; }

        public ICollection<RemoteAccessSite> RemoteAccessSites { get; set; }

        public int? CredentialId { get; set; }

        public string Base64QRCode { get; set; }

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
