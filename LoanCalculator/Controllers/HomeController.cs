using System.Diagnostics;
using LoanCalculator.Models;
using LoanCalculator.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace LoanCalculator.Controllers
{
    public class HomeController : Controller
    {
        // Private logger for logging information, warnings, errors, etc.
        private readonly ILogger<HomeController> _logger;

        // Constructor to initialize the logger.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action method for the home page. This method responds to the root URL.
        [HttpGet]
        public IActionResult Index()
        {
            Loan model = new Loan();

            model.Payment = 0;
            model.TotalInterest = 0;
            model.TotalCost = 0;
            model.Rate = 3.5M;
            model.Amount = 15000M;
            model.Term = 60;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Loan model)
        {
            LoanHelper loanHelper = new LoanHelper();

            Loan newLoan = loanHelper.GetPayments(model);

            return View(newLoan);
        }


        // Action method for handling errors. Response caching is disabled for this method.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
