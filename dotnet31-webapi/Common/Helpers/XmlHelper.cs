using System.IO;
using System.Xml.Serialization;

namespace cmes_webapi.Common.Helpers
{
    public class XmlHelper
    {
        //public static T Deserialize<T>(string xml)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(T));
        //    using (StringReader reader = new StringReader(xml))
        //    {
        //        var data = (T)serializer.Deserialize(reader);
        //        return data;
        //    }
        //}

        public static T Deserialize<T>(string xml, string root = "CrearPendienteSucursalAltaPlazoFijoRequest")
        {
            using (var sr = new StringReader(xml))
            {
                var rootAttribute = new XmlRootAttribute();
                rootAttribute.ElementName = root;
                rootAttribute.IsNullable = true;

                var xmlSerializer = new XmlSerializer(typeof(T), rootAttribute);
                var response = (T)xmlSerializer.Deserialize(sr);

                return response;
            }
        }
    }
}
