using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SensorApp.API.Querys.GetAllSensors;

namespace SensorApp.API.Querys.GetDatesAndValueSensors
{
    public class GetDatesAndValueSensorsQueryHandler : IRequestHandler<GetDatesAndValueSensorsQuery, SensorListDataAndValue>
    {
        private readonly IMediator _mediator;

        public GetDatesAndValueSensorsQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<SensorListDataAndValue> Handle(GetDatesAndValueSensorsQuery request, CancellationToken cancellationToken)
        {
            var sensors = _mediator.Send(new GetAllSensorQuery()).Result
                .OrderBy(x => x.DateGenerate).Where(c => c.Name == request.mess.Name);

            if (request.mess.Date_from != null)
            {
                sensors = sensors.Where(c => c.DateGenerate > request.mess.Date_from);
            }

            if (request.mess.Date_to != null)
            {
                sensors = sensors.Where(c => c.DateGenerate < request.mess.Date_to);
            }

            var sensorsData = sensors.Select(x => x.DateGenerate);
            var sensorsVale = sensors.Select(x => x.Value);
            var resoult = new SensorListDataAndValue();
            if (sensors.Any())
            {
                resoult = new SensorListDataAndValue()
                {
                    Name = sensors.Take(1).Select(x => x.Name).First(),
                    ValueGeneration = sensorsVale.ToList(),
                    DateGeneration = sensorsData.ToList(),
                };
            }

            return Task.FromResult(resoult);

        }
    }
}
