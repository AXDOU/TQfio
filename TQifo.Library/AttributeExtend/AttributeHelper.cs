using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TQifo.Library.AttributeExtend.Vaildate;

namespace TQifo.Library.AttributeExtend
{
   public static  class AttributeHelper
    {
        /// <summary>
        /// 获取属性中文名称
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static string GetColumnName(this PropertyInfo prop)
        {
            return Cache.GetColumnChineseName(prop);
        }

        /// <summary>
        /// 获取实体属性对应数据库的字段名称
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static string GetMappingColumnName(this PropertyInfo prop)
        {
            return Cache.GetColumnMappingName(prop);
        }

        public static string GetMappingName<T>()
        {
            return CacheHelper<T>.MappingName;
        }

        public static bool Validate<T>(this T t,out List<string> errList) where T : BaseModel
        {
            errList = new List<string>();
            var properties = CachePropertyAttribute<T>.Property_Attribute_Cache.Keys;
            foreach(PropertyInfo prop in properties)
            {
                var propValue = prop.GetValue(t);
                var attributeArray = CachePropertyAttribute<T>.Property_Attribute_Cache[prop];
                foreach(AbstractValidateAttribute attribute in attributeArray)
                {
                    if(!attribute.Validate(propValue,out string msg))
                    {
                        errList.Add($"{prop.GetColumnName()}-值：{propValue} 错误信息：{msg}");
                    }
                }
            }
            return errList.Count == 0;
        }
    }
}
