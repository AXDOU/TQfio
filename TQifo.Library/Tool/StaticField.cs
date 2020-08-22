
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace TQifo.Library
{
    public  static class StaticField
    {
        public static string Connection = "Data Source=.; Database=Course12; User ID=sa; Password=sa123; MultipleActiveResultSets=True";// ConfigurationExtend.GetValue("connectionStrings");
        public static string ModelFilePath = "C:\\Users\\Administrator\\Desktop";// ConfigurationExtend.GetValue("ModelFilePath");
        private static string DLL_TYPE = "TQifo.Service.BaseService,TQifo.Service";//ConfigurationExtend.GetValue("DllSetting");
        public static string DllTypeName = DLL_TYPE.Split(',')[0];
        public static string DllName = DLL_TYPE.Split(',')[1];
    }
}
