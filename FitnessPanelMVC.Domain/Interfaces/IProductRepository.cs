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
        public int Create(Product product);

        public void Delete(int id);

        public void Update(Product product);

        public Product GetById(int id);

        public IQueryable<Product> GetAll();

    }
}
