using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace cmes_webapi.Entities.Services.Helpers
{
    public class DataErrorService
    {
        public static string FindLongStrings(object testObject)
        {
            foreach (PropertyInfo propInfo in testObject.GetType().GetProperties())
            {
                foreach (ColumnAttribute attribute in propInfo.GetCustomAttributes(typeof(ColumnAttribute), true))
                {
                    if (!attribute.TypeName.ToLower().Contains("varchar"))
                    {
                        continue;
                    }
                    string dbType = attribute.TypeName.ToLower();
                    int numberStartIndex = dbType.IndexOf("varchar(") + 8;
                    int numberEndIndex = dbType.IndexOf(")", numberStartIndex);
                    string lengthString = dbType.Substring(numberStartIndex, (numberEndIndex - numberStartIndex));
                    int maxLength = 0;
                    int.TryParse(lengthString, out maxLength);

                    string currentValue = (string)propInfo.GetValue(testObject, null);

                    if (!string.IsNullOrEmpty(currentValue) && currentValue.Length > maxLength && lengthString != "max")
                        return testObject.GetType().Name + "." + propInfo.Name + " " + currentValue + " Max: " + maxLength;
                }
            }
            return "";
        }
    }
}
