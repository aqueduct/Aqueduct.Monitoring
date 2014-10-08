using System;
using Aqueduct.Monitoring.Readings;

namespace Aqueduct.Monitoring.Sensors
{
    public class TextSensor : SensorBase
    {
        public TextSensor(string readingName) : base(readingName)
        {
        }

        public void RecordReading(string text)
        {
            AddReading(new StringReading(text));
        }
    }
}