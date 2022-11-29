using System;
using Microsoft.VisualBasic;

namespace SensorApp.API
{
    public class Message
    {
        public string? Name { get; set; }
        public int? Value_from { get; set; }
        public int? Value_to { get; set; }
        public string? Type { get; set; }
        public string? SortBy { get; set; }

        public string? TypSort { get; set; }

        public DateTime? Date_from { get; set; }
        public DateTime? Date_to { get; set; }
    }
}
