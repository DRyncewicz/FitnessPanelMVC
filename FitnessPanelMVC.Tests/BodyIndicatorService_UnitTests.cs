using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.Mapping;
using FitnessPanelMVC.Application.Services;
using FitnessPanelMVC.Application.ViewModels.BodyIndicator;
using FitnessPanelMVC.Domain.Interfaces;
using FitnessPanelMVC.Domain.Model;
using FluentAssertions;
using Moq;

namespace FitnessPanelMVC.Tests
{
    public class BodyIndicatorService_UnitTests
    {
        private readonly Mock<IBodyIndicatorsRepository> mockRepo;

        private readonly IMapper _mapper;

        private readonly BodyIndicatorService _bodyIndicatorService;

        public BodyIndicatorService_UnitTests()
        {
            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);
            mockRepo = new Mock<IBodyIndicatorsRepository>();
            _bodyIndicatorService = new BodyIndicatorService(mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task AddNewAsync_ShouldAddBodyIndicator()
        {
            // Arrange
            var newBodyIndicatorVm = new NewBodyIndicatorVm
            {
                Mass = 70, 
                Height = 180, 
                HipCircumference = 95, 
                AbdominalCircumference = 80, 
                Age = 30, 
                Sex = "male" 
            };
            var expectedBMI = Math.Round(newBodyIndicatorVm.Mass / Math.Pow(newBodyIndicatorVm.Height / 100.0, 2), 2);
            var expectedBAI = Math.Round(newBodyIndicatorVm.HipCircumference / Math.Pow(newBodyIndicatorVm.Height / 100.0, 1.5), 2);
            var expectedPPM = Math.Round(66.47 + 13.75 * newBodyIndicatorVm.Mass + 5 * newBodyIndicatorVm.Height - 6.75 * newBodyIndicatorVm.Age, 2);
            var expectedBeW = Math.Round(-48.7 + 0.087 * newBodyIndicatorVm.AbdominalCircumference + 1.147 * newBodyIndicatorVm.HipCircumference - 0.003 * Math.Pow(newBodyIndicatorVm.HipCircumference, 2) - 7.195, 2);

            var bodyIndicator = new BodyIndicator();
            mockRepo.Setup(r => r.CreateAsync(It.IsAny<BodyIndicator>()))
                .Callback<BodyIndicator>(bi => bodyIndicator = bi)
                .ReturnsAsync(4);
            // Act
            await _bodyIndicatorService.AddNewAsync(newBodyIndicatorVm, "userId");
            // Assert
            mockRepo.Verify(r => r.CreateAsync(It.IsAny<BodyIndicator>()), Times.Once());
            bodyIndicator.BMI.Should().Be(expectedBMI);
            bodyIndicator.BAI.Should().Be(expectedBAI);
            bodyIndicator.PPM.Should().Be(expectedPPM);
            bodyIndicator.BeW.Should().Be(expectedBeW);
        }


        [Fact]
        public async Task GetByIdAsyncShouldReturnBodyIndicator()
        {
            // Arrange
            var bodyIndicator = new BodyIndicator()
            {
                Id = 1,
                Age = 22,
                Height = 170,
                Date = DateTime.Now
            };
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(bodyIndicator);
            // Act
            var result = await _bodyIndicatorService.GetByIdAsync(1);
            // Assert
            result.Should().BeEquivalentTo(bodyIndicator);
        }

    }
}
