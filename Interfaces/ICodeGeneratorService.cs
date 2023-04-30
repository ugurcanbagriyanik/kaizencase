using KaizenCase.Models;

namespace KaizenCase.Interfaces;

public interface ICodeGeneratorService
{
    Task<List<string>> GenerateCode(int count);
    Task<string> CheckCode(string code);
    Task<string> GenerateAndCheckAll(int count);
}