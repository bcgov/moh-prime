using System;
using FluentValidation;

namespace Prime.ViewModels.PaperEnrollees
{
    public class PaperEnrolleeDemographicViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public AddressViewModel PhysicalAddress { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public string SmsPhone { get; set; }
    }

    public class PaperEnrolleeDemographicValidator : AbstractValidator<PaperEnrolleeDemographicViewModel>
    {
        public PaperEnrolleeDemographicValidator()
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
