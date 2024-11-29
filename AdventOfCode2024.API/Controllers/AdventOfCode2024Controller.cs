using AdventOfCode2024.BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using OperationResults.AspNetCore;

namespace AdventOfCode2024.API.Controllers;

[ApiController]
[Route("[action]")]
public class AdventOfCode2024Controller : ControllerBase
{
    private readonly ILogger<AdventOfCode2024Controller> _logger;
    private readonly IAdventOfCode2024Service _adventOfCode2024Service;

    public AdventOfCode2024Controller(ILogger<AdventOfCode2024Controller> logger, IAdventOfCode2024Service adventOfCode2024Service)
    {
        _logger = logger;
        _adventOfCode2024Service = adventOfCode2024Service;
    }

    /// <summary>
    /// Day one
    /// </summary>
    /// <returns>int</returns>
    [HttpGet]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> DayOne()
    {
        var result = await _adventOfCode2024Service.DayOneAsync();
        var response = HttpContext.CreateResponse(result);

        return response;
    }
}
