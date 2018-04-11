using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.CMS.Model
{
    public class adminlogin
    {
        public Guid Token { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime LoginDate { get; set; }
    }
}
