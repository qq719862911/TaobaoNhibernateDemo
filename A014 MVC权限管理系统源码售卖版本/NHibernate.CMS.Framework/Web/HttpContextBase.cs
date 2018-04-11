using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NHibernate.CMS.Framework.Web
{
    public class HttpContextBase
    {

        /// <summary>
        /// Cache或者Cookie的Key前缀
        /// </summary>
        public virtual string KeyPrefix
        {
            get
            {
                return "Context_";
            }
        }
        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetServerPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }
            string _path = System.Web.HttpContext.Current.Server.MapPath(path);
            return _path;
           
        }
    }
}
