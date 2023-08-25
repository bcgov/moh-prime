using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
  [Table("OpcMember")]
  public class OpcMember : BaseAuditable
  {
    [Key]
    public int Id { get; set; }

    public string FullName { get; set; }
    public string Discipline { get; set; }
    public string Status { get; set; }
    public string MemberType { get; set; }
    public string CertificationNumber { get; set; }
    public string Clinic { get; set; }
    public string FullClinicAddress { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string StateProvince { get; set; }
    public string PostalCode { get; set; }
  }
}
