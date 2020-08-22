using System;
using System.Reflection;
using TQifo.IService;
using TQifo.Library;

namespace TQifo.Factory
{
    public class SimpleFactory
    {
      
        static SimpleFactory()
        {
            Assembly assembly = Assembly.Load(StaticField.DllName);
            DllType = assembly.GetType(StaticField.DllTypeName);
        }
        private static Type DllType;
        public static IBaseService CreateInstance()
        {
        
            return (IBaseService)Activator.CreateInstance(DllType);
        }
    }

}
