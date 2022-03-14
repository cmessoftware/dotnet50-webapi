using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{
	[XmlRoot(ElementName = "IdMensaje")]
	public class IdMensaje
	{
		[XmlAttribute(AttributeName = "idEsquema")]
		public string IdEsquema { get; set; }
		[XmlText]
		public string Text { get; set; }
	}
}
