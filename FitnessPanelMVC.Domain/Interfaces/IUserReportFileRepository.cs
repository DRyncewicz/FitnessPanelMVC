using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interfaces
{
    public interface IUserReportFileRepository
    {
        Task<int> CreateAsync(UserReportFile userReportFile);

        IQueryable<UserReportFile> GetAll();
    }
}
