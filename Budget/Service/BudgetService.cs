using System.Runtime.InteropServices.JavaScript;
using Butget.Repositories;

namespace Butget.Service;

public class BudgetService
{
    private IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime startDate, DateTime endDate)
    {
        var budgets = _budgetRepo.GetAll();
        var matchedBudgets = budgets.Where(x => x.StartOfMonth >= startDate && x.StartOfMonth <= endDate);
        var totalBudget = 0;
        foreach (var matchedBudget in matchedBudgets)
        {
            if (matchedBudget.StartOfMonth.Year == startDate.Year &&
                matchedBudget.StartOfMonth.Month == startDate.Month)
            {
                var endOfMonth = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
                var days = (endOfMonth.Date - startDate.Date).Days + 1;
                totalBudget += matchedBudget.GetPartialAmount(days);
            }

            if (matchedBudget.StartOfMonth.Year == endDate.Year && matchedBudget.StartOfMonth.Month == endDate.Month)
            {
                var days = (endDate.Date - matchedBudget.StartOfMonth.Date).Days + 1;
                totalBudget += matchedBudget.GetPartialAmount(days);
            }

            totalBudget += matchedBudget.Amount;
        }

        return totalBudget;
    }
}