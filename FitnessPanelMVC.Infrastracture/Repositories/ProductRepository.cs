using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContext _dbContext;
        public ProductRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
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


        public void UpdateProduct(Product product)
        {
            var existingProduct = _dbContext.Products.Find(product.Id);
            if (existingProduct != null)
            {
                _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);
                _dbContext.SaveChanges();
            }
        }
    }
}
