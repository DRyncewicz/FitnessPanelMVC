using FitnessPanelMVC.Application.Services;
using FitnessPanelMVC.Domain.Enums;
using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPanelMVC.Tests.Helpers;

namespace FitnessPanelMVC.Tests
{
    public class HealthStatsService_UnitTests
    {
        [Fact]
        public async Task GetForListAsync_ShouldReturnCorrectHealthStats()
        {
            // Arrange
            var mockMealRepo = new Mock<IMealRepository>();
            var mockWorkoutRepo = new Mock<IWorkoutRepository>();
            var userId = "testUser";

            var meals = new List<Meal>
            {
                new Meal
                {
                    Id = 1,
                    UserId = "testUser",
                    MealType = MealType.Breakfast,
                    MealDate = DateTime.Now.AddDays(-1),
                    TotalCalories = 500,
                    TotalCarbs = 50,
                    TotalFat = 20,
                    TotalProtein = 30
                }
            };
            var workouts = new List<Workout>
            {
                new Workout
                {
                    Id = 1,
                    UserId = "testUser",
                    Description = "Morning Run",
                    TotalCaloriesBurned = 300,
                    Duration = TimeSpan.FromMinutes(30),
                    Date = DateTime.Now.AddDays(-1)
                },
            };

            mockMealRepo.Setup(repo => repo.GetAll())
                .Returns(meals.AsAsyncQueryable());
            mockWorkoutRepo.Setup(repo => repo.GetAll())
                .Returns(workouts.AsAsyncQueryable());
            var service = new HealthStatsService(mockMealRepo.Object, mockWorkoutRepo.Object);
            // Act
            var result = await service.GetForListAsync(userId);
            // Assert 
            Assert.NotNull(result);
            Assert.NotNull(result.Calendar);
            Assert.NotNull(result.Charts);

            var mealEvents = result.Calendar.Events.Where(e => e.Color == "green").ToList();
            var workoutEvents = result.Calendar.Events.Where(e => e.Color == "blue").ToList();

            Assert.Equal(meals.Count, mealEvents.Count); 
            Assert.Equal(workouts.Count, workoutEvents.Count); 
            Assert.Equal(7, result.Charts.LastWeekCalories.Count); 
            Assert.Equal(7, result.Charts.LastWeekWorkoutTime.Count);

        }
    }
}

