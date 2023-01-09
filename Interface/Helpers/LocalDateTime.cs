using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace Interface.Helpers
{
    public class LocalDateTime
    {
        public static DateTime PegaHoraBrasilia()
        {
            try
            {
                return TimeZoneInfo.ConvertTime(DateTime.UtcNow, TZConvert.GetTimeZoneInfo("America/Sao_Paulo"));
            }
            catch (Exception e)
            {
                var teste = e;
                return DateTime.MinValue;
            }
        }

    }
}
