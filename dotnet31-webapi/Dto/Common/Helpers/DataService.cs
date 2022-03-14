using cmes_webapi.Api.Dto;
using System.Collections.Generic;
using System.Linq;

namespace cmes_webapi.Entities.Services.Helpers
{
    public static class DataService
    {

        public static string NombreProveedor { get; set; }
        public static List<LogItemDto> LogItem { get; set; }


        //public static IdRespuesta NewResponseId()
        //{
        //    return new IdRespuesta
        //    {
        //        NombreProveedor = ConstantValidations.NombreProveedor
        //    };
        //}


        #region logItems
        /// <summary>
        /// <para>Receive a list of errors of type ErrorDto and build the logItems.</para>
        /// <para>Recibe una lista de errores del tipo ErrorDto y arma el logItems.</para>
        /// </summary>
        public static BGBAResultadoOperacionLogDto CreateLogResultByErrorList(List<LogItemDto> externalErrorList)
        {
            if (externalErrorList != null && externalErrorList.Any())
            {
                BGBAResultadoOperacionLogDto logItems = new BGBAResultadoOperacionLogDto
                {
                    LogItem = externalErrorList
                };
                return logItems;
            }
            return null;
        }
        #endregion
    }
}
