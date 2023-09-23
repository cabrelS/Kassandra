using Kassandra.Models;
using Kassandra.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kassandra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShortMessageController : ControllerBase
{
    private readonly ShortMessagesService _shortMessageService;
    public ShortMessageController(ShortMessagesService shortMessageService) => _shortMessageService = shortMessageService;

    [HttpGet("{key}")]
    public async Task<ActionResult<ShortMessage>> Get(string key)
    {
        var _shortMessage = await _shortMessageService.GetAny(key);
        if (_shortMessage is null) return NotFound();
        return _shortMessage;
    }
}