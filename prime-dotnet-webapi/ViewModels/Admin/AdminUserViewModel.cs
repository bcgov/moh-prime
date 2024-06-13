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

        public int Status { get; set; }

        public int SiteAssigned { get; set; }

        public int EnrolleeAssigned { get; set; }
    }
}
