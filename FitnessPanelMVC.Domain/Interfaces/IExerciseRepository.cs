﻿using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IExerciseRepository
    {
        public int Create(Exercise exercise);

        public void Delete(int id);

        public IQueryable<Exercise> GetAll();
    }
}
