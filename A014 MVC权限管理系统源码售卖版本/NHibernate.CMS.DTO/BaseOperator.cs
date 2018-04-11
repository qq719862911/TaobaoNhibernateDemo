using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.CMS.DTO
{
    /// <summary>
    /// NHibernate调用基类--用法1
    /// </summary>
    public class BaseOperator
    {
        private ISession m_Session;
        public ISession Session
        {
            get { return m_Session; }
        }

        private ISessionFactory m_SessionFactory;
        public BaseOperator()
        {

            var config = new Configuration().Configure("D:/GuoHaiyiFile/A014 MVC权限管理系统源码售卖版本/NHibernate.CMS.MVC/bin/Config/hibernate.cfg.xml");
            m_SessionFactory = config.BuildSessionFactory();
            m_Session = m_SessionFactory.OpenSession();
        }
    }

}
