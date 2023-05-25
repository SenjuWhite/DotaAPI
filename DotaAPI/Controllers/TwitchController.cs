using DotaAPI.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitchController : ControllerBase
    {
        TwitchClient client = new TwitchClient();
        [HttpGet("twitch")]
        public IActionResult GetTwitch(string broadcastId)
        {
            var result =  client.GetChannelStreamScheduleAsync(broadcastId).Result;
            return Ok(result);
        }
    
    }
}
