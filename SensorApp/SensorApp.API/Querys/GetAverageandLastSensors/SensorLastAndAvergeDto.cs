namespace SensorApp.API.Querys.GetAverageandLastSensors
{
    public class SensorLastAndAvergeDTO
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public double AveValueLastHundred { get; set; }
        public int LastValue { get; set; }
    }
}
