using cmes_webapi.Common.Configuration;
using cmes_webapi.Common.Services;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace cmes_webapi.Common.Helpers
{
    /// <summary>
    /// Clase de log
    /// </summary>
    public class FileLoggerServiceAsync : IFileLoggerServiceAsync
    {
        private static object syncObject = new Object();
        private static FileLoggerServiceAsync _default;

        private string process;
        private string name;

        /// <summary>
        /// Logger default de la aplicación
        /// Aplico patron Singleton.
        /// </summary>
        /// 
        public FileLoggerServiceAsync Default {
            get
            {
                if (_default == null)
                    _default = new FileLoggerServiceAsync();

                return _default;
            }
        }

        /// <summary>
        /// Inicializa un nuevo logger default usando nombre de la aplicación
        /// </summary>
        private FileLoggerServiceAsync()
        {

            this.name = string.Empty;
            this.process = LoggerConfig.NombreProceso;
            this.Location = LoggerConfig.Ubicacion;

            WeightControl();

            try
            {
                // Crea carpeta de logs
                if (!Directory.Exists(this.Location))
                    Directory.CreateDirectory(this.Location);
            }
            catch { }
        }

        /// <summary>
        /// Inicializa un nuevo logger
        /// </summary>
        public FileLoggerServiceAsync(string name)
            : this()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("El nombre del logger es obligatorio");

            this.name = name;
        }

        /// <summary>
        /// Devuelve la ruta completa sin el nombre de archivo
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// Devuelve la ruta completa del archivo de log que utiliza este logger
        /// </summary>
        public string LogFile
        {
            get
            {
                return GetLogFiles();
            }
        }

        /// <summary>
        /// Graba evento conteniendo información técnica que ayude a depurar la aplicación
        /// </summary>
        public void Debug(string message)
        {
            if (LoggerConfig.Debug)
                SaveMessage(SeveridadEnum.DEBUG, message);
        }

        /// <summary>
        /// Graba evento conteniendo información técnica que ayude a depurar la aplicación
        /// </summary>
        public void Debug(string format, params object[] args)
        {
            if (LoggerConfig.Debug)
                SaveMessage(SeveridadEnum.DEBUG, GetMessage(format, args));
        }

        /// <summary>
        /// Graba evento informando situaciones normales de la aplicación
        /// </summary>
        public void Info(string message)
        {
            if (LoggerConfig.Info)
                SaveMessage(SeveridadEnum.INFO, message);
        }

        /// <summary>
        /// Graba evento informando situaciones normales de la aplicación
        /// </summary>
        public void Info(string format, params object[] args)
        {
            if (LoggerConfig.Info)
                SaveMessage(SeveridadEnum.INFO, GetMessage(format, args));
        }

        /// <summary>
        /// Graba evento advirtiendo anomalías en un proceso aunque haya finalizado correctamente
        /// (ej. asumir defaults por falta de configuración, o exceso en tiempo de proceso)
        /// </summary>
        public void Warn(string message)
        {
            SaveMessage(SeveridadEnum.AVISO, message);
        }

        /// <summary>
        /// Graba evento advirtiendo anomalías en un proceso aunque haya finalizado correctamente
        /// (ej. asumir defaults por falta de configuración, o exceso en tiempo de proceso)
        /// </summary>
        public void Warn(string format, params object[] args)
        {
            SaveMessage(SeveridadEnum.AVISO, GetMessage(format, args));
        }

        /// <summary>
        /// Graba evento advirtiendo anomalías en un proceso aunque haya finalizado correctamente
        /// (ej. asumir defaults por falta de configuración, o exceso en tiempo de proceso)
        /// </summary>
        public void Warn(Exception ex, string format, params object[] args)
        {
            SaveMessage(SeveridadEnum.AVISO, GetMessage(ex, format, args));
        }

        /// <summary>
        /// Graba evento cuando un proceso individual no pudo completarse pero la aplicación continúa funcionando
        /// (ej. argumentos en un pedido, o error de acceso a la base de datos)
        /// </summary>
        public void Error(string message)
        {
            SaveMessage(SeveridadEnum.ERROR, message);
        }

        /// <summary>
        /// Graba evento cuando un proceso individual no pudo completarse pero la aplicación continúa funcionando
        /// (ej. argumentos en un pedido, o error de acceso a la base de datos)
        /// </summary>
        public void Error(string format, params object[] args)
        {
            SaveMessage(SeveridadEnum.ERROR, GetMessage(format, args));
        }

        /// <summary>
        /// Graba evento cuando un proceso individual no pudo completarse pero la aplicación continúa funcionando
        /// (ej. argumentos en un pedido, o error de acceso a la base de datos)
        /// </summary>
        public void Error(Exception ex, string format, params object[] args)
        {
            SaveMessage(SeveridadEnum.ERROR, GetMessage(ex, format, args), GetErrorMessage(ex, format, args));
        }

        /// <summary>
        /// Graba evento cuando la aplicación completa no puede continuar realizando procesamiento
        /// (ej. error permanente de conexión a base de datos, o falta de configuración necesaria para iniciar)
        /// </summary>
        public void Fatal(string message)
        {
            SaveMessage(SeveridadEnum.FATAL, message);
        }

        /// <summary>
        /// Graba evento cuando la aplicación completa no puede continuar realizando procesamiento
        /// (ej. error permanente de conexión a base de datos, o falta de configuración necesaria para iniciar)
        /// </summary>
        public void Fatal(string format, params object[] args)
        {
            SaveMessage(SeveridadEnum.FATAL, GetMessage(format, args));
        }

        /// <summary>
        /// Graba evento cuando la aplicación completa no puede continuar realizando procesamiento
        /// (ej. error permanente de conexión a base de datos, o falta de configuración necesaria para iniciar)
        /// </summary>
        public void Fatal(Exception ex, string format, params object[] args)
        {
            SaveMessage(SeveridadEnum.FATAL, GetMessage(ex, format, args), GetErrorMessage(ex, format, args));
        }

        /// <summary>
        /// Grabacion interna de un evento
        /// </summary>
        private void SaveMessage(SeveridadEnum severity, string message)
        {
            SaveMessage(severity, message, string.Empty);
        }

        /// <summary>
        /// Grabacion interna de un evento
        /// </summary>
        private void SaveMessage(SeveridadEnum severity, string message, string errorMessage)
        {
            try
            {
                // Crea el evento que se grabará a disco
                string evento = FormatEvent(severity, message);

                lock (syncObject)
                {
                    string file = GetLogFiles();
                    SaveEvent(file, evento);

                    // Graba en archivo de error solo si es error o fatal
                    if (severity == SeveridadEnum.ERROR || severity == SeveridadEnum.FATAL)
                    {
                        file = GetErrorFiles();
                        string eventoError = string.IsNullOrEmpty(errorMessage) ? evento : this.FormatEvent(severity, errorMessage);
                        SaveEvent(file, eventoError);
                    }

                    WeightControl();
                }
            }
            catch { }
        }


        private void WeightControl()
        {
            FileInfo valorPeso = new FileInfo(this.LogFile);

            if (File.Exists(this.LogFile) && (valorPeso.Length > 30000000))
            {
                var extension = Path.GetExtension(this.LogFile);
                var nuevoPath = this.LogFile.Replace(extension, "");
                valorPeso.MoveTo(string.Format("{0}.{1:00}-{2:00}-{3:00}{4}",
                    nuevoPath, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, extension));
            }

        }

        /// <summary>
        /// Realiza append al final del archivo
        /// </summary>
        private void SaveEvent(string archivo, string evento)
        {
            try
            {
                FileStream stream = new FileStream(archivo, FileMode.Append, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    writer.Write(evento);
                }
            }
            catch { }
        }

        /// <summary>
        /// Devuelve la linea de log completa para ser grabada
        /// </summary>
        private string FormatEvent(SeveridadEnum severidad, string mensaje)
        {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss.fff} [{1,-5}] {2,-5} - {3}\r\n",
                DateTime.Now, System.Threading.Thread.CurrentThread.ManagedThreadId, severidad, mensaje);
        }

        /// <summary>
        /// Arma el mensaje para loggear
        /// </summary>
        private string GetMessage(string formato, params object[] args)
        {
            return string.Format(formato, args);
        }

        /// <summary>
        /// Arma el mensaje para loggear
        /// </summary>
        private string GetMessage(Exception ex, string formato, params object[] args)
        {
            return string.Format("{0}\r\n{1}", GetMessage(formato, args), ex.InnerException != null ?
                ex.InnerException.Message : ex.Message);
        }

        /// <summary>
        /// Arma el mensaje de error para loggear
        /// </summary>
        private string GetErrorMessage(Exception ex, string formato, params object[] args)
        {
            return string.Format("{0}\r\n{1}", GetMessage(formato, args), ex);
        }

        /// <summary>
        /// Arma el path al archivo de log
        /// </summary>
        private string GetLogFiles()
        {
            return GetFile("log");
        }

        /// <summary>
        /// Arma el path al archivo de errores
        /// </summary>
        private string GetErrorFiles()
        {
            return GetFile("err");
        }

        /// <summary>
        /// Arma el path a un archivo cualquiera de logs
        /// </summary>
        private string GetFile(string extension)
        {
            DateTime today = DateTime.Now;

            // Verifica si es un logger default de la aplicacion
            if (string.IsNullOrEmpty(name))
                return Path.Combine(Location, string.Format("{0:yyyy-MM-dd}.{1}.{2}", today, this.process, extension));
            else
                return Path.Combine(Location, string.Format("{0:yyyy-MM-dd}.{1}.{2}.{3}", today, this.process, this.name, extension));
        }
    }

    /// <summary>
    /// Nivel de severidad de log
    /// </summary>
    public enum SeveridadEnum
    {
        OK,
        /// <summary>
        /// Informativo se usa para mostrar situaciones de funcionamiento normal
        /// </summary>
        INFO,
        /// <summary>
        /// Adevertencia se usa cuando el proceso pudo completarse aunque hubo ciertas anomalías 
        /// (ej. asumir defaults por falta de configuración, o exceso en tiempo de proceso)
        /// </summary>
        AVISO,
        /// <summary>
        /// Error se usa cuando un proceso individual no pudo completarse pero la aplicación continúa funcionando
        /// (ej. argumentos en un pedido, o error de acceso a la base de datos)
        /// </summary>
        ERROR,
        /// <summary>
        /// Fatal se usa cuando la aplicación no puede continuar procesando requerimientos
        /// (ej. error permanente de conexión a base de datos, o falta de configuración necesaria para iniciar)
        /// </summary>
        FATAL,
        DEBUG
    }


    /// <summary>
    /// Acceso a la configuracion
    /// </summary>
    internal static class LoggerConfig
    {
        /// <summary>
        /// Devuelve si se graban eventos de debug
        /// </summary>
        internal static bool Debug
        {
            get
            {
                return GlobalSettings.LoggEnableInDebug;
            }
        }

        /// <summary>
        /// Devuelve si graba eventos de info
        /// </summary>
        internal static bool Info
        {
            get
            {
                return GlobalSettings.LoggEnableInInfo;
            }
        }

        /// <summary>
        /// Devuelve el nombre del proceso.
        /// Por defecto se obtiene el nombre reflection (solo en aplicaciones desktop)
        /// y se puede modificar con el parámetro "Logger.Proceso" del archivo de configuración
        /// </summary>
        internal static string NombreProceso
        {
            get
            {
                string valor = "OMS-TRACE";

                Assembly entryAssembly = Assembly.GetEntryAssembly();
                if (entryAssembly != null)
                    valor = entryAssembly.GetName().Name;

                return valor;
            }
        }

        /// <summary>
        /// Devuelve la carpeta donde se almacenan los logs
        /// Por defecto se utiliza "D:\Logs"
        /// y se puede modificar con el parámetro "Logger.Ubicacion" del archivo de configuración
        /// </summary>
        internal static string Ubicacion
        {
            get
            {
                return GlobalSettings.LoggTrace;
            }
        }
    }
}
