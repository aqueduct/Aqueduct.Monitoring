using System.Collections.Generic;
using System.Linq;

namespace Aqueduct.Monitoring.Readings
{
	public class AvgReadingData : ReadingData
	{
		public AvgReadingData(double value)
		{
			Values = new List<double>();
			Values.Add(value);
		}

		public IList<double> Values { get; private set; }

		public override dynamic GetValue()
		{
			return Values.Average();
		}

		public override void Aggregate(ReadingData other)
		{
			Values.Add((double)other.GetValue());
		}

	    public override bool CanAggregate
	    {
	        get { return true; }
	    }
	}
}