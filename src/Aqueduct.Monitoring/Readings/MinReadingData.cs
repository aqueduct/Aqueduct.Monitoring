using System;

namespace Aqueduct.Monitoring.Readings
{
	public class MinReadingData : ReadingData
	{
		public MinReadingData(double value)
		{
			Value = value;
		}

		public double Value { get; set; }

		public override dynamic GetValue()
		{
			return Value;
		}

		public override void Aggregate(ReadingData other)
		{
			Value = Math.Min(Value, (double)other.GetValue());
		}

	    public override bool CanAggregate
	    {
	        get { throw new NotImplementedException(); }
	    }
	}
}