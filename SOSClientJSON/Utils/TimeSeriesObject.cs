using System;
using System.Collections.Generic;

namespace SOSClientJSON.Utils
{
    public class TimeSeriesObject
    {
        public string Name { get; set; }
        public decimal[] Coordinates { get; set; }
        public Dictionary<string, decimal> TimeSeries { get; set; }

        public override string ToString()
        {
            return Name + "; (" + Coordinates[0] + ", " + Coordinates[1] + "); Time Series: " + TimeSeries.Count + " values";
        }
    }
}