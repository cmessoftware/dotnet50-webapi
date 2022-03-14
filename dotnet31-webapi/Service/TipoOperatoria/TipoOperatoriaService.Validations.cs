using cmes_webapi.Api.Dto;


namespace cmes_webapi.Services
{
    public partial class TipoOperatoriaService
    {
        private void ValidateTipoOperatoriaIsNotNull(TipoOperatoriaRequestDto TipoOperatoriaRequestDto)
        {
            if (TipoOperatoriaRequestDto is null)
            {
                throw new TipoOperatoriaNullException();
            }
        }
        
    }
}
