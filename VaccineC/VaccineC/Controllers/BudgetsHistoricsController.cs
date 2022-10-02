﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Queries.BudgetHistoric;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsHistoricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BudgetsHistoricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<BudgetsHistoricsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetBudgetHistoricListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<BudgetsHistoricsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetBudgetHistoricByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<BudgetsHistoricsController>/5/GetBudgetsHistoricsByBudget
        [HttpGet("{budgetId}/GetBudgetsHistoricsByBudget")]
        public async Task<IActionResult> GetAllBudgetsNegotiationsByBudgetID(Guid budgetId)
        {
            var command = new GetBudgetHistoricListByBudgetQuery(budgetId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
