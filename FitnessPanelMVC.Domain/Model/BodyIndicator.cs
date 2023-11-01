using FitnessPanelMVC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Model
{
    public class BodyIndicator
    {
        public int Id { get; set; }

        public string Sex { get; set; }

        public double Mass { get; set; }

        public int Height { get; set; }

        public int Age { get; set; }

        public PAL PAL { get; set; }

        public double PPM { get; set; }

        public double CPM { get; set; }

        public double WHtR { get; set; }

        public double NMC { get; set; }

        public int HipCircumference { get; set; }

        public int AbdominalCircumference { get; set; }

        public double BAI { get; set; }

        public double BeW { get; set; }

        public double BMI { get; set; }
    }
}
