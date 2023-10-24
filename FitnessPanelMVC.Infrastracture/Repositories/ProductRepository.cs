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
            _dbContext.Attach(product);
            _dbContext.Entry(product).Property("Name").IsModified = true;
            _dbContext.Entry(product).Property("Restaurant").IsModified = true;
            _dbContext.Entry(product).Property("Barcode").IsModified = true;
            _dbContext.Entry(product).Property("CaloriesPer100g").IsModified = true;
            _dbContext.Entry(product).Property("CarbsPer100g").IsModified = true;
            _dbContext.Entry(product).Property("FatPer100g").IsModified = true;
            _dbContext.Entry(product).Property("ProteinPer100g").IsModified = true;
            _dbContext.SaveChanges();
        }
    }
}
