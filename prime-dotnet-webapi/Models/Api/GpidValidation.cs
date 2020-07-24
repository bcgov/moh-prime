using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models.Api
{
    [NotMapped]
    public class GpidValidationParameters
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public string MobilePhone { get; set; }

        public IEnumerable<CollegeRecord> CollegeRecords { get; set; }

        public class CollegeRecord
        {
            public string CollegeName { get; set; }
            public string CollegeId { get; set; }

            public bool Equals(CollegeRecord other)
            {
                if (other == null)
                {
                    return false;
                }

                return CollegeName == other.CollegeName
                    && CollegeId == other.CollegeId;
            }
        }

        public GpidValidationResponse ValidateAgainst(Enrollee enrollee)
        {
            var enrolleeCerts = enrollee.Certifications.Select(c => new CollegeRecord
            {
                CollegeName = c.College.Prefix,
                CollegeId = c.LicenseNumber
            });

            return new GpidValidationResponse
            {
                FirstName = Match(FirstName, enrollee.FirstName),
                LastName = Match(LastName, enrollee.LastName),
                DateOfBirth = Match(DateOfBirth?.Date, enrollee.DateOfBirth.Date),
                Email = Match(Email, enrollee.ContactEmail),
                Phone = Match(Phone, enrollee.VoicePhone),
                PhoneExtension = Match(PhoneExtension, enrollee.VoiceExtension),
                MobilePhone = Match(MobilePhone, enrollee.ContactPhone),
                CollegeRecords = CollegeRecords.Select(record => new GpidValidationResponse.CollegeRecordResponse
                {
                    CollegeName = record.CollegeName,
                    CollegeId = record.CollegeId,
                    Match = enrolleeCerts.Any(cer => cer.Equals(record)) ? "yes" : "no"
                })
            };
        }

        private string Match<T>(T query, T record)
        {
            if (query == null)
            {
                return null;
            }

            if (record == null)
            {
                return "not-available";
            }

            return query.Equals(record) ? "yes" : "no";
        }
    }

    [NotMapped]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class GpidValidationResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public string MobilePhone { get; set; }

        public IEnumerable<CollegeRecordResponse> CollegeRecords { get; set; }

        public class CollegeRecordResponse
        {
            public string CollegeName { get; set; }
            public string CollegeId { get; set; }
            public string Match { get; set; }
        }
    }
}
