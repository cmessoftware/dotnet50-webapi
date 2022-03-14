using Microsoft.AspNetCore.Mvc;
using cmes_webapi.Api.Dto;
using cmes_webapi.Services;
using System;
using System.Threading.Tasks;

namespace cmes_webapi.Api.Controllers
{



    [ApiController]
    [Route("v1")]
    public class TipoOperatoriaController : ControllerBase
    {

        ITipoOperatoriaService _tipoOperatoriaService;

        public TipoOperatoriaController(ITipoOperatoriaService tipoOperatoriaService)
        {
            _tipoOperatoriaService = tipoOperatoriaService;
        }

        // POST: api/TipoOperatoria --> TODO: IMPLEMENTAR!!
        [HttpPost("paas/acdi/tipooperatoria/consultar")]
        public async Task<IActionResult> TipoOperatoria(TipoOperatoriaRequestDto request)
        {
            ResponseResultDto<TipoOperatoriaResponseDatosDto> response = null;
            await Task.Run(async () =>
            {
                response = await _tipoOperatoriaService.GetTipoOperatoria(request);
            });

            return Ok(response);
        }

        //[HttpGet("health")]
        public async Task<IActionResult> Get()
        {

            string m2 = $"Hoy es {DateTime.Now:yyyyMMdd:hhmmss}";

            return Ok(m2);

        }

    }
}
