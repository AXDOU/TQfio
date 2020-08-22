using System;
using System.Collections.Generic;
using System.Text;

namespace TQifo.Library.AttributeExtend.Vaildate
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class LengthAttribute : AbstractValidateAttribute
    {
        private int _Max = 0, _Min = 0;
        public LengthAttribute(int min, int max)
        {
            this._Min = min;
            this._Max = max;
        }

        public override bool Validate(object value, out string msg)
        {
            msg = string.Empty;
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()) && value.ToString().Length >= this._Min && value.ToString().Length <= this._Max)
            {
                return true;
            }
            else
            {
                msg = $"长度校验失败[{this._Min}-{this._Max}]";
                return false;
            }
        }
    }
}
