using System;
using System.Collections.Generic;
using System.Text;

namespace TQifo.Library.AttributeExtend.Vaildate
{
    public class LongAttribute : AbstractValidateAttribute
    {
        private long _Max = 0, _Min = 0;

        public LongAttribute(long min, long max)
        {
            this._Max = max;
            this._Min = min;
        }
        public override bool Validate(object value, out string msg)
        {
            msg = string.Empty;
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()) && long.TryParse(value.ToString(), out long lValue) && lValue >= this._Min && lValue <=  this._Max)
            {
                return true;
            }
            else
            {
                msg = $"区间大小校验失败";
                return false;
            }
        }
    }
}
