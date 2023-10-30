using AutoMapper;
using FitnessPanelMVC.Application.Mapping;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.Exercise
{
    public class ExerciseForListAndNewVm : IMapFrom<FitnessPanelMVC.Domain.Model.Exercise>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double AverageCaloriesBurnedPerMinute { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<FitnessPanelMVC.Domain.Model.Exercise, ExerciseForListAndNewVm>().ReverseMap();

        }
    }
}
