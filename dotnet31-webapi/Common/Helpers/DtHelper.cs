using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace cmes_webapi.Common.Helpers
{
    public class DtHelper
    {
        private DtHelper()
        {
        }

        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public static IList<T> ConvertTo<T>(DataView view)
        {
            if (view == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in view.Table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        var value = row[column.ColumnName];

                        if (prop == null)
                            throw new ArgumentException($"Nombre de campo: {column.ColumnName} es incorrecto");

                        //Si son diferente algo vino mal (null o DBNull)
                        //Lo cargo con null o vacio (si es string)
                        if (prop.PropertyType.Name != value.GetType().Name)
                        {
                            if (prop.PropertyType.Name == "Nullable`1")
                            {
                                switch (value.GetType().Name)
                                {
                                    case "Int32":
                                        value = value as int?;
                                        break;
                                    case "Decimal":
                                        value = value as decimal?;
                                        break;
                                    case "DateTime":
                                        value = ConvertNulleableType(value, value.GetType());
                                        break;
                                    case "DBNull":
                                        var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                        value = ConvertNulleableType(value, type);
                                        break;
                                }
                            }
                            else
                            {
                                if (value == null || string.IsNullOrEmpty(value.ToString()))
                                {
                                    switch (value.GetType().Name)
                                    {
                                        case "String":
                                            value = string.Empty;
                                            break;
                                        case "Int32":
                                            value = 0;
                                            break;
                                        case "Int32?":
                                            value = 0;
                                            break;
                                        case "Decimal":
                                            value = 0;
                                            break;
                                        case "Decimal?":
                                            value = 0;
                                            break;
                                        case "Int64":
                                            value = 0;
                                            break;
                                        case "Int64?":
                                            value = 0;
                                            break;
                                        case "bool?":
                                            value = null;
                                            break;
                                        case "bool":
                                            value = false;
                                            break;
                                        case "DateTime?":
                                            value = DateTime.MinValue;
                                            break;
                                        case "DateTime":
                                            value = DateTime.MinValue;
                                            break;
                                        case "DBNull":
                                            Type type;
                                            if (prop.PropertyType.Name == "Nullable`1")
                                            {
                                                type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                                value = ConvertNulleableType(value, type);
                                            }
                                            else
                                            {
                                                type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                                value = SetDefaultValue(type);
                                            }

                                            break;
                                        case "Nullable`1":
                                            type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                            value = Convert.ToString(value);
                                            value = string.Empty;
                                            break;
                                    }
                                }
                                else
                                {
                                    if (value.GetType().Name == "Int32")
                                        value = value.ToString();
                                    if (value.GetType().Name == "Decimal")
                                        value = value.ToString();

                                }
                            }
                        }

                        prop.SetValue(obj, value, null);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }

            return obj;
        }

        private static object SetDefaultValue(Type type)
        {
            object value = null;

            switch (type.Name)
            {
                case "String":
                    value = string.Empty;
                    break;
                case "Int32":
                    value = 0;
                    break;
                case "Int32?":
                    value = 0;
                    break;
                case "Decimal":
                    value = 0;
                    break;
                case "Decimal?":
                    value = 0;
                    break;
                case "Int64":
                    value = 0;
                    break;
                case "Int64?":
                    value = 0;
                    break;
                case "bool?":
                    value = null;
                    break;
                case "bool":
                    value = false;
                    break;
                case "DateTime?":
                    value = DateTime.MinValue;
                    break;
                case "DateTime":
                    value = DateTime.MinValue;
                    break;
            }

            return value;
        }

        private static object ConvertNulleableType(object value, Type type)
        {
            switch (type.Name)
            {
                case "String":
                    value = Convert.ToString(value);
                    break;
                case "Int32":
                    value = 0;
                    value = (int?)Convert.ToInt32(value);
                    break;
                case "Decimal":
                    value = 0;
                    value = (decimal?)Convert.ToDecimal(value);
                    break;
                case "DateTime":
                    value = DateTime.MinValue;
                    value = (DateTime?)Convert.ToDateTime(value);
                    break;
            }

            return value;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }
    }
    #region Ejemplo de uso
    //public class MyClass
    //{
    //    public static void Main()
    //    {
    //        List<Customer> customers = new List<Customer>();

    //        for (int i = 0; i < 10; i++)
    //        {
    //            Customer c = new Customer();
    //            c.Id = i;
    //            c.Name = "Customer " + i.ToString();

    //            customers.Add(c);
    //        }

    //        DataTable table = DtHelper.ConvertTo<Customer>(customers);

    //        foreach (DataRow row in table.Rows)
    //        {
    //            Console.WriteLine("Customer");
    //            Console.WriteLine("---------------");

    //            foreach (DataColumn column in table.Columns)
    //            {
    //                object value = row[column.ColumnName];
    //                Console.WriteLine("{0}: {1}", column.ColumnName, value);
    //            }

    //            Console.WriteLine();
    //        }

    //        Console.ReadLine();
    //    }
    //}
    #endregion

}
