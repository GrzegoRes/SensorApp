using MediatR;
using SensorApp.API.Repository.Entity;
using System.Collections.Generic;

namespace SensorApp.API.Querys.GetAllSensors
{
    public class GetAllSensorQuery
        : IRequest<IEnumerable<SensorDB>>
    {

    }
}
