using NHibernate.CMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using NHibernate.CMS.IDTO;

namespace NHibernate.CMS.DTO
{
    //public class CustomerOperator : BaseOperator
    public partial class CustomerOperator : BaseRepository<Customer>, ICustomerRepository
    {

        //public object Save(Customer customer)
        //{
        //    var id = Session.Save(customer);
        //    Session.Flush();
        //    return id;
        //}

        //public void Update(Customer customer)
        //{
        //    Session.Update(customer);
        //    Session.Flush();
        //}

        //public void Delete(Customer customer)
        //{
        //    Session.Delete(customer);
        //    Session.Flush();
        //}

        //public Customer Get(object id)
        //{
        //    return Session.Get<Customer>(id);
        //}

        //public IList<Customer> GetAll()
        //{
             
        //    return Session.Query<Customer>().ToList();
        //}

    }
}
