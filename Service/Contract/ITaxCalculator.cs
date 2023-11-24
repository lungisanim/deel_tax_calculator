
using TaxCalculator.Models;

namespace TaxCalculator.Service.Contract
{
    public interface ITaxCalculator
    {
        decimal CalculateTax(string postalCode, decimal annualIncome);
    }
}
