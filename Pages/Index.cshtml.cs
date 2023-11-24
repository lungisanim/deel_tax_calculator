using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data;
using TaxCalculator.Models;
using TaxCalculator.Service.Contract;
using TaxCalculator.Service.Implementation;

namespace TaxCalculator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly TaxCalculationContext _context;
        private readonly ITaxCalculator _taxCalculator;

        public SelectList PostalCodes;
        public string PostalCode;
        public decimal AnnualIncome;

        public IndexModel(ILogger<IndexModel> logger, TaxCalculationContext context, ITaxCalculator taxCalculator)
        {
            _logger = logger;
            _context = context;
            PostalCode = string.Empty;
            AnnualIncome = 0;
            PostalCodes = new SelectList(_context.PostalCodes, "Code", "Code");
            _taxCalculator = taxCalculator;
        }

        public void OnGet()
        {
            _logger.LogInformation("Postal codes fetched...");
            ViewData["PostalCodes"] = PostalCodes;
        }

        public async Task<IActionResult> OnPost(TaxCalculation index)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Tax Calculations for {0} - {1}", index.PostalCode, index.AnnualIncome);
                    TaxCalculation taxCalculation = new()
                    {
                        CalculatedTax = _taxCalculator.CalculateTax(index.PostalCode, index.AnnualIncome),
                        PostalCode = index.PostalCode.ToString(),
                        AnnualIncome = index.AnnualIncome,
                        CalculationDate = DateTime.Now
                    };

                    _context.TaxCalculations.Add(taxCalculation);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Tax Calculations for {0} - {1}, were successfully saved", index.PostalCode, index.AnnualIncome);
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex?.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}