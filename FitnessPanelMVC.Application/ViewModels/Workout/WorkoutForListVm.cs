﻿using AutoMapper;
using FitnessPanelMVC.Application.Mapping;
using FitnessPanelMVC.Application.ViewModels.WorkoutExercise;
using FitnessPanelMVC.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.Workout
{
    public class WorkoutForListVm : IMapFrom<FitnessPanelMVC.Domain.Model.Workout>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public double TotalCaloriesBurned { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime Date { get; set; }

        public List<WorkoutExerciseForListVm> WorkoutExercises { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FitnessPanelMVC.Domain.Model.Workout, WorkoutForListVm>().ReverseMap();
        }
    }
}
