using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SensorApp.API.Repository.Entity
{
    public class SensorDB
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        public string name { get; set; }

        public string type { get; set; }
        public string union { get; set; }

        public int value { get; set; }

        public DateTime dateGenerate { get; set; }
    }
}
