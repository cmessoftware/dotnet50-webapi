using cmes_webapi.Api.Dto;
using System.Threading.Tasks;

namespace cmes_webapi.Services
{
    public interface ITipoOperatoriaService
    {
        public Task<ResponseResultDto<TipoOperatoriaResponseDatosDto>> GetTipoOperatoria(TipoOperatoriaRequestDto request);




    }
}