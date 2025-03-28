using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebsiteLibrary.Models;

namespace WebsiteLibrary.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            if (User.IsInRole("Reader"))
                return RedirectToAction("Index", "Reader");
            if (User.IsInRole("Librarian"))
                return RedirectToAction("Dashboard", "Admin");
        }
        return View();
    }

    public IActionResult About()
    {
        return View(); // Có th? tái s? d?ng layout này cho About
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
