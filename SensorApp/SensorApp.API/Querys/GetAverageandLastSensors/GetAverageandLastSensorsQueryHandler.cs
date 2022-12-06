using MediatR;
using SensorApp.API.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace SensorApp.API.Querys.GetAverageandLastSensors
{
    public class
        GetAverageandLastSensorsQueryHandler : IRequestHandler<GetAverageandLastSensorsQuery,
            IEnumerable<SensorLastAndAvergeDTO>>
    {
        private IRepositorySensor _sensorRepository;

        public GetAverageandLastSensorsQueryHandler(IRepositorySensor repositorySensor)
        {
            this._sensorRepository = repositorySensor;
        }

        public Task<IEnumerable<SensorLastAndAvergeDTO>> Handle(GetAverageandLastSensorsQuery request,
            CancellationToken cancellationToken)
        {
            var sensors = _sensorRepository.findAll();

            var res = sensors.Result.GroupBy(x => (x.name, x.type))
                .OrderBy(x => x.Key.name)
                .Select(d => new SensorLastAndAvergeDTO
                {
                    Name = d.Key.name,
                    Type = d.Key.type,
                    AveValueLastHundred = d.TakeLast(100).Average(v => v.value),
                    LastValue = d.Last().value
                });
            return Task.FromResult(res);

        }
    }
}
