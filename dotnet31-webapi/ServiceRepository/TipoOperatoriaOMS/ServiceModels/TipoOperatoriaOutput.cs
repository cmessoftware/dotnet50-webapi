namespace cmes_webapi.ServiceRepository.OMS
{
	public class TipoOperatoriaOutput
	{
		public string BrokerID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string TradeBook { get; set; }
		public bool Internal { get; set; }
		public bool Active { get; set; }
		public bool IsAcdi { get; set; }
		public string ClientID { get; set; }
		public string SetupDateTime { get; set; }
		public string SetupUser { get; set; }
		public string ModifiedDateTime { get; set; }
		public string ModifiedUser { get; set; }
		public string ProductTypes { get; set; }
		public string ProductExceptions { get; set; }
	}
}