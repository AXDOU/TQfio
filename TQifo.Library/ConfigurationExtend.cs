
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TQifo.Library
{
    public   class ConfigurationExtend
    {
        public static IConfiguration Configs;
        public static string GetValue(string key)
        {
            return Configs[key].ToString();
        }
    }
}
