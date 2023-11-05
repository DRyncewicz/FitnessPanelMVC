using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Interfaces;
using FitnessPanelMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        private readonly IXmlProductReader _xmlProductReader;

        public ProductService(IProductRepository productRepository, IMapper mapper, IXmlProductReader xmlProductReader)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _xmlProductReader = xmlProductReader;
        }

        public async Task<ListProductForListVm> GetAllForListAsync(int pageSize, int pageNo, string searchString, string userId)
        {
            var products = await _productRepository.GetAll().
                Where(p => p.Name.StartsWith(searchString) && p.IsConfirmed == true ||
                p.Name.StartsWith(searchString) && p.UserId == userId)
                .ProjectTo<ProductForListVm>(_mapper.ConfigurationProvider).ToListAsync();
            var productsToShow = products.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
            var productList = new ListProductForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                SearchString = searchString,
                Products = productsToShow,
                Count = products.Count
            };

            return productList;
        }

        public async Task<int> AddNewAsync(NewProductVm newProductVm, string userId)
        {
            var product = _mapper.Map<Product>(newProductVm);
            product.UserId = userId;
            int productId = await _productRepository.CreateAsync(product);
            return productId;
        }

        public async Task<NewProductVm> GetForEditAsync(int productId)
        {
            var product = _productRepository.GetByIdAsync(productId);
            var productVm = _mapper.Map<NewProductVm>(product);
            return productVm;
        }

        public async Task UpdateAsync(NewProductVm editedProduct)
        {
            var product = _mapper.Map<Product>(editedProduct);
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteByIdAsync(int productId)
        {
            await _productRepository.DeleteAsync(productId);
        }

        public async Task<ProductDetailsVm> GetDetailsByIdAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            var productDetailsVm = _mapper.Map<ProductDetailsVm>(product);
            productDetailsVm = ProductDetailsReflection(productDetailsVm);

            return productDetailsVm;
        }

        public async Task AddFromXmlFileAsync(string filePath, string userId)
        {
            List<Product> products = await _xmlProductReader.ReadFromFile(filePath, userId);
            await _productRepository.CreateRangeAsync(products);
        }


        private ProductDetailsVm ProductDetailsReflection(ProductDetailsVm productDetailsVm)
        {
            Type type = productDetailsVm.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(productDetailsVm) == null && property.PropertyType == typeof(string))
                {
                    property.SetValue(productDetailsVm, "Not given");
                }
            }

            return productDetailsVm;
        }


    }
}