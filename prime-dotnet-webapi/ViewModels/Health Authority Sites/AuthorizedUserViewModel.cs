using System;
using System.Security.Claims;
using Newtonsoft.Json;
using Prime.Models;

namespace Prime.ViewModels.Parties
{
    public class AuthorizedUserViewModel : IUserBoundModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GivenNames { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PreferredFirstName { get; set; }
        public string PreferredMiddleName { get; set; }
        public string PreferredLastName { get; set; }
        public PhysicalAddress PhysicalAddress { get; set; }
        public VerifiedAddress VerifiedAddress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SmsPhone { get; set; }
        public string JobRoleTitle { get; set; }
        public string EmploymentIdentifier { get; set; }
        public HealthAuthorityCode HealthAuthorityCode { get; set; }
        public AccessStatusType Status { get; set; }
    }
}
