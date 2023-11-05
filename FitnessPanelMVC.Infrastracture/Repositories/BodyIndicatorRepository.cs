using FitnessPanelMVC.Domain.Interfaces;
using FitnessPanelMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> CreateAsync(BodyIndicator bodyIndicator)
        {
            await _dbContext.BodyIndicators.AddAsync(bodyIndicator);
            await _dbContext.SaveChangesAsync();
            return bodyIndicator.Id;
        }

        public async Task<BodyIndicator> GetByIdAsync(int id)
        {
            var bodyIndicator = await _dbContext.BodyIndicators.FirstOrDefaultAsync(bi => bi.Id == id);
            if (bodyIndicator != null)
            {
                return bodyIndicator;
            }

            return new BodyIndicator();
        }

        public IQueryable<BodyIndicator> GetAll()
        {
            var bodyIndicators = _dbContext.BodyIndicators;
            return bodyIndicators;
        }
    }
}
