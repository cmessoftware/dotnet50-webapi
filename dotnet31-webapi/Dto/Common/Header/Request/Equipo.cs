using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{

	[XmlRoot(ElementName = "Equipo")]
	public class Equipo
	{
		[XmlAttribute(AttributeName = "ip")]
		public string Ip { get; set; }
		[XmlAttribute(AttributeName = "nombre")]
		public string Nombre { get; set; }
	}
}