using cmes_webapi.Api.Dto;
using cmes_webapi.ServiceRepository.OMS;
using System.Threading.Tasks;

namespace cmes_webapi.Services
{
    public interface IOMSServicesTipoOperatoria
    {
        public Task<TipoOperatoriaOutput> GetTipoOperatoria(TipoOperatoriaInput input);
    }
}