﻿using AutoMapper;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Application.Mapper
{
    public class QueryModelMapper : Profile
    {
        public QueryModelMapper()
        {
            CreateMap<Example, ExampleViewModel>();
        }
    }
}