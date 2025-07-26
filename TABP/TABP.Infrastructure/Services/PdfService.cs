using TABP.Domain.Interfaces.Services;
namespace TABP.Infrastructure.Services
{
    public class PdfService : IPdfService
    {
        public async Task<byte[]> GenerateBookingInvoicePdf(string Html)
        {
            IronPdf.License.LicenseKey = "IRONSUITE.ALAAHEWWARI.OUTLOOK.COM.3717-A28D764DF3-AV4OSEYZTPMS6GTB-UGW6PZ3AQHQ5-C2DVGSLFRZNT-2BGWNBOVUJE4-DU6FSL22JNFE-UGYFF2MJHBQ5-FV44MK-TYIEFYT6ODSQEA-DEPLOYMENT.TRIAL-DTXVLI.TRIAL.EXPIRES.24.AUG.2025";
            var renderer = new ChromePdfRenderer();
            var pdf = await renderer.RenderHtmlAsPdfAsync(Html);
            pdf.SaveAs("html_saved.pdf");
            var pdfBytes =await File.ReadAllBytesAsync("html_saved.pdf");
            File.Delete("html_saved.pdf");
            return pdfBytes;
        }
    }
}