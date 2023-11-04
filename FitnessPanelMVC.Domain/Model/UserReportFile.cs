using FitnessPanelMVC.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Model
{
    public class UserReportFile : BaseEntity
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Path { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
