using cmes_webapi.Api.Dto;
using cmes_webapi.Common.Configuration;
using cmes_webapi.Entities.Services.Helpers;

namespace cmes_webapi.Services
{
    public abstract class ServiceBase
    {

        public static void SaveSettings()
        {
            ReadSettings.SaveSettings(ReadSettings.ImpresionCertificadoPF);
        }

        public virtual ResponseResultDto<T> CreateResponse<T>(T response, bool isNoGetData = false)
        {

            if (response != null)
                return SuccessServiceResponse<T>.NewResponseOK(response);
            else
                return SuccessServiceResponse<T>.NewResponseInfo(response, isNoGetData);
        }


    }
}