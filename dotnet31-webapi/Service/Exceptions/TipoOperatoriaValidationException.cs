using cmes_webapi.Common.Resources;
using cmes_webapi.Common.Services;
using System;

namespace cmes_webapi.Services
{
    public class TipoOperatoriaValidationException : TipoOperatoriaException
    {
        private static string message = LogError.GetErrorDescription(EnumErrorCode.TOP0201.ToString());
        public TipoOperatoriaValidationException(Exception innerException)
            : base(message, innerException)
        { }

        public TipoOperatoriaValidationException()
           : base(message)
        { }
    }
}
