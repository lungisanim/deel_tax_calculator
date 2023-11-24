using Microsoft.EntityFrameworkCore;
using TaxCalculator.Models;

namespace TaxCalculator.Data
{
    public class TaxCalculationContext : DbContext
    {
        public TaxCalculationContext(DbContextOptions<TaxCalculationContext> options) : base(options)
        {
        }

        public DbSet<TaxCalculation> TaxCalculations => Set<TaxCalculation>();
        public DbSet<PostalCodes> PostalCodes => Set<PostalCodes>();
    }
}
