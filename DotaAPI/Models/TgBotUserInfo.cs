using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DotaAPI.Models
{
    [BsonIgnoreExtraElements]
    public class TgBotUserInfo
    {   public ObjectId Id { get; set; }
        public int DotaId { get; set; }
        public long ChatId { get; set; }
        public bool Temporary { get; set; }


    }
}
