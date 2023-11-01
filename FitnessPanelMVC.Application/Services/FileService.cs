using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using FitnessPanelMVC.Domain.Model;
using FitnessPanelMVC.Application.ViewModels.Product;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace FitnessPanelMVC.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IProductService _productService;

        public FileService(IProductService productService)
        {
            _productService = productService;
        }

        public void AddProductsFromFile(string? filePath, string userId)
        {
            var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);
            var range = worksheet.RangeUsed();

            foreach (var row in range.Rows().Skip(1))
            {
                var productName = row.Cell(3).Value.ToString();
                var productCalories = Math.Round(((double)row.Cell(4).Value) / 4.18, 2);
                var productProtein = ((double)row.Cell(7).Value);
                var productFat = ((double)row.Cell(9).Value);
                var productCarbs = ((double)row.Cell(39).Value);

                NewProductVm newProductVm = new NewProductVm()
                {
                    Name = productName,
                    CaloriesPer100g = productCalories,
                    CarbsPer100g = productCarbs,
                    ProteinPer100g = productProtein,
                    FatPer100g = productFat
                };

                _productService.AddNew(newProductVm, userId);
            }
        }

        public byte[] GenerateBodyMetricsReport(BodyIndicator bodyIndicators)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);
                        document.Add(new Paragraph("Body indicator report")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(14));

                        document.Add(new Paragraph($"BMI: {bodyIndicators.BMI}"));
                        document.Add(new Paragraph($"PPM: {bodyIndicators.PPM}"));
                        document.Add(new Paragraph($"CPM: {bodyIndicators.CPM}"));
                        document.Add(new Paragraph($"WHtR: {bodyIndicators.WHtR}"));
                        document.Add(new Paragraph($"NMC: {bodyIndicators.NMC}"));
                        document.Add(new Paragraph($"BAI: {bodyIndicators.BAI}"));
                        document.Add(new Paragraph($"BeW: {bodyIndicators.BeW}"));
                        document.Close();
                    }
                }

                return stream.ToArray();
            }
        }
    }
}