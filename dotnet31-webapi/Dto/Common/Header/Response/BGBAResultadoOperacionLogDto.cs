using cmes_webapi.Api.Dto;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{
	[XmlRoot(ElementName = "BGBAResultadoOperacionLog")]
	public class BGBAResultadoOperacionLogDto
	{

		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
        public List<LogItemDto> LogItem { get; internal set; }
    }
}
