using FitnessPanelMVC.Domain.Interfaces;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    public class BodyIndicatorRepository : IBodyIndicatorsRepository
    {
        private readonly DbContext _dbContext;
            public BodyIndicatorRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int CreateBodyIndicator(BodyIndicator bodyIndicator)
        {
            _dbContext.BodyIndicators.Add(bodyIndicator);
            _dbContext.SaveChanges();
            return bodyIndicator.Id;
        }

        public IQueryable<BodyIndicator> GetAllBodyIndicators()
        {
            var bodyIndicators = _dbContext.BodyIndicators;
            return bodyIndicators;
        }
    }
}
