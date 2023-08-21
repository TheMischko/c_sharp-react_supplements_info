using Microsoft.AspNetCore.Mvc;
using SupplementsServer.API.Models;
using SupplementsServer.API.Services;

namespace SupplementsServer.API.Controllers;

[ApiController]
[Route("supplements")]
public class SupplementsController : ControllerBase
{

    private readonly ILogger<SupplementsController> _logger;
    private readonly ISupplementService _supplementService;

    public SupplementsController(ISupplementService supplementService) {
        _supplementService = supplementService;
    }

    [HttpGet(Name = "GetSupplements")]
    public async Task<IActionResult> Get() {
        var supplements =  await _supplementService.GetAllSupplements();
        if (supplements.Any()) {
            return Ok(supplements);    
        }

        return NotFound();
    }

    [HttpGet("find/{name}", Name = "GetFindSupplements")]
    public async Task<IActionResult> GetFind(string name) {
        var supplements =  await _supplementService.GetAllSupplements();
        try {
            Supplement[] supplement = supplements.Where(s => s.Name.Contains(name)).ToArray();
            if (supplement.Length == 0)
                throw new Exception();
            return Ok(supplement);
        }
        catch (Exception) {
            return NotFound();
        }
        
        
    }
}
