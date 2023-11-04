using AutoMapper;
using FitnessPanelMVC.Application.Mapping;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.UserReportFile
{
    public class NewUserReportFileVm : IMapFrom<Domain.Model.UserReportFile>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string UserId { get; set; }

        public string Path { get; set; }

        public DateTime CreationDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewUserReportFileVm, Domain.Model.UserReportFile>().ReverseMap();
        }
    }
}
