using System;
using System.Collections.Generic;
using System.Text;

namespace TQifo.Library.AttributeExtend.Vaildate
{
    public class RequiredAttribute : AbstractValidateAttribute
    {
        public override bool Validate(object value, out string msg)
        {
            msg = string.Empty;
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                return true;
            }
            else
            {
                msg = "非空格式校验失败";
                return false;
            }
        }
    }
}
