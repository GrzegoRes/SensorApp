using System;

namespace SensorApp.API.Querys.GetAllSensors
{
    public class SensorWithSplitDate
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public int Value { get; set; }
        public string Unit { get; set; }

        public DateTime DateGenerate { get; set; }
    }
}
