using System;

namespace cmes_webapi.Services
{
    public class TipoOperatoriaDependencyException : TipoOperatoriaException
    {
        public TipoOperatoriaDependencyException(Exception innerException)
            : base("Service dependency error occurred, contact support", innerException)
        {

        }
    }
}
