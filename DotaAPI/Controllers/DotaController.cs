using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;
using System.Text;
using Newtonsoft.Json;
using DotaAPI.Clients;

namespace DotaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotaController : ControllerBase
    {
        DotaClient  client = new DotaClient();
        [HttpGet("ChatGPT")]
        public async Task<IActionResult> GetAnswerGPT(string message)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string question = message;
            var openAI = new OpenAIAPI("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");
            CompletionRequest completionRequest = new CompletionRequest
            {
                Prompt = question,
                Model = OpenAI_API.Models.Model.DavinciText,
                MaxTokens = 2048
            };
            var completions = await openAI.Completions.CreateCompletionAsync(completionRequest);
            var firstCompletion = completions.ToString();
            return Ok(firstCompletion);
    }
        [HttpGet("player")]
        public IActionResult GetPlayer(int id)
        {   
            var result = client.GetPlayerASync(id).Result;
            return Ok(JsonConvert.SerializeObject(result));
        }
        [HttpGet("match")]
        public IActionResult GetMatch(long id)
        {
            var result = client.GetMatchASync(id).Result;
            return Ok(JsonConvert.SerializeObject(result));

        }
        [HttpGet("player/matches")]

        public IActionResult GetPlayerMatches(int id)
        {
            var result = client.GetPlayerMatchesAsync(id).Result;
            return Ok(JsonConvert.SerializeObject(result));
        }
        [HttpGet("heroes")]

        public IActionResult GetHeroes()
        {
            var result = client.GetHeroesAsync().Result;
            return Ok(JsonConvert.SerializeObject(result));
        }
        [HttpGet("player/wl")]
        public IActionResult GetPlayerWL(int id)
        {
            var result = client.GetPlayerWLAsync(id).Result;
            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
