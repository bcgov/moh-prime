using System;
using FluentValidation;

namespace Prime.ViewModels.SpecialAuthorityTransformation
{
    public class SatEnrolleeDemographicViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GivenNames { get; set; }

        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public AddressViewModel PhysicalAddress { get; set; }

        public AddressViewModel PreferredAddress { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }

    public class SatEnrolleeDemographicValidator : AbstractValidator<SatEnrolleeDemographicViewModel>
    {
        public SatEnrolleeDemographicValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.PhysicalAddress).NotNull();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
        }
    }
}
