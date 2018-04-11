 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.CMS.Model;

namespace NHibernate.CMS.IBusiness
{
   
	
	public partial interface Isys_acl_groupService:IBaseService<sys_acl_group>
    {   }
	
	public partial interface Isys_acl_userService:IBaseService<sys_acl_user>
    {   }
	
	public partial interface Isys_actionService:IBaseService<sys_action>
    {   }
	
	public partial interface Isys_configService:IBaseService<sys_config>
    {   }
	
	public partial interface Isys_DepartmentService:IBaseService<sys_Department>
    {   }
	
	public partial interface Isys_groupService:IBaseService<sys_group>
    {   }
	
	public partial interface Isys_group_userService:IBaseService<sys_group_user>
    {   }
	
	public partial interface Isys_moduleService:IBaseService<sys_module>
    {   }
	
	public partial interface Isys_tokenService:IBaseService<sys_token>
    {   }
	
	public partial interface Isys_userService:IBaseService<sys_user>
    {   }
	
}