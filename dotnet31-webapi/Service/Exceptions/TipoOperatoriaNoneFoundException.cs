using cmes_webapi.Common.Resources;
using cmes_webapi.Common.Services;
using System;

namespace cmes_webapi.Services
{
    public class TipoOperatoriaNoneFoundException : TipoOperatoriaException
    {

        private static string message = LogError.GetErrorDescription(EnumErrorCode.TOP0204.ToString());
        public TipoOperatoriaNoneFoundException(Exception innerException)
            : base(message, innerException)
        { }

        public TipoOperatoriaNoneFoundException()
           : base(message)
        { }
    }
}
