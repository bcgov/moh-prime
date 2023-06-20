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
            var options = new ConvertOptions
            {
                PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                {
                    Top = 15,
                    Bottom = 15,
                }
            };

            _generatePdf.SetConvertOptions(options);

            return _generatePdf.GetPDF(htmlContent);
        }
    }
}
