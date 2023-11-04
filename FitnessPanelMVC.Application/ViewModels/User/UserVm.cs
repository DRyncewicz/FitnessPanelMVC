﻿using AutoMapper;
using FitnessPanelMVC.Application.Mapping;
using FitnessPanelMVC.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.User
{
    public class UserVm : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Name { get; set; }

        public string ?Surname { get; set; }

        public string? Sex { get; set; }

        public double? Weight { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, UserVm>()
                .ReverseMap();
        }


    }
}
