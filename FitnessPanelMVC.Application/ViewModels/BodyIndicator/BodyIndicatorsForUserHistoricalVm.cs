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
    public class BodyIndicatorsForUserHistoricalVm : IMapFrom<FitnessPanelMVC.Domain.Model.BodyIndicator>
    {
        public int Id { get; set; }

        public double Mass { get; set; }

        public int Age { get; set; }

        public PAL PAL { get; set; }

        public double BMI { get; set; }

        public double BeW { get; set; }

        public double BAI { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FitnessPanelMVC.Domain.Model.BodyIndicator, BodyIndicatorsForUserHistoricalVm>().ReverseMap();
        }
    }
}
