using System;
using System.Collections.Generic;
using System.Text;

namespace TQifo.Library.AttributeExtend
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Property)]
   public  class MappingAttribute : Attribute
    {

        public string MappingName { get; private set; }

        public MappingAttribute(string mappingName)
        {
            this.MappingName = mappingName;
        }

    }
}
