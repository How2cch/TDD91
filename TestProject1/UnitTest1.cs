using Butget.Models;
using Butget.Repositories;
using Butget.Service;
using FluentAssertions;
using NSubstitute;

namespace TestProject1;

public class Tests
{
    private IBudgetRepo _budgetRepo = null!;
    private BudgetService _budgetService = null!;

    [SetUp]
    public void Setup()
    {
        _budgetRepo = Substitute.For<IBudgetRepo>();
        _budgetService = new BudgetService(_budgetRepo);
    }

    [Test]
    public void whole_month_budget()
    {
        GivenBudget();
        var actual = AfterQuery(new DateTime(2023, 12, 01), new DateTime(2023, 12, 31));
        actual.Should().Be(3100);
    }


    [Test]
    public void partial_month_budget()
    {
        GivenBudget();
        var actual = AfterQuery(new DateTime(2023, 12, 01), new DateTime(2023, 12, 5));
        actual.Should().Be(500);
    }

    [Test]
    public void cross_Month()
    {
        GivenBudget();
        var actual = AfterQuery(new DateTime(2023, 12, 25), new DateTime(2024, 1, 11));
        actual.Should().Be(2900);
    }

    private decimal AfterQuery(DateTime startDate, DateTime endDate)
    {
        return _budgetService.Query(startDate, endDate);
    }


    private void GivenBudget()
    {
        _budgetRepo.GetAll().Returns(new List<Budget>()
        {
            new()
            {
                YearMonth = "202312",
                Amount = 3100,
            },
            new()
            {
                YearMonth = "202401",
                Amount = 6200,
            },
            new()
            {
                YearMonth = "202402",
                Amount = 2900,
            }

        });
    }
}