using System.Collections.Generic;
using MediatR;

namespace SensorApp.API.Querys.GetDatesAndValueSensors
{
    public class GetDatesAndValueSensorsQuery : IRequest<SensorListDataAndValue>
    {
        public RequestMessage mess { get; set; }
    }
}
