﻿using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IBudgetProductAppService
    {
        Task<IEnumerable<BudgetProductViewModel>> GetAllAsync();
        Task<IEnumerable<BudgetProductViewModel>> GetAllBudgetsProductsByBudgetId(Guid budgetId);
        Task<IEnumerable<BudgetProductViewModel>> GetAllPendingBudgetsProductsByBorrower(Guid budgetId, Guid borrowerId);
        Task<IEnumerable<BudgetProductViewModel>> GetAllPendingBudgetsProductsByResponsible(Guid budgetId);
        BudgetProductViewModel GetById(Guid id);
    }
}
