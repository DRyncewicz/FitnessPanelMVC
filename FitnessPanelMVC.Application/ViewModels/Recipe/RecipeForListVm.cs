﻿using AutoMapper;
using FitnessPanelMVC.Application.Mapping;
using FitnessPanelMVC.Application.ViewModels.RecipeProduct;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.Recipe
{
    public class RecipeForListVm : IMapFrom<FitnessPanelMVC.Domain.Model.Recipe>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double TotalCalories { get; set; }

        public double TotalCarbs { get; set; }

        public double TotalFat { get; set; }

        public double TotalProtein { get; set; }

        public List<RecipeProductForListVm> RecipeProducts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FitnessPanelMVC.Domain.Model.Recipe, RecipeForListVm>();
        }
    }
}
