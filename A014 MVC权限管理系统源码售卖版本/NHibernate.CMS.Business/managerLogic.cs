using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.CMS.DTO;

namespace NHibernate.CMS.Business
{
     
   public class managerLogic
    {
        protected readonly managerOpenrator dal = new managerOpenrator();
        public object Add(Model.dt_manager entity)
        {
            return this.dal.AddEntities(entity);
        }
    }
}
