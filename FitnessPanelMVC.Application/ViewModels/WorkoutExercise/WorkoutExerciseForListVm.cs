using AutoMapper;
using FitnessPanelMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.WorkoutExercise
{
    public class WorkoutExerciseForListVm : IMapFrom<FitnessPanelMVC.Domain.Model.WorkoutExercise>
    {
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
        public DateTime Duration { get; set; }
        public double CaloriesBurned { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FitnessPanelMVC.Domain.Model.WorkoutExercise, WorkoutExerciseForListVm>().ReverseMap();
        }

    }
}
