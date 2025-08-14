using TABP.Domain.Interfaces.Services;
namespace Application.IntegrationTests.Common.TestInfrastructure
{
    /// <summary>
    /// Test implementation of PDF service that captures generated content for verification in tests.
    /// </summary>
    public class TestPdfService : IPdfService
    {
        public List<string> GeneratedHtml { get; } = new();
        public List<byte[]> GeneratedPdfs { get; } = new();
        public Task<byte[]> GenerateBookingInvoicePdf(string htmlContent)
        {
            GeneratedHtml.Add(htmlContent);
            var fakePdf = System.Text.Encoding.UTF8
                .GetBytes($"PDF for invoice at {DateTime.UtcNow}");
            GeneratedPdfs.Add(fakePdf);
            return Task.FromResult(fakePdf);
        }
    }
}