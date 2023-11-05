using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FitnessPanelMVC.Domain.Model;
using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using FitnessPanelMVC.Application.ViewModels.UserReportFile;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using FitnessPanelMVC.Application.ViewModels.User;

namespace FitnessPanelMVC.Application.Services
{
    public class UserReportService : IUserReportService
    {
        private readonly IPdfReportGenerator _pdfReportGenerator;

        private readonly IUserReportFileRepository _userReportFileRepository;

        private readonly IMapper _mapper;

        private readonly string filePath = @"C:\Users\ereen\Temp";

        public UserReportService(IPdfReportGenerator pdfReportGenerator,
            IUserReportFileRepository userReportFileRepository,
            IMapper mapper)
        {
            _pdfReportGenerator = pdfReportGenerator;
            _userReportFileRepository = userReportFileRepository;
            _mapper = mapper;
        }

        public async Task CreateUserBodyReportAsync(BodyIndicator bodyIndicators, string userId)
        {
            byte[] reportPdfFile = await _pdfReportGenerator.Generate(bodyIndicators);
            string fileName = "BodyMetricReport-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            string finalPath = Path.Combine(filePath, fileName);
            await File.WriteAllBytesAsync(finalPath, reportPdfFile);

            NewUserReportFileVm newUserReportFileVm = new NewUserReportFileVm()
            {
                UserId = userId,
                Name = fileName,
                Path = finalPath,
                CreationDate = DateTime.Now
            };
            var userReportFile = _mapper.Map<UserReportFile>(newUserReportFileVm);

            await _userReportFileRepository.CreateAsync(userReportFile);
        }

        public async Task<IEnumerable<UserReportForListVm>> GetUserBodyReportsAsync(string userId)
        {
            var reports = await _userReportFileRepository.GetAll().Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreationDate).Take(5)
                .Select(r => new UserReportForListVm()
                {
                    Path = r.Path,
                    CreationDate = r.CreationDate,
                    Id = r.Id,
                    UserId = r.UserId
                }).ToListAsync();


            return reports;
        }
    }
}