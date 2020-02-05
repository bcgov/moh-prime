using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Prime.Infrastructure;
using Prime.Models;

namespace Prime.ViewModels
{
    public class UpdateEnrolleeProfileViewModel
    {
        public UpdateEnrolleeProfileViewModel(Enrollee model)
        {
            Id = model.Id;
            UserId = model.UserId;
            PreferredFirstName = model.PreferredFirstName;
            PreferredMiddleName = model.PreferredMiddleName;
            PreferredLastName = model.PreferredLastName;
            MailingAddress = model.MailingAddress;
            ContactEmail = model.ContactEmail;
            ContactPhone = model.ContactPhone;
            VoicePhone = model.VoicePhone;
            VoiceExtension = model.VoiceExtension;
            Certifications = model.Certifications;
            Jobs = model.Jobs;
            Organizations = model.Organizations;
            DeviceProviderNumber = model.DeviceProviderNumber;
            IsInsulinPumpProvider = model.IsInsulinPumpProvider;
            HasConviction = model.HasConviction;
            HasConvictionDetails = model.HasConvictionDetails;
            HasRegistrationSuspended = model.HasRegistrationSuspended;
            HasRegistrationSuspendedDetails = model.HasRegistrationSuspendedDetails;
            HasDisciplinaryAction = model.HasDisciplinaryAction;
            HasDisciplinaryActionDetails = model.HasDisciplinaryActionDetails;
            HasPharmaNetSuspended = model.HasPharmaNetSuspended;
            HasPharmaNetSuspendedDetails = model.HasPharmaNetSuspendedDetails;
        }

        [Key]
        public int? Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        public MailingAddress MailingAddress { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public string VoicePhone { get; set; }

        public string VoiceExtension { get; set; }

        public ICollection<Certification> Certifications { get; set; }

        public ICollection<Job> Jobs { get; set; }

        public ICollection<Organization> Organizations { get; set; }

        [RegularExpression(@"([0-9]+)", ErrorMessage = "Device Provider Number should not contain characters")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Device Provider Number must be 5 digits")]
        [JsonConverter(typeof(EmptyStringToNullJsonConverter))]
        public string DeviceProviderNumber { get; set; }

        public bool? IsInsulinPumpProvider { get; set; }

        public bool? HasConviction { get; set; }

        public string HasConvictionDetails { get; set; }

        public bool? HasRegistrationSuspended { get; set; }

        public string HasRegistrationSuspendedDetails { get; set; }

        public bool? HasDisciplinaryAction { get; set; }

        public string HasDisciplinaryActionDetails { get; set; }

        public bool? HasPharmaNetSuspended { get; set; }

        public string HasPharmaNetSuspendedDetails { get; set; }

        public bool ProfileCompleted { get; set; }
    }
}
