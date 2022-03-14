using cmes_webapi.Common.Configuration;
using System.Xml;

namespace cmes_webapi.ServiceHelper.Contracts
{
    public interface ISoapClientService
    {
        public string Invoke(string url, string soapMessage, Authentication.Credential credential = null);
        public string Invoke(string url, XmlDocument soapEnvelopeXml, Authentication.Credential credential);


    }
}