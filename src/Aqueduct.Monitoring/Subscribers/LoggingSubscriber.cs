﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;

namespace Aqueduct.Monitoring.Subscribers
{
    public static class LoggingSubscriber
    {
        private static readonly ILog Logger = LogManager.GetLogger("Aqueduct.Monitoring.Subscripbers.LoggingSubscriber");
        public static void Subscribe()
        {
            ReadingPublisher.Subscribe(new ReadingSubscriber(typeof(LoggingSubscriber).Name, ProcessStats));
        }

        private static void ProcessStats(IList<FeatureStatistics> stats)
        {
            foreach (var featureStat in stats)
            {
                Logger.Info(String.Format("------------------- Feature {0} in {1} at {2} ----------------------", featureStat.Name, featureStat.Group, featureStat.Timestamp)); 
                foreach (var reading in featureStat.Readings)
                {
                	Logger.Info(String.Format("ReadingName: {0}; Value: {1}", reading.Name, reading.GetValue()));
                }
                Logger.Info("---------------------------------------------------");
            }
        }
    }
}
