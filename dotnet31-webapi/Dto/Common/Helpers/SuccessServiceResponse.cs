using cmes_webapi.Api.Dto;
using cmes_webapi.Common.Resources;
using cmes_webapi.Common.Services;
using System.Collections.Generic;
using System.Linq;

namespace cmes_webapi.Entities.Services.Helpers
{
    public static class SuccessServiceResponse<T>
    {
        /// <summary>
        /// <para>Create the successful PS response, with the data and logs passed as a parameter.</para>
        /// Crea la estructura de response de PS de éxito completa y toma como data y log de info pasado por 
        /// parametro.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="logItem"></param>
        /// <returns></returns>
        public static ResponseResultDto<T> NewResponseOK(T data, List<LogItemDto> logItem = null)
        {
            // Si tiene logITem se arma response de INFO
            if (logItem != null && logItem.Any() && logItem.FirstOrDefault() != null)
                return NewResponseInfo(data, false, logItem);

            // Si no tiene logItem se arma response de OK
            ResponseResultDto<T> response = new ResponseResultDto<T>
            {
                Datos = data,
                BGBAResultadoOperacion = NewBGBAOperationResultOK(),
                BGBAResultadoOperacionLog = null
            };

            return response;
        }

        public static ResponseResultDto<T> NewResponseInfo(bool isNoDataWasObtained, List<LogItemDto> logItem = null)
        {
            ResponseResultDto<T> response = new ResponseResultDto<T>
            {
                BGBAResultadoOperacion = isNoDataWasObtained ? NewBGBAOperationResultNoDataWasObtained() : NewBGBAOperationResultInfo(),
                BGBAResultadoOperacionLog = DataService.CreateLogResultByErrorList(logItem)
            };

            return response;
        }
       

        private static BGBAResultadoOperacionDto NewBGBAOperationResultOK()
        {
            return NewBGBAOperationResult(EnumErrorCode.TOP0200, SeveridadEnum.OK);
        }

        #region Info 

        /// <summary>
        /// <para>Returns a ResponseResultDto of type info. If isNoDataWasObtained is True, it puts as a general message that no data was obtained for
        /// the query made.</para>
        /// Retorna un ResponseResultDto del tipo info. Si isNoDataWasObtained es True, coloca como mensaje general que no se obtuvieron datos para
        /// la consulta realizada.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isNoGetData"></param>
        /// <param name="logItem"></param>
        /// <returns></returns>
        public static ResponseResultDto<T> NewResponseInfo(T data, bool isNoGetData, List<LogItemDto> logItem = null)
        {
            ResponseResultDto<T> response = new ResponseResultDto<T>
            {
                Datos = data,
                BGBAResultadoOperacion = isNoGetData ? NewBGBAOperationResultNoDataWasObtained() : NewBGBAOperationResultInfo(),
                BGBAResultadoOperacionLog = DataService.CreateLogResultByErrorList(logItem)
            };

            return response;
        }


        private static BGBAResultadoOperacionDto NewBGBAOperationResultNoDataWasObtained()
        {
            return NewBGBAOperationResult(EnumErrorCode.TOP0204, SeveridadEnum.INFO);
        }

        private static BGBAResultadoOperacionDto NewBGBAOperationResultInfo()
        {
            return NewBGBAOperationResult(EnumErrorCode.TOP0201, SeveridadEnum.INFO);
        }

        private static BGBAResultadoOperacionDto NewBGBAOperationResult(EnumErrorCode enumErrorCode, SeveridadEnum severity)
        {
            BGBAResultadoOperacionDto bGBAOperationResult = new BGBAResultadoOperacionDto
            {
                Severidad = severity.ToString(),
                Codigo = enumErrorCode.ToString(),
                Descripcion = LogError.GetErrorDescription(enumErrorCode.ToString()),
                Tipo = "",
                UrlDetalle = "",
                IdRespuesta = new IdRespuesta()
                {
                   NombreProveedor =  "paas_inpo_calypsops_template"
                }
            };
            return bGBAOperationResult;
        }
        #endregion
    }
}
