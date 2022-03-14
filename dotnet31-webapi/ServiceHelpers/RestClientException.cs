using cmes_webapi.Common.Resources;
using cmes_webapi.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cmes_webapi.ServiceHelpers
{
    public class RestClientException : Exception
    {
        private static string message = LogError.GetErrorDescription(EnumErrorCode.TOP0300.ToString());
        public RestClientException(Exception innerException)
            : base(message, innerException)
        { }

        public RestClientException()
           : base(message)
        { }
    }
}
