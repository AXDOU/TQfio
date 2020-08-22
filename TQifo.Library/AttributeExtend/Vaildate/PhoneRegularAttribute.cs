using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TQifo.Library.AttributeExtend.Vaildate
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PhoneRegularAttribute : BaseRegularAttribute
    {
        private static string phoneRegular = @"^[1]+[3,5]+\d{9}$";
        public PhoneRegularAttribute(): base(phoneRegular)
        {
        }

        public override bool Validate(object value, out string msg)
        {
            msg = string.Empty;
            if (base.Validate(value,out msg))
            {
                return true;
            }
            else
            {
                msg = "手机号格式校验失败";
                return false;
            }
        }
    }
}
