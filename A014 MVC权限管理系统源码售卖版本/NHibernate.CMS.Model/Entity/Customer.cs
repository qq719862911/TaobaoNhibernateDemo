using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.CMS.Model
{
  public  class Customer
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string CustomerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ContactName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ContactTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Region { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PostalCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Country { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Fax { get; set; }
    }
}
