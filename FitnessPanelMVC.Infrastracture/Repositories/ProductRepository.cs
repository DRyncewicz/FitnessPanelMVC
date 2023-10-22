using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _dbContext;
        public ProductRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
            return product.Id;
        }

        public void DeleteProduct(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }

        public IQueryable<Product> GetAllProducts()
        {
            var products = _dbContext.Products;
            return products.AsQueryable();
        }

        public Product GetProductById(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                product = new Product();
            }
            return product;
        }

        public IQueryable<Product> GetProductsByNameContains(string name)
        {
            var products = _dbContext.Products.Where(i => i.Name.Contains(name));
            return products.AsQueryable();
        }

        public int UpdateProduct(Product product)
        {
            if (_dbContext.Products.Find(product.Id) != null)
            {
                _dbContext.Products.Update(product);
                _dbContext.SaveChanges();
                return product.Id;
            }
            return 0;
        }
    }
}
