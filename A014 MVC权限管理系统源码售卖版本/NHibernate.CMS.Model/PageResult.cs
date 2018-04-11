using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.CMS.Model
{
    /// <summary>
    /// 分页
    /// </summary>
   public class PageResult
    {
       public int pageSize { get; set; }
       public int pageIndex { get; set; }
       public int total { get; set; }
    }
}
