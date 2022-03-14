using cmes_webapi.Api.Dto;
using cmes_webapi.ServiceRepository.OMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cmes_webapi.Mappers
{
    public class TipoOperatoriaMapper
    {
        public static TipoOperatoriaInput Map(TipoOperatoriaRequestDto data)
        {

            TipoOperatoriaInput ti = new TipoOperatoriaInput()
            {
                BrokerID = data.Datos.AgenteColocador
            };

            return ti;
        }
    }
}
