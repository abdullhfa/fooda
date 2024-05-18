using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFood.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Policy = "UserPolicy")]
    public IActionResult Method()
    {
        return View();
    }
}
