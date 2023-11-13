using AutoMapper;
using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using FitnessPanelMVC.Application.Services;
using Moq;
using FitnessPanelMVC.Application.ViewModels.Product;
using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using FitnessPanelMVC.Domain.Interfaces;
using FitnessPanelMVC.Application.Mapping;
using FitnessPanelMVC.Infrastructure.Repositories;
using FitnessPanelMVC.Tests.Helpers;
using FluentAssertions;
using Xunit.Extensions.AssertExtensions;

namespace FitnessPanelMVC.Tests
{
    public class ProductService_UnitTests
    {
        private readonly Mock<IProductRepository> mockProductRepo;

        private readonly Mock<IXmlProductReader> mockXmlProductReader;

        private readonly ProductService _productService;

        private readonly IMapper _mapper;

        private readonly List<Product> _products;

        public ProductService_UnitTests()
        {
            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);
            mockProductRepo = new Mock<IProductRepository>();
            mockXmlProductReader = new Mock<IXmlProductReader>();
            _productService = new ProductService(mockProductRepo.Object, _mapper, mockXmlProductReader.Object);
            _products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    CaloriesPer100g = 50,
                    CarbsPer100g = 5,
                    FatPer100g = 5,
                    ProteinPer100g = 5,
                    Name = "Test1",
                    IsConfirmed = true,
                    IsUserAdded = false,
                    UserId = "testUser"
                },
                new Product
                {
                    Id = 2,
                    CaloriesPer100g = 100,
                    CarbsPer100g = 10,
                    FatPer100g = 10,
                    ProteinPer100g = 10,
                    Name = "Test2",
                    IsConfirmed = true,
                    IsUserAdded = false,
                    UserId = "testUser1"
                },
                new Product
                {
                    Id = 3,
                    CaloriesPer100g = 100,
                    CarbsPer100g = 10,
                    FatPer100g = 10,
                    ProteinPer100g = 10,
                    Name = "Test3",
                    IsConfirmed = false,
                    IsUserAdded = true,
                    UserId = "tester"
                }
            };
        }

        [Fact]
        public async Task GetAllForListAsyncShouldReturnListOfProductVm()
        {
            //Arrange
            mockProductRepo.Setup(x => x.GetAll())
                .Returns(_products.AsAsyncQueryable());
            //Act
            var resultForTestUser = await _productService.GetAllForListAsync(20, 1, "", "testUser");
            var resultForTester = await _productService.GetAllForListAsync(20, 1, "", "tester");
            var ResultForSearchString = await _productService.GetAllForListAsync(20, 1, "Test1", "testUser");
            // Assert
            var expectedResultCountForTestUser = 2;
            var expectedResultCountForTester = 3;
            Assert.IsType<ListProductForListVm>(resultForTestUser);
            Assert.Equal(expectedResultCountForTestUser, resultForTestUser.Products.Count());
            Assert.Equal(expectedResultCountForTester, resultForTester.Products.Count());
            var expectedProduct = _products.Find(x => x.Name.Contains("Test1"));
            var actualProduct = ResultForSearchString.Products.FirstOrDefault();
            Assert.NotNull(actualProduct);
            Assert.Equal(_mapper.Map<ProductForListVm>(expectedProduct).Name, actualProduct.Name);
        }

        [Fact]
        public async Task AddNewAsyncShouldAddNewProduct()
        {
            //Arrange
            var fakeProduct = new NewProductVm()
            {
                Name = "Test4",
                ProteinPer100g = 15,
                FatPer100g = 15,
                CarbsPer100g = 15,

            };
            mockProductRepo.Setup(x => x.CreateAsync(It.IsAny<Product>())).ReturnsAsync(4);
            //Act
            var result = await _productService.AddNewAsync(fakeProduct, "tester");
            //Assert
            Assert.NotNull(result);
            result.Should().Be(4);
            mockProductRepo.Verify(repo => repo.CreateAsync(It.IsAny<Product>()), Times.Once());
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateExistingProduct()
        {
            //Arrange
            var updatedProductVm = new NewProductVm()
            {
                Name = "Test111",
                CarbsPer100g = 150,
                ProteinPer100g = 150,
                FatPer100g = 150,
                Barcode = "12301233",
                Restaurant = "McTest",
                Id = 1
            };
            mockProductRepo.Setup(x => x.UpdateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            //Act
            _productService.UpdateAsync(updatedProductVm);
            //Assert
            mockProductRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once());
        }
    }
}
