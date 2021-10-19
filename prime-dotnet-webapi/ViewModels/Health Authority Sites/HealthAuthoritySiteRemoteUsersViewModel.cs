using System;
using System.Collections.Generic;
using FluentValidation;
using Prime.Models;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteRemoteUsersViewModel
    {
        // TODO generic remote user view model
        public ICollection<RemoteUserViewModel> RemoteUsers { get; set; }
    }
}
