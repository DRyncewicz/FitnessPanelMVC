using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IProductRepository
    {

        public IQueryable<Product> GetAll();
        Task CreateRangeAsync(List<Product> products);
        Task<int> CreateAsync(Product product);
        Task DeleteAsync(int id);
        Task UpdateAsync(Product product);
        Task<Product> GetByIdAsync(int id);
    }
}
