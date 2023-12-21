namespace Butget.Models;

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }
    public DateTime StartOfMonth => DateTime.ParseExact(YearMonth, "yyyyMM", null);

    public int GetPartialAmount(int days)
    {
        return Amount/StartOfMonth.Day * days;
    }
}