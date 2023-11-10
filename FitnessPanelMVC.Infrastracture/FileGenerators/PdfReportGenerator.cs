using FitnessPanelMVC.Domain.Interfaces;
using FitnessPanelMVC.Domain.Model;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.FileGenerators
{
    public class PdfReportGenerator : IPdfReportGenerator
    {
        private readonly IReportFormatter _formatter;

        public PdfReportGenerator(IReportFormatter formatter)
        {
            _formatter = formatter;
        }

        public Task<byte[]> Generate(BodyIndicator data)
        {
            var formattedContent = _formatter.FormatReport(data);

            using (var stream = new MemoryStream())
            {
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);
                        document.Add(new Paragraph(formattedContent)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(14));
                        document.Close();
                    }
                }

                return Task.FromResult(stream.ToArray());
            }
        }
    }

}
