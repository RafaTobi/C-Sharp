using Groeiproject.BL;
using Groeiproject.BL.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Groeiproject.UI.Web.Controllers;

public class BakerController : Controller
{
    private readonly IManager _mgr;

    public BakerController(IManager manager)
    {
        _mgr = manager;
    }
    
    public IActionResult BakerDetails(int id)
    {
        Baker baker = _mgr.GetBakerWithBakeries(id);
        return View(baker);
    }
}