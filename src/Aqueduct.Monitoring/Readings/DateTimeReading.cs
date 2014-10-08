using System;

namespace Aqueduct.Monitoring.Readings
{
    public class DateTimeReading : ReadingData
    {
        public DateTimeReading(DateTime value)
        {
            Value = value;
        }

        public DateTime Value { get; set; }

        public override dynamic GetValue()
        {
            return Value;
        }

        public override void Aggregate(ReadingData other)
        {
            throw new NotImplementedException();
        }

        public override bool CanAggregate
        {
            get { return false; }
        }
    }
}