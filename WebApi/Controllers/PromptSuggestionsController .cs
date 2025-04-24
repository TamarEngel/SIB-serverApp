using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using web.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PromptSuggestionsController : ControllerBase
    {
        private readonly OpenAiService _openAiService;

        public PromptSuggestionsController(OpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        [EnableCors("AllowAll")]
        [HttpPost]
        public async Task<IActionResult> GetSuggestions([FromBody] ChallengePromptRequest request)
        {
            var prompts = await _openAiService.GetPromptSuggestionsAsync(request.Topic, request.Description);
            return Ok(new { prompts });
        }
    }

    public class ChallengePromptRequest
    {
        public string Topic { get; set; }
        public string Description { get; set; }
    }
}
