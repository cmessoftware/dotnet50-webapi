using cmes_webapi.Api.Dto;
using cmes_webapi.Mappers;
using cmes_webapi.ServiceRepository.OMS;
using System.Threading.Tasks;

namespace cmes_webapi.Services
{
    public partial class TipoOperatoriaService
    {
       
        public Task<ResponseResultDto<TipoOperatoriaResponseDatosDto>> GetTipoOperatoria(TipoOperatoriaRequestDto request)
         => TryCatch(async () =>
         {

             TipoOperatoriaInput input = TipoOperatoriaMapper.Map(request);
             TipoOperatoriaResponseDatosDto response = null;

             var omsResponse = await _omsServicesTipoOperatoria.GetTipoOperatoria(input);

             if(omsResponse != null)
                 response = _mapper.Map<TipoOperatoriaResponseDatosDto>(omsResponse);
           
             return CreateResponse(response, response == null);
         
         });

      
    }
}

