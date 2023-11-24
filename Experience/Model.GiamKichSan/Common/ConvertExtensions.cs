using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace Model.GiamKichSan.Common
{
    public static class ConvertExtensions
    {
        public static List<T> ToList<T>(this DataTable dataTable)
        {
            List<T> data = new List<T>();
            if (dataTable == null) return data;

            foreach (DataRow row in dataTable.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetFirst<T>(this DataTable dataTable)
        {
            T obj = Activator.CreateInstance<T>();
            if (dataTable == null) return obj;

            if (dataTable != null && dataTable.Rows != null && dataTable.Rows.Count > 0)
                return dataTable.Rows[0].GetItem<T>();
            return obj;
        }

        #region private
        public static T GetItem<T>(this DataRow dataRow)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (PropertyInfo propertyInfo in temp.GetProperties())
            {
                foreach (DataColumn column in dataRow.Table.Columns)
                {
                    if (propertyInfo.Name.ToUpper() == column.ColumnName.ToUpper())
                    {
                        object value = ConvertPropertiesClass.ConvertValue(dataRow[column.ColumnName], propertyInfo.PropertyType);
                        if (value != null && value != DBNull.Value)
                            propertyInfo.SetValue(obj, value, null);
                    }
                }
            }
            return obj;
        }
        public static T ConvertObject<U, T>(this U data)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (PropertyInfo propertyInfo in temp.GetProperties())
            {
                //foreach (DataColumn column in dataRow.Table.Columns)
                foreach (PropertyInfo propertyData in typeof(U).GetProperties())
                {
                    if (propertyInfo.Name == propertyData.Name)
                    {
                        object value = ConvertPropertiesClass.ConvertValue(data, propertyInfo.PropertyType);
                        if (value != null && value != DBNull.Value)
                            propertyInfo.SetValue(obj, value, null);
                    }
                }
            }
            return obj;
        }
        
        #endregion
    }

    internal class ConvertPropertiesClass
    {
        public static object ConvertValue(object value, Type conversionType)
        {
            if (IsNullableType(conversionType))
            {
                return ConvertNullableValue(value, conversionType);
            }
            return ConvertNonNullableValue(value, conversionType);
        }
        private static bool IsNullableType(Type typeOfObject)
        {
            return typeOfObject.IsGenericType && typeOfObject.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        private static object ConvertNullableValue(object value, Type conversionType)
        {
            if (value != DBNull.Value)
            {
                NullableConverter nullableConverter = new NullableConverter(conversionType);
                return nullableConverter.ConvertFrom(value);
            }
            return null;
        }
        private static object ConvertNonNullableValue(object value, Type conversionType)
        {
            object result = null;
            if (value != DBNull.Value && value != null)
            {
                if (conversionType.BaseType == typeof(Enum))
                {
                    try
                    {
                        result = Enum.Parse(conversionType, Convert.ToString(value));
                    }
                    catch
                    {
                        result = Activator.CreateInstance(conversionType);
                    }
                }
                else
                {
                    result = Convert.ChangeType(value, conversionType, CultureInfo.CurrentCulture);
                }
            }
            else if (conversionType.IsValueType)
            {
                result = Activator.CreateInstance(conversionType);
            }
            return result;
        }
    }
}
