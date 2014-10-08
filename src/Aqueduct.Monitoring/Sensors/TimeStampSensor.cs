using System;
using Aqueduct.Monitoring.Readings;

namespace Aqueduct.Monitoring.Sensors
{
    public class TimeStampSensor : SensorBase
    {
        public TimeStampSensor(string readingName) : base(readingName)
        {
        }

        public void TimeStamp(DateTime? dateTime)
        {
            AddReading(new DateTimeReading(dateTime.GetValueOrDefault(DateTime.UtcNow)));
        }
    }
}