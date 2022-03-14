using System;

namespace cmes_webapi.Common.Helpers
{
    public interface IFileLoggerServiceAsync
    {
        public void Debug(string mensaje);
        public void Debug(string formato, params object[] args);
        public void Info(string mensaje);
        public void Info(string formato, params object[] args);
        public void Warn(string mensaje);
        public void Warn(string formato, params object[] args);
        public void Warn(Exception ex, string formato, params object[] args);
        public void Error(string mensaje);
        public void Error(string formato, params object[] args);
        public void Error(Exception ex, string formato, params object[] args);
        public void Fatal(string mensaje);
        public void Fatal(string formato, params object[] args);
        public void Fatal(Exception ex, string formato, params object[] args);


    }
}