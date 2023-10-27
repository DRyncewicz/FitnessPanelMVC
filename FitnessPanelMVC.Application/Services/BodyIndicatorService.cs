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
            bodyIndicator.BMI = bodyIndicator.Mass * Math.Pow((bodyIndicator.Height), 2);
            bodyIndicator.PPM = 665.09 + 9.56 * bodyIndicator.Mass + 1.85 * bodyIndicator.Height - 4.67 * bodyIndicator.Age;
            bodyIndicator.CPM = bodyIndicator.PPM * (double)bodyIndicator.PAL / 10;
            bodyIndicator.WHtR = (bodyIndicator.HipCircumference / bodyIndicator.Height) * 100.0;
            bodyIndicator.NMC = bodyIndicator.Height - 100;
            bodyIndicator.BAI = bodyIndicator.HipCircumference / Math.Pow(bodyIndicator.Height, 1.5);
            if (bodyIndicator.Sex == "Male")
            {
                bodyIndicator.BeW = -48.7 + 0.087 * bodyIndicator.AbdominalCircumference + 1.147 * bodyIndicator.HipCircumference - 0.003 * Math.Pow(bodyIndicator.HipCircumference, 2) - 7.195;
            }
            else
            {
                bodyIndicator.BeW = -48.7 + 0.087 * bodyIndicator.AbdominalCircumference + 1.147 * bodyIndicator.HipCircumference - 0.003 * Math.Pow(bodyIndicator.HipCircumference, 2);
            }
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
