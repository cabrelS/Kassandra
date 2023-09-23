using Kassandra.Models;
using Kassandra.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kassandra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeysController : ControllerBase
{
    private readonly KeysService _keyService;

    public KeysController (KeysService keyService) => _keyService = keyService;

    [HttpGet]
    public async Task<List<Keys>> Get() => await _keyService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Keys>> GetAsync(string key = "any")
    {
        var _key = await _keyService.GetAsync(key);
        if (_key is null) return NotFound();
        return _key;
    }
}