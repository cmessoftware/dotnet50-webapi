using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{

	[XmlRoot(ElementName = "Identificadores")]
	public class Identificadores
	{
		[XmlElement(ElementName = "IdMensaje")]
		public IdMensaje IdMensaje { get; set; }
		[XmlElement(ElementName = "IdOperacion")]
		public IdOperacion IdOperacion { get; set; }
	}
}
