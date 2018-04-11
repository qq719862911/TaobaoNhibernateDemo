 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.CMS.Business;
using NHibernate.CMS.IDTO;
using NHibernate.CMS.Model;

namespace NHibernate.CMS.Business
{
	
	public partial class sys_acl_groupService:BaseService<sys_acl_group>,IBusiness.Isys_acl_groupService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository =new NHibernate.CMS.DTO.sys_acl_groupRepository();
        }  
    }
	
	public partial class sys_acl_userService:BaseService<sys_acl_user>,IBusiness.Isys_acl_userService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository =new NHibernate.CMS.DTO.sys_acl_userRepository();
        }  
    }
	
	public partial class sys_actionService:BaseService<sys_action>,IBusiness.Isys_actionService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository =new NHibernate.CMS.DTO.sys_actionRepository();
        }  
    }
	
	public partial class sys_configService:BaseService<sys_config>,IBusiness.Isys_configService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository =new NHibernate.CMS.DTO.sys_configRepository();
        }  
    }
	
	public partial class sys_DepartmentService:BaseService<sys_Department>,IBusiness.Isys_DepartmentService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository =new NHibernate.CMS.DTO.sys_DepartmentRepository();
        }  
    }
	
	public partial class sys_groupService:BaseService<sys_group>,IBusiness.Isys_groupService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository =new NHibernate.CMS.DTO.sys_groupRepository();
        }  
    }
	
	public partial class sys_group_userService:BaseService<sys_group_user>,IBusiness.Isys_group_userService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository =new NHibernate.CMS.DTO.sys_group_userRepository();
        }  
    }
	
	public partial class sys_moduleService:BaseService<sys_module>,IBusiness.Isys_moduleService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository =new NHibernate.CMS.DTO.sys_moduleRepository();
        }  
    }
	
	public partial class sys_tokenService:BaseService<sys_token>,IBusiness.Isys_tokenService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository =new NHibernate.CMS.DTO.sys_tokenRepository();
        }  
    }
	
	public partial class sys_userService:BaseService<sys_user>,IBusiness.Isys_userService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository =new NHibernate.CMS.DTO.sys_userRepository();
        }  
    }
	
}