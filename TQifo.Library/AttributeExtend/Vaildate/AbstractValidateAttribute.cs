using System;
using System.Collections.Generic;
using System.Text;

namespace TQifo.Library.AttributeExtend.Vaildate
{

    public abstract class AbstractValidateAttribute :Attribute
    {
        public abstract bool Validate(object value, out string msg);
    }
}
