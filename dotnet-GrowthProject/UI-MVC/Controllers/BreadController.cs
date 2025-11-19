using Groeiproject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Groeiproject.UI.Web.Controllers;

public class BreadController : Controller
{
    private readonly IManager _mgr;

    public BreadController(IManager manager)
    {
        _mgr = manager;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    //Add verwijderd, Dit stond er nog maar stond niet aangeduid als error
}