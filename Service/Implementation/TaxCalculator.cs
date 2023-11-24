using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data;
using TaxCalculator.Models;
using TaxCalculator.Service.Contract;

namespace TaxCalculator.Service.Implementation
{
    public class TaxCalculator : ITaxCalculator
    {
        private readonly ILogger<TaxCalculator> _logger;
        private readonly IConfiguration _configuration;
        private readonly TaxCalculationContext _taxContext;

        public TaxCalculator(ILogger<TaxCalculator> logger, IConfiguration configuration, TaxCalculationContext taxContext)
        {
            _logger = logger;
            _configuration = configuration;
            _taxContext = taxContext;
        }

        public decimal CalculateTax(string postalCode, decimal annualIncome)
        {
            var postalCodeEntity = _taxContext.PostalCodes.FirstOrDefault(pc => pc.Code == postalCode);
            if (postalCodeEntity == null)
            {
                _logger.LogError("Postal code not found.");
                throw new Exception("Postal code not found.");
            }

            switch (postalCodeEntity.TaxCalculationType)
            {
                case "Flat rate":
                    return annualIncome * 0.175m;

                case "Flat Value":
                    if (annualIncome < 200000)
                        return annualIncome * 0.05m;
                    else
                        return 10000;

                case "Progressive":
                    if (annualIncome <= 8350)
                        return annualIncome * 0.10m;
                    else if (annualIncome <= 33950)
                        return 835 + (annualIncome - 8350) * 0.15m;
                    else if (annualIncome <= 82250)
                        return 4675 + (annualIncome - 33950) * 0.25m;
                    else if (annualIncome <= 171550)
                        return 16750 + (annualIncome - 82250) * 0.28m;
                    else if (annualIncome <= 372950)
                        return 41754 + (annualIncome - 171550) * 0.33m;
                    else
                        return 110884 + (annualIncome - 372950) * 0.35m;

                default:
                    _logger.LogError("Tax calculation type not found.");
                    throw new Exception("Tax calculation type not found.");
            }
        }

    }
}
