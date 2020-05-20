using System;
using System.Collections.Generic;
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

        [JsonIgnore]
        public IEnumerable<Enrollee> Enrollees { get; set; }

        [JsonIgnore]
        public IEnumerable<AdjudicatorNote> AdjudicatorNotes { get; set; }

        [JsonIgnore]
        public IEnumerable<EnrolmentStatusReference> EnrolmentStatusReference { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Guid.Empty.Equals(this.UserId))
            {
                yield return new ValidationResult($"UserId cannot be the empty value: {this.UserId.ToString()}");
            }
        }
    }
}
