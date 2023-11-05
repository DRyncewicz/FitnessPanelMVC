using DocumentFormat.OpenXml.InkML;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Calendar;
using FitnessPanelMVC.Application.ViewModels.HealthStats;
using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Services
{
    public class HealthStatsService : IHealthStatsService
    {
        private readonly IMealRepository _mealRepository;

        private readonly IWorkoutRepository _workoutRepository;

        public HealthStatsService(IMealRepository mealRepository, IWorkoutRepository workoutRepository)
        {
            _mealRepository = mealRepository;
            _workoutRepository = workoutRepository;
        }

        public async Task<HealthStatsVm> GetForListAsync(string userId)
        {
            var MealEvents = await _mealRepository.GetAll().Where(x => x.UserId == userId)
                .Select(x => new EventVm
                {
                    Title = x.MealType.ToString(),
                    Start = x.MealDate,
                    End = x.MealDate.AddMinutes(5),
                    Color = "green"
                }).ToListAsync();
            var workoutEvents = await _workoutRepository.GetAll().Where(x => x.UserId == userId)
                .Select(x => new EventVm
                {
                    Title = x.Name,
                    Start = x.Date,
                    End = x.Date.AddSeconds(x.Duration.TotalSeconds),
                    Color = "blue"
                }).ToListAsync();

            CalendarVm calendarVm = new CalendarVm();
            calendarVm.Events.AddRange(MealEvents);
            calendarVm.Events.AddRange(workoutEvents);

            var lastSevenDaysMealsCalories = new List<double>();
            var lastSevenDaysWorkoutCaloriesBurned = new List<double>();

            for (int i = -6; i <= 0; i++)
            {
                var currentDate = DateTime.Now.Date.AddDays(i);
                var mealKcal = _mealRepository.GetAll()
                    .Where(m => m.MealDate.Date == currentDate)
                    .Select(m => m.TotalCalories).Sum();
                var workoutKcal = _workoutRepository.GetAll()
                    .Where(m => m.Date.Date == currentDate)
                    .Select(m => m.TotalCaloriesBurned).Sum();
                lastSevenDaysMealsCalories.Add(mealKcal);
                lastSevenDaysWorkoutCaloriesBurned.Add(workoutKcal);

            }

            ChartVm chartVm = new ChartVm()
            {
                LastWeekCalories = lastSevenDaysMealsCalories,
                LastWeekWorkoutTime = lastSevenDaysWorkoutCaloriesBurned
            };

            HealthStatsVm result = new HealthStatsVm()
            {
                Calendar = calendarVm,
                Charts = chartVm
            };
            return result;
        }

    }
}
