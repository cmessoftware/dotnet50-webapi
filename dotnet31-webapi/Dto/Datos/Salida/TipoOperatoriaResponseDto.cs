using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{
	public class TipoOperatoriaResponseDto
	{
		public BGBAResultadoOperacionDto BGBAResultadoOperacion { get; set; }
		public TipoOperatoriaResponseDatosDto Datos { get; set; }
	}
}
