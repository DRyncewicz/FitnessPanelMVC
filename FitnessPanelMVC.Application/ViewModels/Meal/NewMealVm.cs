﻿using AutoMapper;
using FitnessPanelMVC.Application.Mapping;
using FitnessPanelMVC.Application.ViewModels.MealProduct;
using FitnessPanelMVC.Domain.Enums;
using FitnessPanelMVC.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.Meal
{
    public class NewMealVm : IMapFrom<FitnessPanelMVC.Domain.Model.Meal>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public MealType MealType { get; set; }

        public DateTime MealDate { get; set; }

        public double TotalCalories { get; set; }

        public double TotalCarbs { get; set; }

        public double TotalFat { get; set; }

        public double TotalProtein { get; set; }

        public List<MealProductForListVm> MealProducts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FitnessPanelMVC.Domain.Model.Meal, NewMealVm>().ReverseMap();
        }
    }
}
