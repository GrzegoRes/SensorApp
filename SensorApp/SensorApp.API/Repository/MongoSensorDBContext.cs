using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace SensorApp.API.Repository
{
    public class MongoSensorDBContext : IMongoSensorDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSession Session { get; set; }

        public MongoSensorDBContext(IOptions<Settings> options)
        {
            _mongoClient = new MongoClient(options.Value.ConnectionString);
            _db = _mongoClient.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<T> GetCollection<T>(string tabele)
        {
            return _db.GetCollection<T>(tabele);
        }
    }
}