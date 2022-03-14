using cmes_webapi.Common.Resources;
using cmes_webapi.Common.Services;
using System;

namespace cmes_webapi.Services
{
    public class TipoOperatoriaException : Exception
    {
        private static string message = LogError.GetErrorDescription(EnumErrorCode.TOP9999.ToString());
        public TipoOperatoriaException(Exception inner) : 
            base(message, inner)
        {}

        public TipoOperatoriaException(string msg, Exception inner) :
           base(msg, inner)
        { }

        public TipoOperatoriaException(string msg) :
          base(msg)
        { }
    }
}
