using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.CMS.Model.Entity
{
    /// <summary>
    /// redis 表的 key前缀
    /// 规则 redis+表名+KEY
    /// </summary>
    public class RedisKeys
    {
        const string databas = "itCMSAccount:";
        /// <summary>
        /// 登陆管理key
        /// </summary>
        public const string REDIS_KEY_ADMINLOGIN = databas + "adminlogin:KEY:";
        /// <summary>
        /// 验证码key
        /// </summary>
        public const string REDIS_VERIFICATION = databas + "Verification:KEY:";
        /// <summary>
        /// 权限分组key
        /// </summary>
        public const string REDIS_KEY_sys_acl_group = databas + "sys_acl_group:KEY:";
        /// <summary>
        /// 用户权限key
        /// </summary>
        public const string REDIS_KEY_sys_acl_user = databas + "sys_acl_user:KEY:";
        /// <summary>
        /// 权限key
        /// </summary>
        public const string REDIS_KEY_sys_action = databas + "sys_action:KEY:";
        /// <summary>
        /// 权限key
        /// </summary>
        public const string REDIS_KEY_sys_Department = databas + "sys_Department:KEY:";
        /// <summary>
        /// 分组key
        /// </summary>
        public const string REDIS_KEY_sys_group = databas + "sys_group:KEY:";
        /// <summary>
        /// 用户分组key
        /// </summary>
        public const string REDIS_KEY_sys_group_user = databas + "sys_group_user:KEY:";
        /// <summary>
        /// 用户分组key
        /// </summary>
        public const string REDIS_KEY_sys_module = databas + "sys_module:KEY:";
        /// <summary>
        /// 用户key
        /// </summary>
        public const string REDIS_KEY_sys_user = databas + "sys_user:KEY:";
 
    }
}
