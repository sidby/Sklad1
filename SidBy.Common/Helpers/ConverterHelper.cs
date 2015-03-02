using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Common.Helpers
{
    public static class ConverterHelper
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Convert Datetime value to unix timestamp
        /// </summary>
        /// <param name="value">Datetime value kind of Utc (as Epoch)</param>
        /// <returns></returns>
        private static long ConvertToUtcTimestamp(DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long) elapsedTime.TotalSeconds;
        }

        public static long ConvertToTimeStamp(DateTime value)
        {
            return ConvertToUtcTimestamp(
                new DateTime(value.Year, value.Month, value.Day,value.Hour, 
                    value.Minute, value.Second, DateTimeKind.Utc)
                );
        }
    }
}
