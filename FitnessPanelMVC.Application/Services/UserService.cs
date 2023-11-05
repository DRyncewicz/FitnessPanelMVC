using AutoMapper;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.User;
using FitnessPanelMVC.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Services
{
    public class UserService  : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDetailsVm> GetAsync(ClaimsPrincipal userClaims)
        {
            var user = await _userManager.GetUserAsync(userClaims);

            return _mapper.Map<UserDetailsVm>(user); 
        }

        public async Task<string> GetIdAsync (ClaimsPrincipal userClaims)
        {
            var user = await _userManager.GetUserAsync(userClaims);

            return user.Id;
        }
    }
}
