using System.Security.Cryptography;
using System.Text;
using KaizenCase.Helpers;
using KaizenCase.Interfaces;
using KaizenCase.Models;
using Microsoft.AspNetCore.Mvc;

namespace KaizenCase.Controllers;

[ApiController]
[Route("[controller]")]
public class CodeCaseController : ControllerBase
{
    
    private readonly ILogger<CodeCaseController> _logger;
    private readonly ICodeGeneratorService _codeGeneratorService;

    public CodeCaseController(ILogger<CodeCaseController> logger,ICodeGeneratorService codeGeneratorService)
    {
        _logger = logger;
        _codeGeneratorService = codeGeneratorService;
    }
    
    /// <summary>
    /// Generates and returns 8-length promotion code as much as the given integer input.
    /// </summary>
    /// <remarks>
    /// ### DETAILS ###
    /// <br/>
    /// <br/>
    /// Input: int
    /// <br/>
    /// Output: List &lt; string &gt;
    /// </remarks>
    [HttpPost("GenerateCode")]
    public async Task<List<string>> GenerateCode([FromBody] int count)
    {
        return await _codeGeneratorService.GenerateCode(count);
    }
       
    
    /// <summary>
    /// Checks the given code input.
    /// </summary>
    /// <remarks>
    /// ### DETAILS ###
    /// <br/>
    /// <br/>Returns "Valid" for valid code and "NotValid" for not valid code.
    /// <br/>
    /// Input: string
    /// <br/>
    /// Output: string ("Valid" || "NotValid")
    /// </remarks>
    [HttpPost("CheckCode")]
    public async Task<string> CheckCode([FromBody] string code)
    {
        return await _codeGeneratorService.CheckCode(code);
    }
    
    
    /// <summary>
    /// Generates 8-length promotion code as much as the given integer input. And checks all.
    /// </summary>
    /// <remarks>
    /// ### DETAILS ###
    /// <br/>
    /// <br/>Returns "All Checked With {failCount} Fail Count"
    /// <br/>
    /// Input: int
    /// <br/>
    /// Output: string 
    /// </remarks>
    [HttpPost("GenerateAndCheckAll")]
    public async Task<string> GenerateAndCheckAll([FromBody] int count)
    {
        return await _codeGeneratorService.GenerateAndCheckAll(count);
    }
    

}