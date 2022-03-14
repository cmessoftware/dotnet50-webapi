using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using cmes_webapi.Api.Dto;
using cmes_webapi.Common.Resources;
using cmes_webapi.Common.Services;
using cmes_webapi.Entities.Services.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace cmes_webapi.Services
{
    public partial class TipoOperatoriaService
    {
        private delegate Task<T> returningFunction<T>();
        private delegate Task<IQueryable<T>> returningsFunction<T>();

        private async Task<T> TryCatch<T>(returningFunction<T> returningTipoOperatoriaFunction)
        {
            try
            {
                return await returningTipoOperatoriaFunction();
            }
            catch (Exception ex)
            {
                throw CreateAndLogValidationException<Exception>(ex);
            }

        }

        private Task<IQueryable<T>> TryCatch<T>(returningsFunction<T> returningsFunction)
        {
            try
            {
                return returningsFunction();
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
        }

        //Caso de una excepcion no controlada.
        public ResponseResultDto<T> CallErrorService<T>(Exception ex)
        {
            var response = ErrorService<T>.NewResponseErrorByException(ex, true);
            string message = LogError.GetErrorDescription(EnumErrorCode.TOP9999.ToString());
            _logger.LogError(message);
            return response;
        }

        private Exception CreateAndLogValidationException<E>(Exception ex)
        {
            string msg;
            string message;
            Exception pfEx = null;


            if (ex is TipoOperatoriaNoneFoundException)
            {
                msg = LogError.GetErrorDescription(EnumErrorCode.TOP0204.ToString());
                message = ex.InnerException != null ? $"{msg}: {ex.Message} - {ex.InnerException.Message}"
                                 : $"{msg}: {ex.Message}";
                _logger.LogError(message);
                pfEx = new TipoOperatoriaNoneFoundException(ex);
            }

            if (ex is TipoOperatoriaNotFoundException)
            {
                msg = LogError.GetErrorDescription(EnumErrorCode.TOP0209.ToString());
                message = ex.Message;
                _logger.LogError(message);
                pfEx = new TipoOperatoriaNotFoundException(message, ex);
            }
            if (ex is Exception)
            {
                msg = LogError.GetErrorDescription(EnumErrorCode.TOP9999.ToString());
                message = ex.InnerException != null ? $"{msg}: {ex.Message} - {ex.InnerException.Message}"
                                 : $"{msg}: {ex.Message}";
                _logger.LogError(message);
                pfEx = new Exception(message, ex);
            }


            return pfEx;
        }

        private TipoOperatoriaDependencyException CreateAndLogCriticalDependencyException(Exception exception)
        {
            var TipoOperatoriaDependencyException = new TipoOperatoriaDependencyException(exception);
            _logger.LogCritical(TipoOperatoriaDependencyException.Message);

            return TipoOperatoriaDependencyException;
        }
    }
}
