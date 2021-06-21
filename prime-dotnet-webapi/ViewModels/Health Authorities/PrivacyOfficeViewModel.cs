using FluentValidation;

namespace Prime.ViewModels.HealthAuthorities
{
    public class PrivacyOfficeViewModel
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public PrivacyOfficerViewModel PrivacyOfficer { get; set; }
        public AddressViewModel PhysicalAddress { get; set; }
    }

    public class PrivacyOfficeValidator : AbstractValidator<PrivacyOfficeViewModel>
    {
        public PrivacyOfficeValidator()
        {
            RuleFor(x => x.PhysicalAddress).NotNull();
            RuleFor(x => x.PrivacyOfficer).NotNull();
        }
    }
}
