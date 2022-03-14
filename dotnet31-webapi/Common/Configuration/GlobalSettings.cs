using System.Globalization;

namespace cmes_webapi.Common.Configuration
{
    public static class GlobalSettings
    {
        public static NumberFormatInfo NumberFormat { get; set; }
        public static string ConnectionString { get; set; }
        public static string ASPNETCORE_ENVIRONMENT { get; set; }
        public static string Server { get; set; }
        public static int Port { get; set; }
        public static int Port2 { get; set; }
        public static int MonitorTimming { get; set; }
        public static int VerifyPendingTimming { get; set; }
        public static string LogFile { get; set; }
        public static string LogFileMonitor { get; set; }
        public static string LoggTrace { get; set; }
        public static bool LoggEnableInDebug { get; set; }
        public static bool LoggEnableInInfo { get; set; }

        public static bool SaveFile { get; set; }
        public static bool ENABLE_TRACE { get; set; }
        public static string Host { get; set; }
        public static string applicationUrl { get; internal set; }
        public static string USERNAME_SQL { get; internal set; }
        public static string PASSWORD_SQL { get; internal set; }
        public static string TEMP_CONNECTIONSTRING { get; internal set; }
        public static string ACDI_TIPOOPERATORIA_URL { get; internal set; }
    }
}
