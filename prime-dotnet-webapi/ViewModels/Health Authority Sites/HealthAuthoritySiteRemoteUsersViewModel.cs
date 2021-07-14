using System;
using System.Collections.Generic;
using FluentValidation;
using Prime.Models;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteRemoteUsersViewModel
    {
        public ICollection<RemoteUser> RemoteUsers { get; set; }
    }
}
