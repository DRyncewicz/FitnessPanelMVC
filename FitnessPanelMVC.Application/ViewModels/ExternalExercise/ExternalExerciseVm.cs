using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.ExternalExercise
{
    public class ExternalExerciseVm
    {
        public string BodyPart { get; set; }

        public string Equipment { get; set; }

        public string Id { get; set; }

        public string GifUrl { get; set; }

        public string Name { get; set; }

        public string Target { get; set; }

        public List<string> SecondaryMuscles { get; set; }

        public List<string> Instructions { get; set; }
    }
}
