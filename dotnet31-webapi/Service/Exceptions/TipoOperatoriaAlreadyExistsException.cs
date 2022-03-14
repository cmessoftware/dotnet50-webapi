using cmes_webapi.Common.Resources;
using cmes_webapi.Common.Services;
using System;

namespace cmes_webapi.Services
{
    public class TipoOperatoriaAlreadyExistsException : TipoOperatoriaException
    {

        private static string message = LogError.GetErrorDescription(EnumErrorCode.TOP0208.ToString());

        public TipoOperatoriaAlreadyExistsException()
          : base(message)
        { }

        public TipoOperatoriaAlreadyExistsException(Exception innerException)
            : base(message, innerException)
        { }
    }
}
