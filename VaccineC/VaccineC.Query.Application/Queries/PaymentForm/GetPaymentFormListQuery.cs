﻿using MediatR;
using System.Collections.Generic;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PaymentForm
{
    public class GetPaymentFormListQuery : IRequest<IEnumerable<PaymentFormViewModel>>
    {
    }
}
