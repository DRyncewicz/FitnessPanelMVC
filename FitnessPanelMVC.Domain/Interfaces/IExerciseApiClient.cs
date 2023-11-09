using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interfaces
{
    public interface IExerciseApiClient
    {
        Task<HttpResponseMessage> GetExerciseByBodyPartAsync(string bodyPart);
    }
}
