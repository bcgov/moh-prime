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
            /// <summary>
            /// The two-character College Prefix, such as "P1" for the College of Pharmacists of BC
            /// </summary>
            public string CollegeName { get; set; }

            /// <summary>
            /// The Licence Number, typically 5 digits.
            /// </summary>
            public string CollegeId { get; set; }

            public bool Equals(Certification cert)
            {
                if (CollegeName == null || CollegeId == null || cert == null)
                {
                    return false;
                }

                return CollegeName.Equals(cert.College.Prefix, StringComparison.OrdinalIgnoreCase)
                    && CollegeId.Equals(cert.LicenseNumber, StringComparison.OrdinalIgnoreCase);
            }
        }

        public GpidValidationResponse ValidateAgainst(Enrollee enrollee)
        {
            if (enrollee == null
                || enrollee.Certifications == null
                || enrollee.Certifications.Any(cert => cert.College == null))
            {
                throw new ArgumentException("Could not validate against enrollee; enrollee or certs were null");
            }

            return new GpidValidationResponse
            {
                FirstName = MatchAny(FirstName, enrollee.FirstName, enrollee.PreferredFirstName),
                LastName = MatchAny(LastName, enrollee.LastName, enrollee.PreferredLastName),
                DateOfBirth = MatchDate(DateOfBirth, enrollee.DateOfBirth),
                Email = MatchAny(Email, enrollee.ContactEmail),
                Phone = MatchAny(Phone, enrollee.VoicePhone),
                PhoneExtension = MatchAny(PhoneExtension, enrollee.VoiceExtension),
                MobilePhone = MatchAny(MobilePhone, enrollee.ContactPhone),
                CollegeRecords = CollegeRecords?.Select(record => new GpidValidationResponse.CollegeRecordResponse
                {
                    CollegeName = record.CollegeName,
                    CollegeId = record.CollegeId,
                    Match = MatchAny(record, enrollee.Certifications)
                })
            };
        }

        private string MatchAny(string query, params string[] records)
        {
            if (query == null)
            {
                return null;
            }

            if (HasNoData(records))
            {
                return "not-available";
            }

            return records.Any(record => query.Equals(record, StringComparison.OrdinalIgnoreCase))
                .ToString().ToLower();
        }

        private string MatchDate(DateTime? query, DateTime record)
        {
            if (!query.HasValue)
            {
                return null;
            }

            return query.Value.Date.Equals(record.Date)
                .ToString().ToLower();
        }

        private string MatchAny(CollegeRecord query, IEnumerable<Certification> records)
        {
            if (query == null)
            {
                return null;
            }

            if (HasNoData(records))
            {
                return "not-available";
            }

            return records.Any(cert => query.Equals(cert))
                .ToString().ToLower();
        }

        private bool HasNoData<T>(IEnumerable<T> records)
        {
            return records == null || !records.Any() || records.All(r => r == null);
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
