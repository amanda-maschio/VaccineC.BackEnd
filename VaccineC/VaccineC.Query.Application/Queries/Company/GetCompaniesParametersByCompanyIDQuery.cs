﻿using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Company
{
    public class GetCompaniesParametersByCompanyIDQuery : IRequest<CompaniesParametersViewModel>
    {
        public Guid ID { get; set; }

        public GetCompaniesParametersByCompanyIDQuery(Guid id)
        {
            ID = id;
        }
    }
}