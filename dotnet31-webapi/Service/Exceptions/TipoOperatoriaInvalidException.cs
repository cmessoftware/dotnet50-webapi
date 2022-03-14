using System;

namespace cmes_webapi.Services
{
    public class TipoOperatoriaInvalidException : TipoOperatoriaException
    {
        public TipoOperatoriaInvalidException(string parameterName, object parameterValue)
            : base($"Invalid PFImpresion Certif error occurred. parameter name: {parameterName} " +
                 $"parameter value: {parameterValue}")
        { }
    }

}
