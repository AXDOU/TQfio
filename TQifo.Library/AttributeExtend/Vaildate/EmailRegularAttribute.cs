using System;
using System.Collections.Generic;
using System.Text;

namespace TQifo.Library.AttributeExtend.Vaildate
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class EmailRegularAttribute : BaseRegularAttribute
    {
        private static string emailRegular = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public EmailRegularAttribute():base(emailRegular)
        {

        }

        public override bool Validate(object value, out string msg)
        {
            msg = string.Empty;
            if(base.Validate(value,out msg))
            {
                return true;
            }
            else
            {
                msg = "邮箱地址校验失败";
                return false;
            }
        }

    }
}
