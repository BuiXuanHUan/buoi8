﻿using Lab05.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05.BUS
{
    public class FacultyService
    {
        public List<Faculty> GetAll()
        {
            Model1 model1 = new Model1();
            return model1.Faculties.ToList();
        }
    }
}
