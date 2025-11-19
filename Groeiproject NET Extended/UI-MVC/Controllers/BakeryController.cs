using Groeiproject.BL;
using Groeiproject.BL.Domain;
using Groeiproject.UI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Groeiproject.UI.Web.Controllers;

public class BakeryController : Controller
{
    private readonly IManager _mgr;
    private UserManager<IdentityUser> _usermgr;

    public BakeryController(IManager manager, UserManager<IdentityUser> userManager)
    {
        _mgr = manager;
        _usermgr = userManager;
    }

    public IActionResult Index()
    {
        IEnumerable<Bakery> bakeries = _mgr.GetAllBakeries();
        return View(bakeries);
    }

    [HttpGet]
    public IActionResult Add()
    {
        ViewData["Bakers"] = new List<Baker>();
        return View();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Add(NewBakeryViewModel bakery)
    {
        if (!ModelState.IsValid)
        {
            return View(bakery);
        }

        Bakery newBakery = _mgr.AddBakeryWithMaintainer(bakery.Name, bakery.Adress, _usermgr.GetUserAsync(User).Result);
        return RedirectToAction("Details", new { Id = newBakery.Id });
    }

    public IActionResult Details(int id)
    {
        Bakery bakery = _mgr.GetBakeryWithBakers(id);
        return View(bakery);
    }
}