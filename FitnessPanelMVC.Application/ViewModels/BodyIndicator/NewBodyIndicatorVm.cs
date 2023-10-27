using AutoMapper;
using FitnessPanelMVC.Application.Mapping;
using FitnessPanelMVC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.BodyIndicator
{
    public class NewBodyIndicatorVm : IMapFrom<FitnessPanelMVC.Domain.Model.BodyIndicator>
    {
        public int Id { get; set; }
        public string Sex { get; set; }
        public double Mass { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public PAL PAL { get; set; }
        public int HipCircumference { get; set; }
        public int AbdominalCircumference { get; set; }



        public void Mapping(Profile profile)
        {
            profile.CreateMap<FitnessPanelMVC.Domain.Model.BodyIndicator, NewBodyIndicatorVm>().ReverseMap();
        }
    }
}