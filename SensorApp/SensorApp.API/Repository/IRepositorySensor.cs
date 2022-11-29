using SensorApp.API.Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using DnsClient.Protocol;

namespace SensorApp.API.Repository
{
    public interface IRepositorySensor
    {
        Task<IEnumerable<SensorDB>> findAll();
        Task save(SensorDB sensor);
        Task<IEnumerable<SensorDB>> findAllByName(string name);
    }
}
