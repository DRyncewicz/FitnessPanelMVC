using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ListProductForListVm GetAllProductsForList(int pageSize, int pageNo, string searchString)
        {
            var products = _productRepository.GetAllProducts().Where(p => p.Name.StartsWith(searchString))
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

        public int AddNewProduct(NewProductVm newProductVm)
        {
            var product = _mapper.Map<Product>(newProductVm);
            int productId = _productRepository.CreateProduct(product);
            return productId;
        }
    }
}