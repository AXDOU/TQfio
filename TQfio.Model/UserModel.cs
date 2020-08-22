using System;
using System.Collections.Generic;
using System.Text;
using TQifo.Library;
using TQifo.Library.AttributeExtend;
using TQifo.Library.AttributeExtend.Vaildate;

namespace TQifo.Model
{
    [Mapping("User")]
    public class UserModel : BaseModel
    {
        [Column("姓名")]
        [Required]
        [Length(2, 8)]
        public string Name { get; set; }

        [Column("账号")]
        [Length(6, 20)]
        [Required]
        public string Account { get; set; }

        [Column("密码")]
        [Required]
        public string Password { get; set; }

        [Column("邮件")]
        [Required]
        [EmailRegular]
        public string Email { get; set; }

        [Column("手机号")]
        [Required]
        [PhoneRegular]
        public string Mobile { get; set; }

        [Column("公司Id")]
        public int? CompanyId { get; set; }

        [Column("公司名称")]
        public string CompanyName { get; set; }

        [Column("状态")]
        [Mapping("State")]
        public int Status { get; set; }

        [Column("用户类型")]
        public int UserType { get; set; }

        [Column("最后登陆时间")]
        public DateTime? LastLoginTime { get; set; }

        [Column("创建时间")]
        public DateTime CreateTime { get; set; }

    

        [Column("创建人Id")]
        public int CreatorId { get; set; }

        [Column("最后修改人员Id")]
        public int? LastModifierId { get; set; }

        [Column("最后修改时间")]
        public DateTime? LastModifyTime { get; set; }
    }

}
