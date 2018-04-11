using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.CMS.Model
{
    //管理员信息表
    public class dt_manager
    {

        /// <summary>
        /// 自增ID
        /// </summary>
        public virtual string id
        {
            get;
            set;
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual string role_id
        {
            get;
            set;
        }
         
        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string user_name
        {
            get;
            set;
        }
        /// <summary>
        /// 登录密码
        /// </summary>
        public virtual string password
        {
            get;
            set;
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string telephone
        {
            get;
            set;
        }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public virtual string email
        {
            get;
            set;
        }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public virtual int? is_lock
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public virtual DateTime? add_time
        {
            get;
            set;
        }
        /// <summary>
        /// sys_id
        /// </summary>
        public virtual string sys_id
        {
            get;
            set;
        }        
		
    }
}
