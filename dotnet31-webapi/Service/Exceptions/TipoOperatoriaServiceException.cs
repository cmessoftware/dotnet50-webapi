using System;

namespace cmes_webapi.Services
{
    public class TipoOperatoriaServiceException : TipoOperatoriaException
    {
        public TipoOperatoriaServiceException(Exception innerException)
            : base("System error occurred, contact support.", innerException)
        {

        }
    }
}
