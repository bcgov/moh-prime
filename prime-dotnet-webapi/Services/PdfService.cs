using Wkhtmltopdf.NetCore;

namespace Prime.Services
{
    public class PdfService : IPdfService
    {
        readonly IGeneratePdf _generatePdf;

        public PdfService(IGeneratePdf generatePdf)
        {
            _generatePdf = generatePdf;
        }

        public byte[] Generate(string htmlContent)
        {
            return _generatePdf.GetPDF(htmlContent);
        }
    }
}
