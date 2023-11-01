using AutoMapper;
using FitnessPanelMVC.Application.Mapping;
using FitnessPanelMVC.Application.ViewModels.Meal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.MealProduct
{
    public class MealProductForListVm : IMapFrom<FitnessPanelMVC.Domain.Model.MealProduct>
    {
        public int MealId { get; set; }

        public int ProductId { get; set; }

        public double Weight { get; set; }

        public double Calories { get; set; }

        public double Protein { get; set; }

        public double Fat { get; set; }

        public double Carbs { get; set; }

        public string ProductName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FitnessPanelMVC.Domain.Model.MealProduct, MealProductForListVm>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
