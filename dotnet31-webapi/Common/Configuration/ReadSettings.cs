using Microsoft.Extensions.Configuration;
using cmes_webapi.Common.Services;
using System;
using System.IO;
using System.Net;

namespace cmes_webapi.Common.Configuration
{


    public static class ReadSettings
    {
        public const string PORT = "HOST";
        public const string Server = "SERVER";
        public const string ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";
        public const string APPLICATIONFILE = "APPLICATIONFILE";
        public const string ImpresionCertificadoPF = "ImpresionCertificadoPF";
        public const string MonitorTimming = "MONITORTIMMING";
        public const string SaveFile = "SAVEFILE";
        public const string ENABLE_TRACE = "ENABLE_TRACE";
        public const string Host = "HOST";
        public const string LogFile = "LOGFILE";
        public const string LogFileMonitor = "LOGFILEMONITOR";
        public const string LoggEnableInDebug = "LOGG_ENABLE_IN_DEBUG";
        public const string LoggEnableInInfo = "LOGG_ENABLE_IN_INFO";
        public const string LoggTrace = "LOGG_TRACE";
        public const string MACHINE_IP = "MACHINE_IP";
        public const string AVAILABLE_SERVERS = "AVAILABLE_SERVERS";
        public const string applicationUrl = "applicationUrl";
        public const string BD_SERVERNAME = "BD_SERVERNAME";
        public const string BD_INSTANCENAME = "BD_INSTANCENAME";
        public const string USERNAME_SQL = "USERNAME_SQL";
        public const string PASSWORD_SQL = "PASSWORD_SQL";
        public const string BD_NAME = "BD_NAME";
        public const string TEMP_CONNECTIONSTRING = "TEMP_CONNECTIONSTRING";
        public static string LEGAJO = "LEGAJO";
        public static string ACDI_TIPOOPERATORIA_URL = "ACDI_TIPOOPERATORIA_URL";

        public static IConfiguration Configuration { get; set; }
       

        public static string ReadConfigKey(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }

        public static string ReadLogFile(string key)
        {
            var file = System.Environment.GetEnvironmentVariable(key);
            return Directory.GetCurrentDirectory() + file;
        }

        public static bool ReadBoolean(string key)
        {
            var value = System.Environment.GetEnvironmentVariable(key);
            int.TryParse(value, out int intValue);
            return Convert.ToBoolean(intValue);
        }
        public static string ReadDBConnection(string userName, string password, string temp_connectionString)
        {
               var connectionString = string.Format(temp_connectionString,
                                                    userName,
                                                    password);

            return connectionString;
        }


        public static void SaveSettings(string service)
        {
            GlobalSettings.LogFile = ReadLogFile(LogFile);
            GlobalSettings.LogFileMonitor = ReadLogFile(LogFileMonitor);
            GlobalSettings.SaveFile = Convert.ToBoolean(Convert.ToInt16(ReadConfigKey(SaveFile)));
            GlobalSettings.ENABLE_TRACE = Convert.ToBoolean(Convert.ToInt32(ReadConfigKey(ENABLE_TRACE)));
            GlobalSettings.USERNAME_SQL = ReadConfigKey(USERNAME_SQL);
            GlobalSettings.PASSWORD_SQL = ReadConfigKey(PASSWORD_SQL);
          
            GlobalSettings.ASPNETCORE_ENVIRONMENT = ReadConfigKey(ASPNETCORE_ENVIRONMENT);
            GlobalSettings.ConnectionString = ReadDBConnection(ReadConfigKey(USERNAME_SQL),
                                                               ReadConfigKey(PASSWORD_SQL),
                                                               ReadConfigKey(TEMP_CONNECTIONSTRING));
            // Set FormatNumber
            GlobalSettings.NumberFormat = new System.Globalization.NumberFormatInfo();
            GlobalSettings.NumberFormat.NumberDecimalSeparator = ".";

            GlobalSettings.Server = ReadConfigKey(Server);
            GlobalSettings.Host = ReadConfigKey(Host);
            GlobalSettings.applicationUrl = ReadConfigKey(applicationUrl);


            GlobalSettings.LoggEnableInDebug = ReadBoolean(LoggEnableInDebug);
            GlobalSettings.LoggEnableInInfo = ReadBoolean(LoggEnableInInfo);
            GlobalSettings.LoggTrace = ReadLogFile(LoggTrace);

            //Set Integration parameters
            GlobalSettings.ACDI_TIPOOPERATORIA_URL = ReadConfigKey(ACDI_TIPOOPERATORIA_URL);

    }

    public static string GetIPAddress()
        {
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            string IPAddress = string.Empty;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                    //      break;
                }
            }
            return IPAddress;
        }

    }
}
