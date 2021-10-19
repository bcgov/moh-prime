using System;
using System.Collections.Generic;
using FluentValidation;
using Prime.Models;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteHoursOperationViewModel
    {
        // TODO generic view model for sites
        public ICollection<BusinessHourViewModel> BusinessHours { get; set; }
    }

    public class HealthAuthoritySiteHoursOperationValidator : AbstractValidator<HealthAuthoritySiteHoursOperationViewModel>
    {
        public HealthAuthoritySiteHoursOperationValidator()
        {
            RuleFor(x => x.BusinessHours).NotNull();
        }
    }
}
