using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Admin")]
    public class Admin : BaseAuditable, IValidatableObject, IUserBoundModel
    {
        [Key]
        public int Id { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string IDIR { get; set; }

        [Required]
        [DefaultValue(AdminStatusType.Enabled)]
        public AdminStatusType Status { get; set; }

        /// <summary>
        /// e.g. "jsmith@idir"
        /// </summary>
        public string Username { get; set; }

        [JsonIgnore]
        public IEnumerable<Enrollee> Enrollees { get; set; }

        [JsonIgnore]
        public IEnumerable<Site> Sites { get; set; }

        [JsonIgnore]
        public IEnumerable<EnrolleeNote> AdjudicatorNotes { get; set; }

        [JsonIgnore]
        public IEnumerable<EnrolmentStatusReference> EnrolmentStatusReference { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Guid.Empty.Equals(UserId))
            {
                yield return new ValidationResult($"UserId cannot be empty");
            }
            if (null == Username)
            {
                yield return new ValidationResult($"Username cannot be empty");
            }
        }
    }
}
