using MongoDB.Driver;

namespace SensorApp.API.Repository
{
    public interface IMongoSensorDBContext
    {
        IMongoCollection<T> GetCollection<T>(string tabele);
    }
}
