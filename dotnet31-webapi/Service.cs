using cmes_webapi.Common.Configuration;

namespace cmes_webapi
{
    public class Service
    {
        //public static IMapper mapper;

        //public static ILogger logger;
        ////Ejemplo de como hacer un mapper mas complejo

        ////public static OrderFund MappFromTo(OrderFundInput item)
        ////{
        ////    var orderfund = mapper.Map<OrderFundInput, OrderFund>(item);
        ////    orderfund.Settlements = OrderProcess.GetSettlementList(item.Settlements);
        ////    return orderfund;
        ////}

        public static void SaveSettings()
        {
            ReadSettings.SaveSettings(ReadSettings.ImpresionCertificadoPF);
        }
    }
}
