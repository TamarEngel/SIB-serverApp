using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using web.Core.Services;
using web.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PromptSuggestionsController : ControllerBase
    {
        private readonly HttpClient client = new HttpClient();

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChallengePromptRequest gptRequest)
        {
            try
            {
                var content = $"האתגר בנושא: {gptRequest.Topic}\nתיאור האתגר: {gptRequest.Description}\n תן לי רעיונות מקוריים ומתאימים לפרומפטים לתמונה ";
                var prompt = new
                {
                    model = "gpt-4o-mini",
                    messages = new[] {
                    new { role = "user", content = content }
            }
                };

                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
                {
                    Content = JsonContent.Create(prompt)
                };
                request.Headers.Add("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAI_API_KEY")}");

                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"שגיאה ב-OpenAI. סטטוס: {response.StatusCode}. תשובה: {responseContent}");
                }

                var responseJson = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(responseJson);
                var contentText = jsonDoc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                // מניח שה-GPT מחזיר טקסט כמו: "1. משהו\n2. משהו\n3. ..." – נפרק לשורות
                var suggestions = contentText
                    .Split('\n')
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => line.Trim().TrimStart('-', '*', '•').Trim())
                    .ToList();

                return Ok(new { prompts = suggestions });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה: {ex.Message}");
                return StatusCode(500, "שגיאה כלשהי במהלך הפעולה.");
            }
        }

    }

    public class ChallengePromptRequest
    {
        public string Topic { get; set; }
        public string Description { get; set; }
    }
}
