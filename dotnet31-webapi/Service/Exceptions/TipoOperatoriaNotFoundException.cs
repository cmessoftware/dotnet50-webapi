using cmes_webapi.Common.Resources;
using cmes_webapi.Common.Services;
using System;

namespace cmes_webapi.Services
{
    public class TipoOperatoriaNotFoundException : TipoOperatoriaException
    {

        public TipoOperatoriaNotFoundException(string Comitente, Exception inner)
           : base($"Comitente: {Comitente} - {LogError.GetErrorDescription(EnumErrorCode.TOP0201.ToString())}",
                 inner)
        { }

        public TipoOperatoriaNotFoundException(string Comitente)
           : base($"Comitente: {Comitente} - {LogError.GetErrorDescription(EnumErrorCode.TOP0209.ToString())}")
                 
        { }
    }
}
