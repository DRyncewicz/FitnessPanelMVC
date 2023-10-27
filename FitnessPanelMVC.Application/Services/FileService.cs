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

namespace FitnessPanelMVC.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IProductService _productService;

        public FileService(IProductService productService)
        {
            _productService = productService;
        }



        public void AddProductsFromFile(string? filePath)
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
                _productService.AddNewProduct(newProductVm);

            }
        }
    }
}