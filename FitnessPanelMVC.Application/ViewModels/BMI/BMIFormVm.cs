using AutoMapper;
using FitnessPanelMVC.Application.Mapping;
using FitnessPanelMVC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.BMI
{
    public class BMIFormVm : IMapFrom<FitnessPanelMVC.Domain.Model.BMI>
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
            profile.CreateMap<FitnessPanelMVC.Domain.Model.BMI, BMIFormVm>();
        }
    }
}