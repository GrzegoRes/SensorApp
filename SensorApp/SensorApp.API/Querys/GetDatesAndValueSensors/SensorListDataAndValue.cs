using System;
using System.Collections.Generic;

namespace SensorApp.API.Querys.GetDatesAndValueSensors
{
    public class SensorListDataAndValue
    {
        public string Name { get; set; }
        public List<DateTime> DateGeneration { get; set; }
        public List<int> ValueGeneration { get; set; }
    }
}
