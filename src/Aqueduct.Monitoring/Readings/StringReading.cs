using System;

namespace Aqueduct.Monitoring.Readings
{
    public class StringReading : ReadingData
    {
        public StringReading(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

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