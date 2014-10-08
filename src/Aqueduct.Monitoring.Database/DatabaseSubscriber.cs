using Aqueduct.Monitoring.Readings;
using Common.Logging;
using System;
using System.Collections.Generic;
using PetaPoco;

namespace Aqueduct.Monitoring.Database
{
    public class DatabaseSubscriber
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(DatabaseSubscriber).Name);
        private readonly string _connectionStringName;

        static DatabaseSubscriber()
        {
        }

        public DatabaseSubscriber(string connectionStringName = "")
        {
            _connectionStringName = string.IsNullOrEmpty(connectionStringName) ? "Aqueduct.Monitoring" : connectionStringName;
        }

        public void Subscribe()
        {
            ReadingPublisher.Subscribe(new ReadingSubscriber("Database", ProcessStats));
        }

        private void ProcessStats(IList<FeatureStatistics> stats)
        {
            try
            {
                Logger.Debug("Adding Stats to database");
                using (var database = new PetaPoco.Database(_connectionStringName))
                {
                    using (ITransaction transaction = database.GetTransaction())
                    {
                        foreach (var stat in stats)
                        {

                            foreach (var reading in stat.Readings)
                            {
                                var data = new FeatureData()
                                {
                                    Reading = reading.Name,
                                    Timestamp = DateTime.Now,
                                    Name = stat.Name,
                                    Group = stat.Group
                                };
                                data.ValueType = reading.GetType().Name;
                                SetValue(data, reading);
                                database.Insert("FeatureData", "Id", true, data);
                            }
                        }
                        transaction.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error saving stats to the database", ex);
            }
        }

        private static void SetValue(FeatureData data, ReadingData reading)
        {
            dynamic value = reading.GetValue();

            if (value is double)
            {
                data.DoubleValue = value;
            }
            else if (value is int)
            {
                data.IntValue = value;
            }
            else if (value != null)
            {
                data.Value = value.ToString();
            }
        }
    }
}
