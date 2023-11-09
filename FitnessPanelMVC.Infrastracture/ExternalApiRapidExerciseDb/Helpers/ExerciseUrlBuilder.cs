using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.ExternalApiRapidExerciseDb.Helpers
{
    public class ExerciseUrlBuilder
    {
        private readonly string bodyPartUrl;

        private StringBuilder sb = new StringBuilder();

        public ExerciseUrlBuilder(IConfiguration configuration)
        {
            bodyPartUrl = bodyPartUrl = configuration["BodyPartUrl"];
        }

        public string HttpUlrBuilder(string bodyPart)
        {
            sb.Append(bodyPartUrl);
            sb.Append(bodyPart);

            return sb.ToString();
        }
    }
}