using AutoMapper;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.BodyIndicator;
using FitnessPanelMVC.Domain.Interfaces;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Services
{
    public class BodyIndicatorService : IBodyIndicatorService

    {
        private readonly IBodyIndicatorsRepository _bodyIndicatorRepository;

        private readonly IMapper _mapper;

        public BodyIndicatorService(IBodyIndicatorsRepository bodyIndicatorRepository, IMapper mapper)
        {
            _bodyIndicatorRepository = bodyIndicatorRepository;
            _mapper = mapper;
        }

        public async Task<int> AddNewAsync(NewBodyIndicatorVm newBodyIndicatorVm, string userId)
        {
            var bodyIndicator = _mapper.Map<BodyIndicator>(newBodyIndicatorVm);
            bodyIndicator.BMI = Math.Round(bodyIndicator.Mass / Math.Pow(((double)bodyIndicator.Height / 100), 2), 2);
            bodyIndicator.BAI = Math.Round((double)bodyIndicator.HipCircumference / Math.Pow((double)bodyIndicator.Height / 100, 1.5), 2);
            if (bodyIndicator.Sex == "male")
            {
                bodyIndicator.PPM = Math.Round(66.47 + 13.75 * bodyIndicator.Mass + 5 * bodyIndicator.Height - 6.75 * bodyIndicator.Age, 2);
                bodyIndicator.BeW = Math.Round(-48.7 + 0.087 * bodyIndicator.AbdominalCircumference + 1.147 * bodyIndicator.HipCircumference - 0.003 * Math.Pow(bodyIndicator.HipCircumference, 2) - 7.195, 2);
            }
            else
            {
                bodyIndicator.PPM = Math.Round(665.09 + 9.56 * bodyIndicator.Mass + 1.85 * bodyIndicator.Height - 4.67 * bodyIndicator.Age, 2);
                bodyIndicator.BeW = Math.Round(-48.7 + 0.087 * bodyIndicator.AbdominalCircumference + 1.147 * bodyIndicator.HipCircumference - 0.003 * Math.Pow(bodyIndicator.HipCircumference, 2), 2);
            }
            bodyIndicator.CPM = Math.Round(bodyIndicator.PPM * (double)bodyIndicator.PAL / 10, 2);
            bodyIndicator.WHtR = Math.Round(((double)bodyIndicator.HipCircumference / (double)bodyIndicator.Height) * 100.0, 2);
            bodyIndicator.NMC = bodyIndicator.Height - 100;
            bodyIndicator.UserId = userId;
            bodyIndicator.Date = DateTime.Now;


            await _bodyIndicatorRepository.CreateAsync(bodyIndicator);
            return bodyIndicator.Id;
        }

        public async Task<BodyIndicator> GetByIdAsync(int bodyIndicatorId)
        {
            var bodyIndicator = await _bodyIndicatorRepository.GetByIdAsync(bodyIndicatorId);
            return bodyIndicator;
        }
    }
}
