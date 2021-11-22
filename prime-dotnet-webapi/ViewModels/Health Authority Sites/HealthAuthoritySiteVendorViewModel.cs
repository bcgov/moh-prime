using FluentValidation;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteVendorViewModel
    {
        public int Id { get; set; }
        public int VendorCode { get; set; }
    }

    public class HealthAuthoritySiteVendorValidator : AbstractValidator<HealthAuthoritySiteVendorViewModel>
    {
        public HealthAuthoritySiteVendorValidator()
        {
            RuleFor(x => x.VendorCode).NotNull();
        }
    }
}

