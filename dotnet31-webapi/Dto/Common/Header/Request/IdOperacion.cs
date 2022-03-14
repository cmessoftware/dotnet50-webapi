using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{
	[XmlRoot(ElementName = "IdOperacion")]
	public class IdOperacion
	{
		[XmlAttribute(AttributeName = "idEsquema")]
		public string IdEsquema { get; set; }
		[XmlText]
		public string Text { get; set; }
	}
}
