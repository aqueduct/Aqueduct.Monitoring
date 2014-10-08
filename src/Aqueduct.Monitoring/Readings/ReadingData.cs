namespace Aqueduct.Monitoring.Readings
{
	public abstract class ReadingData
	{
		public string Name { get; set; }
		public abstract dynamic GetValue();

	    public virtual void Aggregate(ReadingData other)
	    {
	    }

        public abstract bool CanAggregate { get; }

	}
}