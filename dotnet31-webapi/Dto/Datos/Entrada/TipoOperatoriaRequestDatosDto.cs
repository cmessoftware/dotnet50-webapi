using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{
	public class TipoOperatoriaRequestDatosDto
	{
		public string AgenteColocador { get; set; }
		public string Canal { get; set; }
		public string Comitente { get; set; }
		public string HostId { get; set; }
	}
}
