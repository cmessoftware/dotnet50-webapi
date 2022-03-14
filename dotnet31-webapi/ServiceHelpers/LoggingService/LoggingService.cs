using cmes_webapi.Common.Configuration;
using Serilog;
using System;

namespace cmes_webapi.Common.Services
{
    public static class LoggingService
    {
        public static void Save(int level, string message)
        {
            var log = new LoggerConfiguration().WriteTo
                .Async(a => a.RollingFile(GlobalSettings.LogFile, shared: true, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {NewLine} {Message:lj}{NewLine}{Exception} {NewLine} {NewLine}"))
                .CreateLogger();
            if (level == (int)EnumLogType.Information)
                log.Information(message);
            else if (level == (int)EnumLogType.Error)
                log.Fatal(message);
            log.Dispose();
        }

        public static void Save(string message, [System.Runtime.CompilerServices.CallerMemberName] string ProcedureName = "")
        {
            message = "-- PROCESO: " + ProcedureName + Environment.NewLine + message;
            var log = new LoggerConfiguration().WriteTo
                .Async(a => a.RollingFile(GlobalSettings.LogFileMonitor, shared: true, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {NewLine} {Message:lj}{NewLine}{Exception} {NewLine} {NewLine}"))
                .CreateLogger();
            log.Information(message);
            
            log.Dispose();
        }

        /// <summary>
        /// Para registrar mas detalles en el log.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ProcedureName"></param>
        public static void Trace(string message, [System.Runtime.CompilerServices.CallerMemberName] string ProcedureName = "")
        {
            if (GlobalSettings.ENABLE_TRACE)
            {
                //message = "-- PROCESO: " + ProcedureName + Environment.NewLine + message;
                var log = new LoggerConfiguration().WriteTo
                    .Async(a => a.RollingFile(GlobalSettings.LogFileMonitor + "_trace.txt", shared: true, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {NewLine} {Message:lj}{NewLine}{Exception} {NewLine}"))
                    .CreateLogger();
                log.Information(message);
                log.Dispose();
            }
        }
    }
}
