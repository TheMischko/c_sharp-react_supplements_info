using Microsoft.AspNetCore.Mvc;
using SupplementsServer.API.Services;

namespace SupplementsServer.API.Controllers;

[ApiController]
[Route("[controller]")]
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
}
