using System;
using FluentValidation;

namespace Prime.ViewModels
{
    public class EnrolleeAbsenceViewModel
    {
        public int Id { get; set; }

        public DateTime StartTimestamp { get; set; }

        public DateTime EndTimestamp { get; set; }
    }

    public class EnrolleeAbsenceValidator : AbstractValidator<EnrolleeAbsenceViewModel>
    {
        public EnrolleeAbsenceValidator()
        {
            RuleFor(x => x.EndTimestamp).GreaterThanOrEqualTo(x => x.StartTimestamp);
        }
    }
}
