﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestFoursys.Application.Mapping
{
    public interface IMapFrom<T>
    {
        [ExcludeFromCodeCoverage]
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
