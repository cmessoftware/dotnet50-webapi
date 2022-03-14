using System;
using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{

	[XmlRoot(ElementName = "BGBAResultadoOperacion")]
	public class BGBAResultadoOperacionDto
	{
		[XmlElement(ElementName = "Severidad")]
		public string Severidad { get; set; }
		[XmlElement(ElementName = "Codigo")]
		public string Codigo { get; set; }
		[XmlElement(ElementName = "Descripcion")]
		public string Descripcion { get; set; }
		[XmlElement(ElementName = "tipo")]
		public string Tipo { get; set; }
		[XmlElement(ElementName = "urlDetalle")]
		public string UrlDetalle { get; set; }
		[XmlElement(ElementName = "idRespuesta")]
		public IdRespuesta IdRespuesta { get; set; }
	}

}
