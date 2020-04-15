﻿using AutoMapper;
using SL.Sigesoft.WebApi.Domain.Models;
using SL.Sigesoft.WebApi.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SL.Sigesoft.WebApi.Mapping
{
    public class ModelToResourceProfile: Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Product, ProductResource>();
        }
    }
}