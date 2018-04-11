using NHibernate.CMS.DTO;
using NHibernate.CMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.CMS.Business
{
    public class CustomerLogic
    {
        protected readonly CustomerOperator dal = new CustomerOperator();
        public IList<Customer> GetAll()
        {
            return this.dal.LoadEntities(" from Customer");
        }
    }
}
