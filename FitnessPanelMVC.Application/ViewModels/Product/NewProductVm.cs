using AutoMapper;
using FitnessPanelMVC.Application.Mapping;
using FitnessPanelMVC.Application.ViewModels.Meal;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.Product
{
    public class NewProductVm : IMapFrom<FitnessPanelMVC.Domain.Model.Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Restaurant { get; set; }
        public string? Barcode { get; set; }
        public double CaloriesPer100g { get; set; }
        public double CarbsPer100g { get; set; }
        public double FatPer100g { get; set; }
        public double ProteinPer100g { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<FitnessPanelMVC.Domain.Model.Product, NewProductVm>();
        }
    }
}
