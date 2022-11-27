using MediatR;
using SensorApp.API.Repository;
using SensorApp.API.Repository.Entity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SensorApp.API.Querys.GetAllSensors
{
    public class GetAllSensorQueryHandler : IRequestHandler<GetAllSensorQuery, IEnumerable<SensorDB>>
    {
        private readonly IRepositorySensor _sensorRepository;

        public GetAllSensorQueryHandler(IRepositorySensor repositorySensor)
        {
            this._sensorRepository = repositorySensor;
        }

        public Task<IEnumerable<SensorDB>> Handle(GetAllSensorQuery request, CancellationToken cancellationToken)
        {
            var sensors = _sensorRepository.findAll();

            return sensors;
        }
    }
}

