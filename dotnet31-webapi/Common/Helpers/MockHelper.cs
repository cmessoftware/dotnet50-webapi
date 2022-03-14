using System.IO;

namespace cmes_webapi.Common.Helpers
{
    public class MockHelper
    {
        public static string GetJsonResponse(string responseName)
        {
            string path = $"Dominio/ImpresionPF/CrearImpresionPF" + 
                          $"/Implementacion/MockResponses/{responseName}.json";
            return File.ReadAllText(path);
        }

    }
}
