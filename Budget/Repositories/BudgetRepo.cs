using Butget.Models;

namespace Butget.Repositories;

public interface IBudgetRepo
{
    List<Budget> GetAll();
}

public class BudgetRepo : IBudgetRepo
{
    public List<Budget> GetAll()
    {
        throw new NotImplementedException();
    }
}