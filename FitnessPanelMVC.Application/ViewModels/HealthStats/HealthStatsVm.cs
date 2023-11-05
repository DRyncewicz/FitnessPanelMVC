using FitnessPanelMVC.Application.ViewModels.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.HealthStats
{
    public class HealthStatsVm
    {
        public CalendarVm Calendar { get; set; }
        public ChartVm Charts { get; set; }
    }
}
