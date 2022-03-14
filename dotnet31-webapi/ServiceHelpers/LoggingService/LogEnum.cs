using cmes_webapi.Common.Resources;
using System.Resources;

namespace cmes_webapi.Common.Services
{
    public enum EnumLogType
    {
        Error = 4,
        Information = 2
    };
    public static class LogError
    {
        public static string GetErrorDescription(string codigo)
        {
            ResourceManager rm = new ResourceManager(typeof(ErrorCode));
            return rm.GetString(codigo);
        }
    }

}
