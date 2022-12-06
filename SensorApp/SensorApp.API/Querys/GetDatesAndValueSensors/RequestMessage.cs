using System;

namespace SensorApp.API.Querys.GetDatesAndValueSensors
{
    public class RequestMessage
    {
        public string Name { get; set; }

        public DateTime? Date_from { get; set; }
        public DateTime? Date_to { get; set; }
    }
}
