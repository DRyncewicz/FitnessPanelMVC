using AutoMapper;
using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using FitnessPanelMVC.Application.Services;
using Moq;
using FitnessPanelMVC.Application.ViewModels.Product;
using System.Collections.ObjectModel;

namespace FitnessPanelMVC.Tests
{
    public class ProductService_UnitTests
    {
        [Fact]
        public void AddNewProduct_AddsProductAndReturnsProductId()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var mockMapper = new Mock<IMapper>();
            var service = new ProductService(mockRepository.Object, mockMapper.Object);
            var newProductVm = new NewProductVm()
            {
                Id = 1,
                Name = "Test",
                ProteinPer100g = 100,
                CaloriesPer100g = 100,
                CarbsPer100g = 100,
                FatPer100g = 100,
                Barcode = "1231355243",
                Restaurant = "TestDonald"
            }; 
            var expectedProductId = 1;

            mockMapper.Setup(m => m.Map<Product>(It.IsAny<NewProductVm>())).Returns(new Product());
            mockRepository.Setup(r => r.CreateProduct(It.IsAny<Product>())).Returns(expectedProductId);

            // Act
            var result = service.AddNewProduct(newProductVm);

            // Assert
            Assert.Equal(expectedProductId, result);
        }

        [Fact]
        public void UpdateProduct_UpdatesExistingProduct()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var mockMapper = new Mock<IMapper>();
            var service = new ProductService(mockRepository.Object, mockMapper.Object);
            var editedProductVm = new NewProductVm()
            {
                Id = 1,
                Name = "Test2",
                ProteinPer100g = 200,
                CaloriesPer100g = 200,
                CarbsPer100g = 200,
                FatPer100g = 200,
                Barcode = "1651355243",
                Restaurant = "Test"
            };

            mockMapper.Setup(m => m.Map<Product>(It.IsAny<NewProductVm>())).Returns(new Product());
            mockRepository.Setup(r => r.UpdateProduct(It.IsAny<Product>()));

            // Act
            service.UpdateProduct(editedProductVm);

            // Assert
            mockRepository.Verify(r => r.UpdateProduct(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void DeleteProduct_DeletesProductById()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var service = new ProductService(mockRepository.Object, null); 
            var productId = 1;

            mockRepository.Setup(r => r.DeleteProduct(productId));

            // Act
            service.DeleteProduct(productId);

            // Assert
            mockRepository.Verify(r => r.DeleteProduct(productId), Times.Once);
        }

        [Fact]
        public void GetProductForEdit_GetsProductByIdAndReturnsProductVm()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var mockMapper = new Mock<IMapper>();
            var service = new ProductService(mockRepository.Object, mockMapper.Object);
            var productId = 1;
            var product = new Product()
            {
                Id = 1,
                Name = "Test2",
                ProteinPer100g = 200,
                CaloriesPer100g = 200,
                CarbsPer100g = 200,
                FatPer100g = 200,
                Barcode = "1651355243",
                Restaurant = "Test"
            }; 
            var productVm = new NewProductVm()
            {
                Id = 1,
                Name = "Test2",
                ProteinPer100g = 200,
                CaloriesPer100g = 200,
                CarbsPer100g = 200,
                FatPer100g = 200,
                Barcode = "1651355243",
                Restaurant = "Test"
            }; 

            mockRepository.Setup(r => r.GetProductById(productId)).Returns(product);
            mockMapper.Setup(m => m.Map<NewProductVm>(It.IsAny<Product>())).Returns(productVm);

            // Act
            var result = service.GetProductForEdit(productId);

            // Assert
            Assert.Equal(productVm, result);
        }


    }
}