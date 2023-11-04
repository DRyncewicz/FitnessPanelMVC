﻿using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interfaces
{
    public interface IXmlProductReader
    {
        Task<List<Product>> ReadFromFile(string? filePath, string userId);
    }
}
