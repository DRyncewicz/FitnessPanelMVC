using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IFileService
    {
        void AddProductsFromFile(string? filePath);
        byte[] GenerateBodyMetricsReport(BodyIndicator bodyIndicators);
    }
}
