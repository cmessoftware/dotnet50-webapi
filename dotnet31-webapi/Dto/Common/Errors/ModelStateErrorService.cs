using Microsoft.AspNetCore.Mvc.ModelBinding;
using cmes_webapi.Common.Resources;
using cmes_webapi.Entities.Services.Helpers;
using System.Collections.Generic;

namespace cmes_webapi.Api.Dto
{
    /// <summary>
    /// Crea el response con todos los errores del ModelState.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ModelStateErrorService<T>
    {
        /// <summary>
        /// <para>Recibe un ModelState, arma una lista de ErrorDto con los mensajes de error 
        /// del ModelState y se la envia a ErrorService para retornar ResponseResultDto de 
        /// Error con los mensajes de error del modelo de datos.</para>
        /// </summary>
        public static ResponseResultDto<T> GetErrorListModelState(ModelStateDictionary modelState, T data)
        {
            var externalsErrorList = new List<LogItemDto>();
            foreach (var ms in modelState.Values)
            {
                foreach (ModelError error in ms.Errors)
                {
                    var externalError = new LogItemDto(EnumErrorCode.TOP0001.ToString(), string.IsNullOrEmpty(error.ErrorMessage) ? error.Exception.Message :
                        error.ErrorMessage, SeveridadEnum.ERROR);
                    externalsErrorList.Add(externalError);
                }
            }
            return ErrorService<T>.NewResponseErrorByErrorList(externalsErrorList,
                                                               EnumErrorCode.TOP0001,
                                                               data);
        }
    }
}
