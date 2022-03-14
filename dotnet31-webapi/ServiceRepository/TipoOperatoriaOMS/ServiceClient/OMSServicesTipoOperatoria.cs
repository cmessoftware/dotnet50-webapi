using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using cmes_webapi.Api.Dto;
using cmes_webapi.Common.Configuration;
using cmes_webapi.ServiceHelper.Contracts;
using cmes_webapi.Services;
using System;
using System.Threading.Tasks;

namespace cmes_webapi.ServiceRepository.OMS
{

	public partial class OMSServicesTipoOperatoria : ServiceBase, IOMSServicesTipoOperatoria
	{
		private readonly ILogger _logger;
		private readonly IRestServiceClient _client;
		private static readonly string uuid = Guid.NewGuid().ToString();

		public OMSServicesTipoOperatoria(ILogger logger,
										 IRestServiceClient client)
		{
			_logger = logger;
			_client = client;

		}

		public async Task<TipoOperatoriaOutput> GetTipoOperatoria(TipoOperatoriaInput input)
		{

			TipoOperatoriaOutput response = null;
			string infoLog = uuid + ";JAVA; Inicio GetBrokers;";
			string baseUrl = GlobalSettings.ACDI_TIPOOPERATORIA_URL;
			_logger.LogInformation(infoLog + "Inicio.");

			//Invocamos al OMS.
			response = await _client.SendPostAsync<TipoOperatoriaInput,
												   TipoOperatoriaOutput>
												   (baseUrl, input);

			if (response != null)
			{
				infoLog = uuid + ";JAVA; VALOR VARIABLE IS_ACDI: " + response.IsAcdi + ";";
				_logger.LogInformation(infoLog + "Inicio.");
			}
			else
			{
				infoLog = uuid + ";JAVA; OUTPUT NULL;";
				_logger.LogInformation(infoLog + "Inicio.");


				return response;

			}

				
			infoLog = uuid + ";JAVA; Fin GetBrokers;";
			_logger.LogInformation(infoLog + "Inicio.");

			return response;
		}
	}	
}