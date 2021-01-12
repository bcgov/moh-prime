namespace Prime.Services.Razor
{
    public class EnrolleeAgreementRazorPackage : RazorRenderingPackage<EnrolleeAgreementRazorViewModel>
    {
        public EnrolleeAgreementRazorPackage(EnrolleeAgreementRazorViewModel viewModel)
            : base("/NewViews/Agreements/TermsOfAccessPdf.cshtml", viewModel)
        { }
    }
}
