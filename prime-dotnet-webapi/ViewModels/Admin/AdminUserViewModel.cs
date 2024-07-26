using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class AdminUserViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public AdminStatusType Status { get; set; }

        public int SitesAssigned { get; set; }

        public int EnrolleesAssigned { get; set; }
    }
}
