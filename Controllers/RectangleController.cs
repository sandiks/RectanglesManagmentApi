using Microsoft.AspNetCore.Mvc;
using RectanglesManagmentApi.Authorization;
using RectanglesManagmentApi.Mappings;
using RectanglesManagmentApi.Services;

namespace RectanglesManagmentApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RectangleController : ControllerBase
{
    private readonly ILogger<RectangleController> _logger;
    private readonly IRectangleService _rectService;

    public RectangleController(ILogger<RectangleController> logger, IRectangleService rectService)
    {
        _logger = logger;
        _rectService = rectService;
    }

    [HttpGet("Generate")]
    public IActionResult Generate()
    {
        return Ok(_rectService.GenerateRectangles(200));
    }

    [Authorize]
    [HttpGet("GenerateAndSave")]
    public async Task<IActionResult> GenerateAndSave()
    {
        var data = _rectService.GenerateRectangles(100);
        await _rectService.BulkInsert(data.Select(r => r.ToEntity()).ToList());
        return Ok();
    }

    [Authorize]
    [HttpPost("Filter")]
    public async Task<IActionResult> FilterRectangles([FromBody] List<int> coords)
    {
        var data = await _rectService.FilterRectanglesByCoordinates(coords.ListToPoints());
        return Ok(data);
    }

    [HttpPost("Transform")]
    public async Task<IActionResult> TransformListToPoints([FromBody] List<int> coords)
    {
        return Ok(coords.ListToPoints());
    }
}


