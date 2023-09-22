using Kassandra.Models;
using Kassandra.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kassandra.Controlles;

[ApiController]
[Route("api/[controller]")]
public class KeysController : ControllerBase
{
    private readonly KeysService _keyService;

    public KeysController (KeysService keyService) => _keyService = keyService;

    [HttpGet]
    public async Task<List<Keys>> Get() => await _keyService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Keys>> Get(string id)
    {
        var key = await _keyService.GetAsync(id);
        if (key is null) return NotFound();
        return key;
    }

    [HttpGet]
    public async Task<ActionResult<Keys>> GetAny()
    {
        var key = await _keyService.GetAnyAsync();
        if (key is null) return NotFound();
        return key;
    }
}