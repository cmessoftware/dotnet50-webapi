using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{
	[XmlRoot(ElementName = "Origen")]
	public class Origen
	{
		[XmlElement(ElementName = "ModuloAplicativo")]
		public ModuloAplicativo ModuloAplicativo { get; set; }
		[XmlElement(ElementName = "Canal")]
		public string Canal { get; set; }
		[XmlElement(ElementName = "Equipo")]
		public Equipo Equipo { get; set; }
		[XmlElement(ElementName = "Terminal")]
		public string Terminal { get; set; }
		[XmlElement(ElementName = "Operador")]
		public string Operador { get; set; }
		[XmlElement(ElementName = "Supervision")]
		public string Supervision { get; set; }
	}
}