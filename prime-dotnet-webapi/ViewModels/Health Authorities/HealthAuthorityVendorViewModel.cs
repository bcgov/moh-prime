using FluentValidation;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthorityVendorViewModel
    {
        public int Id { get; set; }
        public int HealthAuthorityOrganizationId { get; set; }
        public int VendorCode { get; set; }
    }

    public class HealthAuthorityVendorValidator : AbstractValidator<HealthAuthorityVendorViewModel>
    {
        public HealthAuthorityVendorValidator()
        {
            RuleFor(x => x.VendorCode).NotEmpty();
        }
    }
}
