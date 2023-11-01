using AutoMapper;
using FitnessPanelMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.Workout
{
    public class NewWorkoutVm :IMapFrom<FitnessPanelMVC.Domain.Model.Workout>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public String Name { get; set; }

        public string? Description { get; set; }

        public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FitnessPanelMVC.Domain.Model.Workout, NewWorkoutVm>().ReverseMap();
        }
    }
}
