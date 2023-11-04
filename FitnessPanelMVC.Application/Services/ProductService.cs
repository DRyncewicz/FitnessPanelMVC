using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Interfaces;
using FitnessPanelMVC.Domain.Model;
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

        public ListProductForListVm GetAllForList(int pageSize, int pageNo, string searchString, string userId)
        {
            var products = _productRepository.GetAll().
                Where(p => p.Name.StartsWith(searchString) && p.IsConfirmed == true ||
                p.Name.StartsWith(searchString) && p.UserId == userId)
                .ProjectTo<ProductForListVm>(_mapper.ConfigurationProvider).ToList();
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

        public int AddNew(NewProductVm newProductVm, string userId)
        {
            var product = _mapper.Map<Product>(newProductVm);
            product.UserId = userId;
            int productId = _productRepository.Create(product);
            return productId;
        }

        public NewProductVm GetForEdit(int productId)
        {
            var product = _productRepository.GetById(productId);
            var productVm = _mapper.Map<NewProductVm>(product);
            return productVm;
        }

        public void Update(NewProductVm editedProduct)
        {
            var product = _mapper.Map<Product>(editedProduct);
            _productRepository.Update(product);
        }

        public void DeleteById(int productId)
        {
            _productRepository.Delete(productId);
        }

        public ProductDetailsVm GetDetailsById(int productId)
        {
            var product = _productRepository.GetById(productId);
            var productDetailsVm = _mapper.Map<ProductDetailsVm>(product);
            productDetailsVm = ProductDetailsReflection(productDetailsVm);

            return productDetailsVm;
        }

        public async Task AddFromXmlFile(string filePath, string userId)
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