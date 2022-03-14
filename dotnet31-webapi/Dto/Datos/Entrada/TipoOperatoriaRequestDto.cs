using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{

    public class TipoOperatoriaRequestDto
    {
        public BGBAHeaderDto BGBAHeader { get; set; }

        public TipoOperatoriaRequestDatosDto Datos { get; set; }
    }
}
