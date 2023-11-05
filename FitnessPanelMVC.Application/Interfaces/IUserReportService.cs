using FitnessPanelMVC.Application.ViewModels.User;
using FitnessPanelMVC.Application.ViewModels.UserReportFile;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IUserReportService
    {
        public Task CreateUserBodyReportAsync(BodyIndicator bodyIndicators, string userId);
        Task<IEnumerable<UserReportForListVm>> GetUserBodyReportsAsync(string userId);
    }
}
