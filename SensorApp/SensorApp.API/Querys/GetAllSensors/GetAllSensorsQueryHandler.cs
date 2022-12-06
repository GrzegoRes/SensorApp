using MediatR;
using SensorApp.API.Repository;
using SensorApp.API.Repository.Entity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SensorApp.API.Querys.GetAllSensors
{
    public class GetAllSensorQueryHandler : IRequestHandler<GetAllSensorQuery, IEnumerable<SensorWithSplitDate>>
    {
        private readonly IRepositorySensor _sensorRepository;

        public GetAllSensorQueryHandler(IRepositorySensor repositorySensor)
        {
            this._sensorRepository = repositorySensor;
        }

        public Task<IEnumerable<SensorWithSplitDate>> Handle(GetAllSensorQuery request, CancellationToken cancellationToken)
        {
            var sensors = _sensorRepository.findAll().Result
                .Select(ele => new SensorWithSplitDate()
                {
                    Name = ele.name,
                    Type = ele.type,
                    Value = ele.value,
                    Unit =ele.union,
                    DateGenerate = ele.dateGenerate,
                });


            return Task.FromResult(sensors);
        }

        public string getJM(string nameType)
        {
            switch (nameType)
            {
                case "temperature":
                    return "C";
                case "light":
                    return "nm";
                case "humidity":
                    return "mn";
                case "other":
                    return "none";
                default:
                    break;
            }

            return null;
        }
    }
}

