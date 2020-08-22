using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using TQifo.Library.AttributeExtend;
using TQifo.Library.AttributeExtend.Vaildate;


namespace TQifo.Library
{
    public static class Cache
    {
        public static Dictionary<string, string> sqlCache = new Dictionary<string, string>();
        public static Dictionary<PropertyInfo, string> ColumnChineseNameCache = new Dictionary<PropertyInfo, string>();
        public static Dictionary<PropertyInfo, string> ColumnMappingNameCache = new Dictionary<PropertyInfo, string>();

        public static string GetSqlectSql<T>() where T : BaseModel
        {
            Type type = typeof(T);
            string sqlKey = $"SELECT_{AttributeHelper.GetMappingName<T>()}";
            if (Cache.sqlCache.ContainsKey(sqlKey))
            {
                return sqlCache[sqlKey];
            }
            else
            {
                string columnName = string.Join(",", type.GetProperties().Select(prop => $"[{prop.GetMappingColumnName()}]"));
                var sql = $"SELECT {columnName} FROM [{AttributeHelper.GetMappingName<T>()}]";
                sqlCache.Add(sqlKey, sql);
                return sql;
            }
        }


        internal static string GetColumnChineseName(PropertyInfo prop)
        {
            if (!ColumnChineseNameCache.ContainsKey(prop))
            {
                string columnname = string.Empty;
                if (prop.IsDefined(typeof(ColumnAttribute), true))
                {
                    ColumnAttribute attribute = (ColumnAttribute)prop.GetCustomAttribute(typeof(ColumnAttribute), true);
                }
                else
                {
                    columnname = prop.Name;
                }
                ColumnChineseNameCache.Add(prop, columnname);
                return columnname;
            }
            else
                return ColumnChineseNameCache[prop];
     
        }
        /// <summary>
        /// 缓存实体属性对应的数据库中的真是字段
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        internal static string GetColumnMappingName(PropertyInfo prop)
        {
            if (!ColumnMappingNameCache.ContainsKey(prop))
            {
                string mappingName = string.Empty;
                if (prop.IsDefined(typeof(MappingAttribute), true))
               {
                  MappingAttribute attribute =(MappingAttribute)prop.GetCustomAttribute(typeof(MappingAttribute),true);
                    mappingName = attribute.MappingName;
                }
                else
                    mappingName = prop.Name;
                ColumnMappingNameCache.Add(prop, mappingName);
                return mappingName;
            }
            else
                return ColumnMappingNameCache[prop];

        }

       
        public static class CachePropertyAttribute<T> where T : BaseModel
        {
            public static Dictionary<PropertyInfo, object[]> Property_Attr_Cache = new Dictionary<PropertyInfo, object[]>();

            static CachePropertyAttribute()
            {
                Type type = typeof(T);
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    Property_Attr_Cache.Add(prop, prop.GetCustomAttributes(typeof(AbstractValidateAttribute), true));
                }
            }
        }
    }
    public static class CacheHelper<T>
    {
        public static string MappingName = null;
        static CacheHelper()
        {
            Type type = typeof(T);
            if (type.IsDefined(typeof(MappingAttribute), true))
            {
                MappingAttribute attribute = (MappingAttribute)type.GetCustomAttribute(typeof(MappingAttribute), true);

                MappingName = attribute.MappingName;

            }
            else
                MappingName = type.Name;
        }
    }

    public static class CachePropertyAttribute<T> where T : BaseModel 
    {
        public static Dictionary<PropertyInfo, object[]> Property_Attribute_Cache = new Dictionary<PropertyInfo, object[]>();

        static CachePropertyAttribute()
        {
            Type type = typeof(T);
            foreach(PropertyInfo prop in type.GetProperties())
            {
                Property_Attribute_Cache.Add(prop, prop.GetCustomAttributes(typeof(AbstractValidateAttribute), true));
            }
        }
    }

}
