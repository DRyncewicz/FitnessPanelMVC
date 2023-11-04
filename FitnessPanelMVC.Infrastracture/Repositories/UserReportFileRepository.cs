using FitnessPanelMVC.Domain.Interfaces;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    public class UserReportFileRepository : IUserReportFileRepository
    {
        private readonly DbContext _dbContext;

        public UserReportFileRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(UserReportFile userReportFile)
        {
            _dbContext.Add(userReportFile);
            _dbContext.SaveChanges();
            return userReportFile.Id;
        }

        public IQueryable<UserReportFile> GetAll()
        {
            return _dbContext.UserReportFiles;
        }
    }
}
