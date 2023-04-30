using KaizenCase.Models;

namespace KaizenCase.Interfaces;

public interface IJsonService
{
    Task<List<string>> ParseJsonToLines(List<ValueField> json);
}