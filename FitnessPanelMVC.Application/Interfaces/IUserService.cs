using FitnessPanelMVC.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserVm> GetAsync(ClaimsPrincipal userClaims);
    }
}
