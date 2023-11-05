using FitnessPanelMVC.Application.ViewModels.User;
using FitnessPanelMVC.Application.ViewModels.UserReportFile;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.UserHealthDashboard
{
    public class ReportDashboardVm
    {
        public IEnumerable<UserReportForListVm> Reports { get; set; }
        public UserDetailsVm User { get; set; }
    }
}
