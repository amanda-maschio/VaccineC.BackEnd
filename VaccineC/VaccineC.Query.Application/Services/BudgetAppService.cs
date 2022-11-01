﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Application.Services
{
    public class BudgetAppService : IBudgetAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public BudgetAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context; 
        }

        public async Task<IEnumerable<BudgetViewModel>> GetAllAsync()
        {
            var budgets = await _queryContext.AllBudgets.ToListAsync();
            var budgetsViewModel = budgets.Select(r => _mapper.Map<BudgetViewModel>(r)).ToList();
            return budgetsViewModel;
        }

        public async Task<IEnumerable<BudgetViewModel>> GetByName(string personName)
        {

            var budgets = await _queryContext.AllBudgets.ToListAsync();
            var budgetsViewModel = budgets
                .Select(r => _mapper.Map<BudgetViewModel>(r))
                .Where(r => r.Persons.Name.Contains(personName, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return budgetsViewModel;

        }

        public async Task<IEnumerable<BudgetViewModel>> GetAllByBudgetNumber(int budgetNumber)
        {
            var budgets = await _queryContext.AllBudgets.ToListAsync();
            var budgetsViewModel = budgets
                .Select(r => _mapper.Map<BudgetViewModel>(r))
                .Where(r => r.BudgetNumber == budgetNumber).ToList();
            return budgetsViewModel;
        }

        public async Task<IEnumerable<BudgetViewModel>> GetAllByResponsible(Guid responsibleId)
        {
            var budgets = (from b in _context.Budgets
                           where b.PersonId.Equals(responsibleId) && b.Situation.Equals("A")
                           orderby b.Register descending
                           select b).ToList();

            var budgetsViewModel = _mapper.Map<IEnumerable<BudgetViewModel>>(budgets);

            foreach (var budget in budgetsViewModel)
            {
                var person = (from p in _context.Persons
                              where p.ID.Equals(budget.PersonId) 
                              select p).FirstOrDefault();

                var personViewModel = _mapper.Map<PersonViewModel>(person);

                budget.Persons = personViewModel;
            }

            return budgetsViewModel;
        }

        public async Task<IEnumerable<BudgetViewModel>> GetAllByBorrower(Guid borrowerId)
        {

            var budgetsId = (from b in _context.Budgets
                            join bp in _context.BudgetsProducts on b.ID equals bp.BudgetId
                            where bp.BorrowerPersonId.Equals(borrowerId) && bp.SituationProduct.Equals("P")
                            select b).Select(b => b.ID).Distinct().ToList();

            var budgets = (from b in _context.Budgets
                          where budgetsId.Contains(b.ID) && b.Situation.Equals("A")
                          orderby b.Register descending
                          select b).ToList();

            var budgetsViewModel = _mapper.Map<IEnumerable<BudgetViewModel>>(budgets);

            foreach(var budget in budgetsViewModel)
            {
                var person = (from p in _context.Persons
                                where p.ID.Equals(budget.PersonId)
                                select p).FirstOrDefault();

                var personViewModel = _mapper.Map<PersonViewModel>(person);

                budget.Persons = personViewModel;
            }

            return budgetsViewModel;
        }

        public async Task<BudgetViewModel> GetById(Guid id)
        {
            var budget = _mapper.Map<BudgetViewModel>(_queryContext.AllBudgets.Where(r => r.ID == id).FirstOrDefault());
            return budget;
        }

        public async Task<BudgetDashInfoViewModel> GetBudgetsDashInfo(int month, int year)
        {
            DateTime dateSearchMinimum = new DateTime(year, month, 1);
            DateTime dateSearchMaximum = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            DateTime dateSearchMinimumPrevious = dateSearchMinimum.AddMonths(-1);
            DateTime dateSearchMaximumPrevious = dateSearchMaximum.AddMonths(-1);

            BudgetDashInfoViewModel budgetDashInfoViewModel = new BudgetDashInfoViewModel();
            budgetDashInfoViewModel.totalBudgetDiscount = 0;

            var budgets = (from b in _context.Budgets
                          join p in _context.Persons on b.PersonId equals p.ID
                          where b.Register >= dateSearchMinimum.Date
                          where b.Register <= dateSearchMaximum.Date
                          select new BudgetViewModel
                          {
                              Situation = b.Situation,
                              DiscountPercentage = b.DiscountPercentage,
                              DiscountValue = b.DiscountValue,
                              TotalBudgetedAmount = b.TotalBudgetedAmount, 
                              Persons = new PersonViewModel()
                              {
                                  PersonType = p.PersonType
                              }
                          }).ToList();

            var budgetsPrevious = (from b in _context.Budgets
                                   join p in _context.Persons on b.PersonId equals p.ID
                                   where b.Register >= dateSearchMinimumPrevious.Date
                                   where b.Register <= dateSearchMaximumPrevious.Date
                                   select new BudgetViewModel
                                   {
                                       DiscountPercentage = b.DiscountPercentage,
                                       DiscountValue = b.DiscountValue,
                                       TotalBudgetedAmount = b.TotalBudgetedAmount,
                                   }).ToList();

            var budgetsAmount = (from bn in _context.BudgetsNegotiations
                                 join b in _context.Budgets on bn.BudgetId equals b.ID
                                 where b.Register >= dateSearchMinimum.Date
                                 where b.Register <= dateSearchMaximum.Date
                                 select bn.TotalAmountTraded).Sum();

            var budgetsAmountPrevious = (from bn in _context.BudgetsNegotiations
                                         join b in _context.Budgets on bn.BudgetId equals b.ID
                                         where b.Register >= dateSearchMinimumPrevious.Date
                                         where b.Register <= dateSearchMaximumPrevious.Date
                                         select bn.TotalAmountTraded).Sum();



            foreach (var budget in budgets)
            {
                if (budget.Situation.Equals("A")) 
                {
                    budgetDashInfoViewModel.budgetAprovedNumber++;
                }
                else if (budget.Situation.Equals("P")) 
                {
                    budgetDashInfoViewModel.budgetPendingNumber++;
                }
                else if (budget.Situation.Equals("X"))
                {
                    budgetDashInfoViewModel.budgetCanceledNumber++;
                }
                else if (budget.Situation.Equals("V"))
                {
                    budgetDashInfoViewModel.budgetOverduedNumber++;
                }
                else if (budget.Situation.Equals("F"))
                {
                    budgetDashInfoViewModel.budgetFinalizedNumber++;
                }
                else if (budget.Situation.Equals("E"))
                {
                    budgetDashInfoViewModel.budgetNegotiationNumber++;
                }

                if (budget.Persons.PersonType.Equals("F")) 
                {
                    budgetDashInfoViewModel.personPhysicalNumber++;
                }
                else
                {
                    budgetDashInfoViewModel.personJuridicalNumber++;
                }

                if (budget.DiscountValue != 0 && budget.DiscountPercentage == 0)
                {
                    budgetDashInfoViewModel.totalBudgetDiscount += budget.DiscountValue;
                } 
                else if (budget.DiscountValue == 0 && budget.DiscountPercentage != 0) 
                { 
                    var percentageNumber = budget.DiscountPercentage/100;
                    var result = budget.TotalBudgetedAmount * percentageNumber;

                    budgetDashInfoViewModel.totalBudgetDiscount += result;
                }

            }

            foreach (var budgetPrevious in budgetsPrevious)
            {
                if (budgetPrevious.DiscountValue != 0 && budgetPrevious.DiscountPercentage == 0)
                {
                    budgetDashInfoViewModel.totalBudgetDiscountPrevious += budgetPrevious.DiscountValue;
                }
                else if (budgetPrevious.DiscountValue == 0 && budgetPrevious.DiscountPercentage != 0)
                {
                    var percentageNumber = budgetPrevious.DiscountPercentage / 100;
                    var result = budgetPrevious.TotalBudgetedAmount * percentageNumber;

                    budgetDashInfoViewModel.totalBudgetDiscountPrevious += result;
                }
            }

            budgetDashInfoViewModel.totalBudgetNumber = budgets.Count();
            budgetDashInfoViewModel.totalBudgetNumberPrevious = budgetsPrevious.Count();
            budgetDashInfoViewModel.totalBudgetAmount = budgetsAmount;
            budgetDashInfoViewModel.totalBudgetAmountPrevious = budgetsAmountPrevious;

            return budgetDashInfoViewModel;

        }
    }
}
