using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{

	[XmlRoot(ElementName = "ModuloAplicativo")]
	public class ModuloAplicativo
	{
		[XmlElement(ElementName = "IdGalicia")]
		public string IdGalicia { get; set; }
		[XmlElement(ElementName = "IdConsumidor")]
		public string IdConsumidor { get; set; }
		[XmlElement(ElementName = "IdProveedor")]
		public string IdProveedor { get; set; }
	}
}