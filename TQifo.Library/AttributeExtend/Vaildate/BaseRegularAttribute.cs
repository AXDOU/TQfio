using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TQifo.Library.AttributeExtend.Vaildate
{

    public class BaseRegularAttribute : AbstractValidateAttribute
    {
        private string _Regular = null;
        public BaseRegularAttribute(string reqular)
        {
            this._Regular = reqular;
        }
        public override bool Validate(object value, out string msg)
        {
            msg = string.Empty;
            return value != null && !string.IsNullOrWhiteSpace(value.ToString()) && Regex.IsMatch(value.ToString(), _Regular);
        }
    }
}
