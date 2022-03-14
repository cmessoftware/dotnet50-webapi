using cmes_webapi.Api.Dto;
using cmes_webapi.Common;
using cmes_webapi.Common.Resources;
using cmes_webapi.Common.Services;
using System;
using System.Collections.Generic;

namespace cmes_webapi.Entities.Services.Helpers
{
    public static class ErrorService<T>
    {
        #region Instancia de errores a partir de Excepciones.

        public static List<LogItemDto> LogItem  { get; set; }


        /// <summary>
        /// <para>Returns a ResponseResultDto with the Data. If isControlled is true, it is a controlled exception and returns a generic error message in the Result and
        /// the custom error message corresponding to the exception in ErrorLog.
        /// If isControlled is false, it is an Unhandled exception. Returns a generic error message in the Result and
        /// the detail of the exception in ErrorLog.</para>
        /// <para>Retorna un ResponseResultDto con la Data. Si isControlled es true se trata de una excepción controlada y retorna un mensaje de error generico en el Resultado y 
        /// el mensaje de error personalizado correspondiente a la exepción en ErrorLog. 
        /// Si isControlled es false, se trata de una excepción no Controlada. Retorna un mensaje de error generico en el Resultado y 
        /// el detalle de la exepción en ErrorLog.  </para>
        /// </summary>
        public static ResponseResultDto<T> NewResponseErrorByException(Exception ex, bool isControlled, T data)
        {
            if (isControlled)
                return NewResponseError(data, CreateLogResultByException(ex, ex.Message), ValidationsResultError(ex.Message, null));

            return NewResponseError(data, CreateLogResultByException(ex, EnumErrorCode.TOP9999.ToString()),
                ValidationsResultError(EnumErrorCode.TOP9999.ToString(), EnumErrorCode.TOP9999));
        }

        public static ResponseResultDto<T> NewResponseErrorByException(Exception ex, bool isControlled)
        {
            if (isControlled)
                return NewResponseError(CreateLogResultByException(ex, ex.Message), ValidationsResultError(ex.Message, null));

            return NewResponseError(CreateLogResultByException(ex, EnumErrorCode.TOP9999.ToString()),
                ValidationsResultError(EnumErrorCode.TOP9999.ToString(), EnumErrorCode.TOP9999));
        }

        public static ResponseResultDto<T> NewResponseError(BGBAResultadoOperacionLogDto log, BGBAResultadoOperacionDto result)
        {
            ResponseResultDto<T> response = new ResponseResultDto<T>
            {
                //Datos = null,
                BGBAResultadoOperacion = result,
                BGBAResultadoOperacionLog = log
            };
            return response;
        }

        public static BGBAResultadoOperacionLogDto CreateLogResultByException(Exception ex, string errorCode)
        {
            BGBAResultadoOperacionLogDto logItems = new BGBAResultadoOperacionLogDto
            {
                LogItem = new List<LogItemDto>()
            };
            LogItemDto logItem = NewLogItemError(ex.InnerException != null ? ex.InnerException.Message :
                ex.Message, errorCode, SeveridadEnum.INFO);
            logItems.LogItem.Add(logItem);
            return logItems;
        }
        #endregion

        #region Instancia de errores a partir de Lista de ErrorDto.
        /// <summary>
        /// /// <para> Returns an Error ResponseResultDto, in the Response result a generic message corresponding to the EnumErrorCode that receives by parameter and in the ErrorLog responds
        /// the list of ErrorDto you receive by parameter. </para>
        /// <para>Retorna un ResponseResultDto de error, en el Resultado response un mensaje generico correspondiente al EnumErrorCode que recibe por parámetro y en el ErrorLog responde 
        /// la lista de ErrorDto que recibe por parámetro.</para>
        /// </summary>
        public static ResponseResultDto<T> NewResponseErrorByErrorList(List<LogItemDto> externalErrorList, EnumErrorCode errorCode, T data)
        {
            return NewResponseError(data, CreateLogResultByErrorList(externalErrorList), ValidationsResultError(errorCode.ToString(), errorCode));
        }


        /// <summary>
        ///  <para> Returns an Error ResponseResultDto, in the Response result a generic message corresponding to the generalDescription that receives by parameter and in the ErrorLog responds
        /// the list of ErrorDto you receive by parameter. </para>
        /// <para>Retorna un ResponseResultDto de error, en el Resultado response un mensaje generico correspondiente al generalDescription que recibe por parámetro y en el ErrorLog responde 
        /// la lista de ErrorDto que recibe por parámetro.</para>
        /// </summary>
        /// <param name="externalErrorList"></param>
        /// <param name="errorCode"></param>
        /// <param name="generalDescription"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResponseResultDto<T> NewResponseErrorByErrorList(List<LogItemDto> externalErrorList, EnumErrorCode errorCode,
            string generalDescription, T data)
        {
            return NewResponseError(data, CreateLogResultByErrorList(externalErrorList), ValidationsResultError(errorCode.ToString(),
                null, generalDescription));
        }


        /// <summary>
        /// <para>Receive a list of errors of type ErrorDto and build the logItems.</para>
        /// <para>Recibe una lista de errores del tipo ErrorDto y arma el logItems.</para>
        /// </summary>
        public static BGBAResultadoOperacionLogDto CreateLogResultByErrorList(List<LogItemDto> externalErrorList)
        {
            BGBAResultadoOperacionLogDto logItems = new BGBAResultadoOperacionLogDto
            {
                LogItem = new List<LogItemDto>()
            };
            foreach (var error in externalErrorList)
            {
                LogItemDto logItem = NewLogItemError(error.Description, error.Code, error.Severity);
                logItems.LogItem.Add(logItem);
            }
            return logItems;
        }
        #endregion

        #region Métodos comúnes utilizados para instanciar errores.

        public static ResponseResultDto<T> NewResponseError(T data, BGBAResultadoOperacionLogDto log, BGBAResultadoOperacionDto result)
        {
            ResponseResultDto<T> response = new ResponseResultDto<T>
            {
                Datos = data,
                BGBAResultadoOperacionLog = log,
                BGBAResultadoOperacion = result
            };
            return response;
        }

        #region Result.
        /// <summary>
        /// <para>Returns a BGBAOperationResultDto when the Response is Error, modifying its description (general error) depending on whether it is a controlled internal error,
        /// an external services error or an unhandled exception.</para>
        /// <para>Retorna un BGBAOperationResultDto cuando el Response es de Error, modificando la descripcion del mismo (error general) validando si es un error interno 
        /// controlado,  un error de servicios externos o una excepción sin controlar.</para>
        /// </summary>
        public static BGBAResultadoOperacionDto ValidationsResultError(string errorCode, EnumErrorCode? enumErrorCode = null, string description = null)
        {
            if (string.IsNullOrEmpty(description))
                description = ConstantValidations.InternalServicesError;

            if (enumErrorCode != null)
                description = LogError.GetErrorDescription(enumErrorCode.ToString());


            var result = NewBGBAOperationResultError(description, errorCode);
            return result;
        }

        /// <summary>
        /// <para>Instance a BGBAOperationResultDto for when the Response is Error, using the data it receives as a parameter.</para>
        /// <para>Instancia un BGBAOperationResultDto para cuando el Response es de Error, utilizando los datos que recibe como parámetro.</para>
        /// </summary>
        public static BGBAResultadoOperacionDto NewBGBAOperationResultError(string generalDescription, string errorCode)
        {
            return new BGBAResultadoOperacionDto()
            {
                Severidad = SeveridadEnum.ERROR.ToString(),
                Codigo = errorCode,
                Descripcion = generalDescription,
                Tipo = "",
                UrlDetalle = "",
                IdRespuesta = new IdRespuesta()
                {
                    NombreProveedor = "paas_inpo_calypsops_template"
                }
            };
        }
        #endregion

        #region Log.
        /// <summary>
        /// <para>Instance a LogItemDto with the data passed by parameter.</para>
        /// <para>Instancia un LogItemDto con los datos pasados por parametro.</para>
        /// </summary>
        public static LogItemDto NewLogItemError(string errorMessage, string errorCode, SeveridadEnum severity)
        {
            return new LogItemDto
            {
                Description = "Error: " + errorMessage,
                Severity = severity,
                Code = errorCode,
                DetailURL = ""
            };
        }
        #endregion

        #endregion
    }
}
