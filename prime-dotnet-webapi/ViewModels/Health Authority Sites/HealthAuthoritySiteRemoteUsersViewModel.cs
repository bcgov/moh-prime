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

    public class HealthAuthoritySiteRemoteUsersValidator : AbstractValidator<HealthAuthoritySiteRemoteUsersViewModel>
    {
        public HealthAuthoritySiteRemoteUsersValidator()
        {
            RuleFor(x => x.RemoteUsers).NotNull();
        }
    }
}
