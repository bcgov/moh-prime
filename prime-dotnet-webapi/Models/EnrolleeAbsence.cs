using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
  [Table("EnrolleeAbsence")]
  public class EnrolleeAbsence : BaseAuditable, IEnrolleeNavigationProperty
  {
    [Key]
    public int Id { get; set; }

    [JsonIgnore]
    public int EnrolleeId { get; set; }

    [JsonIgnore]
    public Enrollee Enrollee { get; set; }
    public DateTime StartTimestamp { get; set; }

    public DateTime EndTimestamp { get; set; }
  }
}
