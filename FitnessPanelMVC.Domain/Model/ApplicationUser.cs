using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public double? Weight { get; set; }

        public string? Sex { get; set; }

        public virtual ICollection<UserReportFile> UserReportFiles { get; set; }
    }
}
