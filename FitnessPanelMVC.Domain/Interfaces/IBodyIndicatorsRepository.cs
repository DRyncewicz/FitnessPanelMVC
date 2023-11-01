using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interfaces
{
    public interface IBodyIndicatorsRepository
    {
        public int Create(BodyIndicator bMI);

        IQueryable<BodyIndicator> GetAll();
    }
}
