using FitnessPanelMVC.Application.ViewModels.Calendar;
using FitnessPanelMVC.Application.ViewModels.HealthStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IHealthStatsService
    {
        Task<HealthStatsVm> GetForListAsync(string userId);
    }
}
