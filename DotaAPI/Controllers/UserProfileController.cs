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
        IMongoCollection<TgBotUserInfo> userCollection;
        IMongoCollection<NotificationInfo> notificationCollection;
        public UserProfileController()
        {
            database = client.GetDatabase("DotaTgBot");
            userCollection = database.GetCollection<TgBotUserInfo>("TgBotUsers");
            notificationCollection = database.GetCollection<NotificationInfo>("Notifications");
        }
        [HttpGet("get_notifications")]
        public IActionResult GetNotifications()
        {
            List<NotificationInfo> result = notificationCollection.Find(s=>true).ToList();
            return Ok(result);
                /*.Find(s=>s.ChatId == ChatId))*/
        }
        [HttpPost("post_notification")]
        public IActionResult PostNotification(long ChatId,string date,string title)
        { 
            NotificationInfo notificationInfo = new NotificationInfo();
            notificationInfo.ChatId = ChatId;
            notificationInfo.date = date;
            notificationInfo.title = title;
            notificationCollection.InsertOne(notificationInfo);
            return Ok();
        }
        [HttpDelete("delete_notification")]
        public IActionResult DeleteNotification(long ChatId)
        {
            var filter = Builders<NotificationInfo>.Filter.Eq("ChatId", ChatId);
            notificationCollection.DeleteOne(filter);
            return Ok();
        }
        [HttpGet("get_user")]
        public IActionResult GetUser(long ChatId,bool temporary)
        {           
            return Ok(userCollection.Find(s=>s.ChatId==ChatId&&s.Temporary == temporary).ToList());

        }
        [HttpPost("create_user")]
        public IActionResult CreateUser(long ChatId,int DotaId,bool temporary)
        {
            TgBotUserInfo user = new TgBotUserInfo();
            user.ChatId = ChatId;
            user.DotaId = DotaId;
            user.Temporary = temporary;
            userCollection.InsertOne(user);
            return Ok();

        }
        [HttpDelete("delete_user")]
        public IActionResult DeleteUser( int DotaId,bool temporary)
        {
            var filter = Builders<TgBotUserInfo>.Filter.Eq("DotaId", DotaId)& Builders<TgBotUserInfo>.Filter.Eq("Temporary", temporary);
            userCollection.DeleteOne(filter);
            return Ok();

        }
    }
}
