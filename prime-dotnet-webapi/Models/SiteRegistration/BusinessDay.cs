using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("BusinessDay")]
    public class BusinessDay : BaseAuditable, IValidatableObject
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Does the supplied time fall between the Start Time and End Time?
        /// </summary>
        public bool IsOpen(TimeSpan time)
        {
            return time >= StartTime && time <= EndTime;
        }

        /// <summary>
        /// Does the supplied time fall between the Start Time and End Time?
        /// Only the time portion of the input parameter is considered.
        /// </summary>
        public bool IsOpen(DateTimeOffset time)
        {
            return IsOpen(time.TimeOfDay);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartTime >= EndTime)
            {
                yield return new ValidationResult($"Start Time must be before End Time");
            }
        }
    }
}
