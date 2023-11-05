using FitnessPanelMVC.Application.ViewModels.BodyIndicator;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IBodyIndicatorService
    {
        Task<int> AddNewAsync(NewBodyIndicatorVm bMIFormVm);

        Task<BodyIndicator> GetByIdAsync(int bodyIndicatorId);
    }
}
