using DotaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DotaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        MongoClient client = new MongoClient(Constants.data_base);
        IMongoDatabase database;
        IMongoCollection<TgBotUserInfo> collection;
        public UserProfileController()
        {
            database = client.GetDatabase("DotaTgBot");
            collection = database.GetCollection<TgBotUserInfo>("TgBotUsers");
        }

        [HttpGet("get_user")]
        public IActionResult GetUser(long ChatId,bool temporary)
        {           
            return Ok(collection.Find(s=>s.ChatId==ChatId&&s.Temporary == temporary).ToList());

        }
        [HttpPost("create_user")]
        public IActionResult CreateUser(long ChatId,int DotaId,bool temporary)
        {
            TgBotUserInfo user = new TgBotUserInfo();
            user.ChatId = ChatId;
            user.DotaId = DotaId;
            user.Temporary = temporary;
            collection.InsertOne(user);
            return Ok();

        }
        [HttpDelete("delete_user")]
        public IActionResult DeleteUser( int DotaId,bool temporary)
        {
            var filter = Builders<TgBotUserInfo>.Filter.Eq("DotaId", DotaId)& Builders<TgBotUserInfo>.Filter.Eq("Temporary", temporary);
            collection.DeleteOne(filter);
            return Ok();

        }
    }
}
