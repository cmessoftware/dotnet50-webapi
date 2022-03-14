using AutoMapper;
using Microsoft.Extensions.Logging;

namespace cmes_webapi.Services
{
    public partial class TipoOperatoriaService : ServiceBase, ITipoOperatoriaService
    {

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IOMSServicesTipoOperatoria _omsServicesTipoOperatoria;


        public TipoOperatoriaService(ILogger<TipoOperatoriaService> logger,
                                     IMapper mapper,
                                     IOMSServicesTipoOperatoria omsServicesTipoOperatoria)
        {
            _logger = logger;
            _mapper = mapper;
            _omsServicesTipoOperatoria = omsServicesTipoOperatoria;
        }

    }
}

