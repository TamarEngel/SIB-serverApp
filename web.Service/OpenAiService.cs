using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using web.Core.Services;

namespace web.Service
{
    public class OpenAiService:IOpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenAi:ApiKey"];
        }

        public async Task<List<string>> GetPromptSuggestionsAsync(string topic, string description)
        {
            var prompt = $"האתגר בנושא: {topic}\nתיאור האתגר: {description}\nתן לי רעיונות מקוריים ומתאימים לפרומפטים לתמונה.";

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[] {
                new { role = "user", content = prompt }
            },
                max_tokens = 150,
                temperature = 0.8
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<JsonElement>(json);
            var text = data.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            return text?.Split('\n').Where(line => !string.IsNullOrWhiteSpace(line)).ToList() ?? new List<string>();
        }
    }
}
