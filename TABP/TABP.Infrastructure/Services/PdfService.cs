using Microsoft.Extensions.Options;
using TABP.Domain.Interfaces.Services;
using TABP.Infrastructure.Configurations;
namespace TABP.Infrastructure.Services
{
    public class PdfService : IPdfService
    {
        private readonly IOptionsMonitor<IronPdfConfiguration> _configs;
        public PdfService(IOptionsMonitor<IronPdfConfiguration> optionsMonitor)
        {
            _configs = optionsMonitor;
        }
        public async Task<byte[]> GenerateBookingInvoicePdf(string Html)
        {
            var licenseKey = _configs.CurrentValue.LicenseKey;
            IronPdf.License.LicenseKey = licenseKey;
            var renderer = new ChromePdfRenderer();
            var pdf = await renderer.RenderHtmlAsPdfAsync(Html);
            pdf.SaveAs("html_saved.pdf");
            var pdfBytes =await File.ReadAllBytesAsync("html_saved.pdf");
            File.Delete("html_saved.pdf");
            return pdfBytes;
        }
    }
}