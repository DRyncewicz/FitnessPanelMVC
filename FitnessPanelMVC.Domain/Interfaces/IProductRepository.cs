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
        public int CreateProduct(Product product);
        public void DeleteProduct(int id);
        public int UpdateProduct(Product product);
        public Product GetProductById(int id);
        public IQueryable<Product> GetAllProducts();
        public IQueryable<Product> GetProductsByNameContains(string name);

    }
}
