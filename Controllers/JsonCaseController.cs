using System.Security.Cryptography;
using System.Text;
using KaizenCase.Helpers;
using KaizenCase.Interfaces;
using KaizenCase.Models;
using Microsoft.AspNetCore.Mvc;

namespace KaizenCase.Controllers;

[ApiController]
[Route("[controller]")]
public class JsonCaseController : ControllerBase
{
    
    private readonly ILogger<JsonCaseController> _logger;
    private readonly IJsonService _jsonService;

    public JsonCaseController(ILogger<JsonCaseController> logger,IJsonService jsonService)
    {
        _logger = logger;
        _jsonService = jsonService;
    }
    
    /// <summary>
    /// Gets OCR generated response.json data() than returns all lines.
    /// </summary>
    /// <remarks>
    /// ### DETAILS ###
    /// <br/>
    /// <br/>
    /// Input:  List &lt; ValueField &gt; 
    /// <br/>
    /// Output:  List &lt; string &gt;
    /// </remarks>
    [HttpPost("ParseJson")]
    public async Task<List<string>> ParseJson([FromBody] List<ValueField> req)
    {
        return await _jsonService.ParseJsonToLines(req);
    }
    
}