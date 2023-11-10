using FitnessPanelMVC.Domain.Interfaces;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.FileGenerators
{
    public class BodyIndicatorReportFormatter : IReportFormatter
    {
        public string FormatReport(BodyIndicator data)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Body indicators report");
            stringBuilder.AppendLine($"BMI: {data.BMI}");
            stringBuilder.AppendLine($"PPM: {data.PPM}");
            stringBuilder.AppendLine($"CPM: {data.CPM}");
            stringBuilder.AppendLine($"WHtR: {data.WHtR}");
            stringBuilder.AppendLine($"NMC: {data.NMC}");
            stringBuilder.AppendLine($"BAI: {data.BAI}");
            stringBuilder.AppendLine($"BeW: {data.BeW}");
            return stringBuilder.ToString();
        }
    }
}
