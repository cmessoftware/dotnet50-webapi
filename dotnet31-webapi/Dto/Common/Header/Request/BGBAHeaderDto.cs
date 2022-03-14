using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{
	[XmlRoot(ElementName = "BGBAHeader")]
	public class BGBAHeaderDto
	{
		[XmlElement(ElementName = "Identificadores")]
		[Required(ErrorMessage ="El nodo Identificadores es requerido")]
		public Identificadores Identificadores { get; set; }
		[XmlElement(ElementName = "ModuloAplicativo")]
		public ModuloAplicativo ModuloAplicativo { get; set; }
		[XmlElement(ElementName = "Equipo")]
		public Equipo Equipo { get; set; }
		[XmlElement(ElementName = "Origen")]
		public Origen Origen { get; set; }
	}
}
