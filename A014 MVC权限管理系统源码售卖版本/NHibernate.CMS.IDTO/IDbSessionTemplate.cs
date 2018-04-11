 

using System.Data.Objects;
namespace NHibernate.CMS.IDTO
{
    public partial interface IDbSession
    {
   
	  

		IDTO.Isys_acl_groupRepository sys_acl_groupRepository { get; }
	  

		IDTO.Isys_acl_userRepository sys_acl_userRepository { get; }
	  

		IDTO.Isys_actionRepository sys_actionRepository { get; }
	  

		IDTO.Isys_configRepository sys_configRepository { get; }
	  

		IDTO.Isys_DepartmentRepository sys_DepartmentRepository { get; }
	  

		IDTO.Isys_groupRepository sys_groupRepository { get; }
	  

		IDTO.Isys_group_userRepository sys_group_userRepository { get; }
	  

		IDTO.Isys_moduleRepository sys_moduleRepository { get; }
	  

		IDTO.Isys_tokenRepository sys_tokenRepository { get; }
	  

		IDTO.Isys_userRepository sys_userRepository { get; }
	}
}