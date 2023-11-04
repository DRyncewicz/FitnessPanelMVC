﻿using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interfaces
{
    public interface IPdfReportGenerator
    {
        byte[] Generate(BodyIndicator data);
    }
}
