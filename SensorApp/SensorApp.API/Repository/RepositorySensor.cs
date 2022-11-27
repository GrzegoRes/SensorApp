using MongoDB.Driver;
using SensorApp.API.Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SensorApp.API.Repository
{
    public class RepositorySensor : IRepositorySensor
    {
        private readonly IMongoSensorDBContext _mongoContext;
        private readonly IMongoCollection<SensorDB> _dbCollection;

        public RepositorySensor(IMongoSensorDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<SensorDB>(typeof(SensorDB).Name);
        }
        public async Task save(SensorDB sensor)
        {
            await _dbCollection.InsertOneAsync(sensor);
        }

        public async Task<IEnumerable<SensorDB>> findAll()
        {
            var all = await _dbCollection.FindAsync(Builders<SensorDB>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task<IEnumerable<SensorDB>> findAllByName(string name)
        {
            FilterDefinition<SensorDB> filter = Builders<SensorDB>.Filter.Eq("name", name);
            return await _dbCollection.FindAsync(filter).Result.ToListAsync();
        }
    }
}
