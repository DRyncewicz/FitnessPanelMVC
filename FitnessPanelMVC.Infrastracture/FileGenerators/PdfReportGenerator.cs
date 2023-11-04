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
        public byte[] Generate(BodyIndicator data) 
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);
                        document.Add(new Paragraph("Body indicators report")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(14));
                        
                        document.Add(new Paragraph($"BMI: {data.BMI}"));
                        document.Add(new Paragraph($"PPM: {data.PPM}"));
                        document.Add(new Paragraph($"CPM: {data.CPM}"));
                        document.Add(new Paragraph($"WHtR: {data.WHtR}"));
                        document.Add(new Paragraph($"NMC: {data.NMC}"));
                        document.Add(new Paragraph($"BAI: {data.BAI}"));
                        document.Add(new Paragraph($"BeW: {data.BeW}"));
                        document.Close();
                    }
                }

                return stream.ToArray();
            }
        }
    }
}
