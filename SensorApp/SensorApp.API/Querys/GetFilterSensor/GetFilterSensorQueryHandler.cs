using System;
using System.Collections;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SensorApp.API.Querys.GetAllSensors;
using SensorApp.API.Querys.GetAllSensors.GetFilterSensor;
using SensorApp.API.Repository.Entity;

namespace SensorApp.API.Querys.GetFilterSensor
{
    public class GetFilterSensorQueryHandler: IRequestHandler<GetFilterSensorQuery, IEnumerable<SensorWithSplitDate>>
    {
        private readonly IMediator _mediator;

        public GetFilterSensorQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<IEnumerable<SensorWithSplitDate>> Handle(GetFilterSensorQuery request, CancellationToken cancellationToken)
        {
            var sensors = _mediator.Send(new GetAllSensorQuery())
                .Result.OrderByDescending(c => c.DateGenerate)
                .ThenByDescending(c=> c.TimeGenerate)
                .Where(c => 1 == 1);

            if (request.Name != null){
                sensors = sensors.Where(c => c.Name == request.Name);
            }

            if (request.Value_from != null)
            {
                sensors = sensors.Where(c => c.Value > request.Value_from);
            }

            if (request.Value_to != null)
            {
                sensors = sensors.Where(c => c.Value < request.Value_to);
            }

            if (request.Type != null)
            {
                sensors = sensors.Where(c => c.Type == request.Type);
            }

            if (request.Date_from != null)
            {
                sensors = sensors.Where(c => DateTime.Parse(c.DateGenerate) > request.Date_from);
            }

            if (request.Date_to != null)
            {
                sensors = sensors.Where(c => DateTime.Parse(c.DateGenerate) < request.Date_to);
            }

            switch (request.SortBy)
            {
                case "Name":
                    sensors = sensors.OrderByDescending(c => c.Name);
                    break;
                case "Value":
                    sensors = sensors.OrderByDescending(c => c.Value);
                    break;
                case "Type":
                    sensors = sensors.OrderByDescending(c => c.Type);
                    break;
                default:
                    sensors = sensors.OrderByDescending(c => c.DateGenerate)
                        .ThenByDescending(c => c.TimeGenerate);
                    break;
            }

            if (request.TypSort == "ascending")
            {
                sensors = sensors.Reverse();
            }

            return Task.FromResult(sensors.Take(50));
        }
    }
}
