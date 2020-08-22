using System;
using System.Collections.Generic;
using System.Text;

namespace TQifo.Library.AttributeExtend
{
    [AttributeUsage(AttributeTargets.Property)]
   public  class ColumnAttribute:Attribute
    {
        public string ColumnName { get; private set; }

        public ColumnAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }

    }
}
