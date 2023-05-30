using MongoDB.Bson.Serialization.Attributes;

namespace DotaAPI.Models
{
    [BsonIgnoreExtraElements]
    public class NotificationInfo
    {
        public string date { get; set; }
        public long ChatId { get; set; }
        public string title { get; set; }
    }
}
