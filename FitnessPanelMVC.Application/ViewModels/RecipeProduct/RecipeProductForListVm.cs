using AutoMapper;
using FitnessPanelMVC.Application.Mapping;
using FitnessPanelMVC.Application.ViewModels.MealProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.RecipeProduct
{
    public class RecipeProductForListVm : IMapFrom<FitnessPanelMVC.Domain.Model.RecipeProduct>
    {
        public int RecipeId { get; set; }

        public int ProductId { get; set; }

        public double Weight { get; set; }

        public double Calories { get; set; }

        public double Protein { get; set; }

        public double Fat { get; set; }

        public double Carbs { get; set; }

        public string ProductName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FitnessPanelMVC.Domain.Model.RecipeProduct, RecipeProductForListVm>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
