using System;
using PetaPoco;

namespace Aqueduct.Monitoring.Database
{
    [TableName("FeatureData")]
    public class FeatureData
    {
        public string Reading { get; set; }

        public string Name { get; set; }

        public string Group { get; set; }

        public int? IntValue { get; set; }

        public double? DoubleValue { get; set; }

        public string Value { get; set; }

        public string ValueType { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
