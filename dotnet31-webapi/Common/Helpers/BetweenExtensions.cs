using System;

namespace cmes_webapi.Common.Helpers
{
    public static class BetweenExtensions
    {
        /// <summary>
        /// Verifica si el decimal se encuentra entre dos valores incluido el igual
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsBetweenDecimal(this decimal value, decimal min, decimal max)
        {
            return (value >= min && value <= max);
        }

        /// <summary>
        /// Verifica si la fecha y hora actual se encuentra entre dos fechas incluido el igual
        /// </summary>
        /// <param name="datetimeNow"></param>
        /// <param name="openMarket"></param>
        /// <param name="closeMarket"></param>
        /// <returns></returns>
        public static bool IsBetweenDate(this DateTime datetimeNow, DateTime openMarket, DateTime closeMarket)
        {
            return datetimeNow >= openMarket && datetimeNow <= closeMarket;
        }
    }
}
