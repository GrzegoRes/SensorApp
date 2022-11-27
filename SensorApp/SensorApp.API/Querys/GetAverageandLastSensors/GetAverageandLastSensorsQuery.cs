using MediatR;
using System.Collections.Generic;

namespace SensorApp.API.Querys.GetAverageandLastSensors
{
    public class GetAverageandLastSensorsQuery
        : IRequest<IEnumerable<SensorLastAndAvergeDTO>>
    {

    }
}
