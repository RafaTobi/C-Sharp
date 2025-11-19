using Groeiproject.BL;
using Groeiproject.BL.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Groeiproject.UI.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class BreadsController : ControllerBase
{
    private readonly IManager _mgr;

    public BreadsController(IManager mgr)
    {
        _mgr = mgr;
    }

    [HttpGet]
    public IActionResult Get()
    {
        IEnumerable<Bread> breads = _mgr.GetAllBreads();
        if (!breads.Any()) return NoContent();

        return Ok(breads);
    }
}