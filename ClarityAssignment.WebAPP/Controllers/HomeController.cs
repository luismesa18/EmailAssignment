using ClarityAssignment.WebAPP.Models;
using ClarityAssignment.WebAPP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClarityAssignment.WebAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IEmailService emailService, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _logger = logger;
            _emailService = emailService;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }  
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult CreateEmail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmail(EmailViewModel emailViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(emailViewModel);
                var response = await _emailService.SendEmail(emailViewModel);
                if (response)
                    return View("EmailResult", emailViewModel);
                else
                {
                    ModelState.AddModelError(String.Empty, "Email sent failed. Try again.");
                    return View(emailViewModel);
                }
            }catch(Exception ex)
            {
                return View(emailViewModel);
            }
        }
        public IActionResult EmailResult()
        {
            return View();
        }
    }
}