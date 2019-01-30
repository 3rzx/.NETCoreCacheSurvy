namespace CacheSurvy.Models
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class MongoDBCache
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("DestinationId")]
        public string DestinationId { get; set; }

        [BsonElement("Url")]
        public string Url { get; set; }

        [BsonElement("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }
    }
}
