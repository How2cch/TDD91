using System.Runtime.InteropServices.JavaScript;
using Butget.Models;
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
        
        var matchedBudgets = budgets.Where(x =>
        {
            return x.StartOfMonth.Month >= startDate.Month && x.StartOfMonth.Year >= startDate.Year && x.StartOfMonth.Year <= endDate.Year;
        });
        var a = new DateTime(startDate.Year,startDate.Month,0);
        var b = new DateTime(endDate.Year,endDate.Month,0);
        var enumerable = budgets.Where(x=>x.StartOfMonth >= a && x.StartOfMonth <=b);
        var totalBudget = 0;
        foreach (var matchedBudget in matchedBudgets)
        {
            if (matchedBudget.IsSameMonth(startDate) && matchedBudget.IsSameMonth(endDate))
            {
                var days = (endDate - startDate).Days + 1;
                totalBudget += matchedBudget.GetPartialAmount(days);
            }
            else if (matchedBudget.IsSameMonth(startDate) )
            {
                var endOfMonth = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
                var days = (endOfMonth.Date - startDate.Date).Days + 1;
                totalBudget += matchedBudget.GetPartialAmount(days);
            }
            else if (matchedBudget.IsSameMonth(endDate))
            {
                var days = (endDate.Date - matchedBudget.StartOfMonth.Date).Days + 1;
                totalBudget += matchedBudget.GetPartialAmount(days);
            }
            else
            {
                totalBudget += matchedBudget.Amount;
            }

        }

        return totalBudget;
    }
}