using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.Services
{
    public interface IOpenAiService
    {
        Task<List<string>> GetPromptSuggestionsAsync(string topic, string description);
    }
}
