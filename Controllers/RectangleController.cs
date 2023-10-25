using Microsoft.AspNetCore.Mvc;
using RectanglesManagmentApi.Mappings;
using RectanglesManagmentApi.Models;
using RectanglesManagmentApi.Services;

namespace RectanglesManagmentApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RectangleController : ControllerBase
{
    private readonly ILogger<RectangleController> _logger;
    private readonly RectangleManager _rectangleManager;
    private readonly IRectangleService _rectService;

    public RectangleController(ILogger<RectangleController> logger, RectangleManager rectangleManager, IRectangleService rectService)
    {
        _logger = logger;
        _rectangleManager = rectangleManager;
        _rectService = rectService;
    }

    [HttpGet("Generate")]
    public IActionResult Generate()
    {
        return Ok(_rectangleManager.GenerateRectangles(200));
    }

    [HttpGet("GenerateAndSave")]
    public async Task<IActionResult> GenerateAndSave()
    {
        var data = _rectangleManager.GenerateRectangles(10);
        await _rectService.BulkInsert(data.Select(r => r.ToEntity()).ToList());
        return Ok();
    }

    [HttpPost("Filter")]
    public async Task<IActionResult> FilterRectangles([FromBody] List<int> coords)
    {
        var rectangles = (await _rectService.GetRectangles()).Select(r => r.ToModel());
        var data = _rectangleManager.FilterRectanglesByCoordinates(rectangles, coords.ListToPoints());
        return Ok(data);
    }

    [HttpPost("Transform")]
    public async Task<IActionResult> TransformListToPoints([FromBody] List<int> coords)
    {
        return Ok(coords.ListToPoints());
    }
}


