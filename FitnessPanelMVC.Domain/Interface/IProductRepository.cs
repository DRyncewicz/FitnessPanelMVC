using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IProductRepository<T>
    {
        public int CreateProduct(T product);
        public void DeleteProduct(int id);
        public int UpdateProduct(int id);
        public T GetProductById(int id);
        public IQueryable<T> GetAll();
        public IQueryable<T> GetProductsByNameContains(string name);

    }
}
