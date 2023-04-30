using KaizenCase.Interfaces;
using KaizenCase.Models;

namespace KaizenCase.Services
{

    public class JsonService : IJsonService
    {
        
        private readonly ILogger<JsonService> _logger;
        
        public JsonService(ILogger<JsonService> logger)
        {
            _logger = logger;
        }
        
        
        public async Task<List<string>> ParseJsonToLines(List<ValueField> json)
        {
            var response = new List<string>();
            try
            {
                json.Remove(json[0]);
                json = json.OrderBy(l => (int)Math.Round(l.BoundingPoly.Vertices[0].Y/10.0)).ThenBy(l => l.BoundingPoly.Vertices[0].X)
                    .ToList();

                int index=0;
                int lastY = 0;
                string tempLine = "";
                while (index!=json.Count)
                {
                    if (Math.Abs(lastY-json[index].BoundingPoly.Vertices[0].Y)>10)
                    {
                        response.Add(tempLine);
                        tempLine = json[index].Description??"";
                    }
                    else
                    {
                        tempLine += " "+json[index].Description??"";
                    }

                    lastY = json[index].BoundingPoly.Vertices[0].Y;
                    index++;

                }
                response.Add(tempLine);
            }
            catch (Exception)
            {
                response.Add("Error!");
            }
            return response;
        }
        
    }
}