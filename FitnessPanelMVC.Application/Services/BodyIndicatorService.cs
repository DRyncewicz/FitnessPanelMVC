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

        public int AddNewBodyIndicator(NewBodyIndicatorVm newBodyIndicatorVm)
        {
            var bodyIndicator = _mapper.Map<BodyIndicator>(newBodyIndicatorVm);
            bodyIndicator.BMI = bodyIndicator.Mass / Math.Pow(((double)bodyIndicator.Height/100), 2);
            bodyIndicator.BAI = (double)bodyIndicator.HipCircumference / Math.Pow((double)bodyIndicator.Height/100, 1.5);
            if (bodyIndicator.Sex == "male")
            {
                bodyIndicator.PPM = 66.47 + 13.75 * bodyIndicator.Mass + 5 * bodyIndicator.Height - 6.75 * bodyIndicator.Age;
                bodyIndicator.BeW = -48.7 + 0.087 * bodyIndicator.AbdominalCircumference + 1.147 * bodyIndicator.HipCircumference - 0.003 * Math.Pow(bodyIndicator.HipCircumference, 2) - 7.195;
            }
            else
            {
                bodyIndicator.PPM = 665.09 + 9.56 * bodyIndicator.Mass + 1.85 * bodyIndicator.Height - 4.67 * bodyIndicator.Age;
                bodyIndicator.BeW = -48.7 + 0.087 * bodyIndicator.AbdominalCircumference + 1.147 * bodyIndicator.HipCircumference - 0.003 * Math.Pow(bodyIndicator.HipCircumference, 2);
            }
            bodyIndicator.CPM = bodyIndicator.PPM * (double)bodyIndicator.PAL / 10;
            bodyIndicator.WHtR = ((double)bodyIndicator.HipCircumference / (double)bodyIndicator.Height) * 100.0;
            bodyIndicator.NMC = bodyIndicator.Height - 100;

            _bodyIndicatorRepository.CreateBodyIndicator(bodyIndicator);
            return bodyIndicator.Id;
        }


        public BodyIndicator GetBodyIndicatorById(int bodyIndicatorId)
        {
            var bodyIndicator = _bodyIndicatorRepository.GetAllBodyIndicators().FirstOrDefault(b => b.Id == bodyIndicatorId);
            return bodyIndicator;
        }


    }
}
