using Groeiproject.BL;
using Groeiproject.BL.Domain;
using Groeiproject.UI.Web.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Groeiproject.UI.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class BakeriesController : ControllerBase
{
    private readonly IManager _mgr;

    public BakeriesController(IManager mgr)
    {
        _mgr = mgr;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        IEnumerable<Bakery> bakeries = _mgr.GetAllBakeries();
        if (!bakeries.Any()) return NoContent();

        return Ok(bakeries);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        Bakery bakery = _mgr.GetBakery(id);
        if (bakery==null) return NoContent();
        return Ok(bakery);
    }
    
    [HttpGet("{id}/bakeriesFromBaker")]
    public IActionResult GetBakeriesOfBaker(int id)
    {
        IEnumerable<Bakery> bakeries = _mgr.GetBakeriesOfBaker(id);
        if (!bakeries.Any()) return NoContent();

        return Ok(bakeries);
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Post([FromBody] ContractDTO contractDto)
    {
        Baker baker = _mgr.GetBaker(contractDto.BakerId);
        Bakery bakery = _mgr.GetBakery(contractDto.BakeryId);
        Contract contract = new Contract()
        {
            Baker = baker,
            Bakery = bakery,
            StartDate = contractDto.StartDate,
            EndDate = contractDto.EndDate,
            Price = contractDto.Price
        };
        if (_mgr.ContractExists(contract.Id)) return Conflict();
        _mgr.AddContract(contract);
        return CreatedAtAction("GET", new { controller = "Bakeries", id = contract.Bakery.Id }, contract);
    }

    [HttpPut("{id}/update")]
    [Authorize]
    public IActionResult PutName(int id, [FromBody] string newAddress)
    {
        _mgr.UpdateAddress(id, newAddress);
        return NoContent();
    }

    [HttpPost("add")]
    [Authorize]
    public IActionResult Add([FromBody] Bakery newBakery)
    {
        _mgr.AddBakery(newBakery.Name, newBakery.Adres);
        return CreatedAtAction("GET", new { Controller = "Bakeries", id = newBakery.Id }, newBakery);
    }
}