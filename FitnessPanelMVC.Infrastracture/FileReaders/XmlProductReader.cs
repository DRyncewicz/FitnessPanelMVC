using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using FitnessPanelMVC.Domain.Interfaces;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.FileReaders
{
    public class XmlProductReader : IXmlProductReader
    {
        public async Task<List<Product>> ReadFromFile(string? filePath, string userId)
        {
            var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);
            var range = worksheet.RangeUsed();
            List<Product> products = new List<Product>();

            foreach (var row in range.Rows().Skip(1))
            {
                var productName = row.Cell(3).Value.ToString();
                var productCalories = Math.Round(((double)row.Cell(4).Value) / 4.18, 2);
                var productProtein = ((double)row.Cell(7).Value);
                var productFat = ((double)row.Cell(9).Value);
                var productCarbs = ((double)row.Cell(39).Value);

                products.Add(new Product
                {
                    Name = productName,
                    CaloriesPer100g = productCalories,
                    CarbsPer100g = productCarbs,
                    ProteinPer100g = productProtein,
                    FatPer100g = productFat,
                    UserId = userId,
                    IsConfirmed = true,
                    IsUserAdded = false
                });

            }

            return products;
        }
    }
}
