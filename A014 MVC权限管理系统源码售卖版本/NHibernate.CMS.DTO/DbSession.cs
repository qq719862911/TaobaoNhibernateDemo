 
using System.Data.Objects;

namespace NHibernate.CMS.DTO
{
    public partial class DbSession:IDTO.IDbSession  
    {   
	



	public IDTO.Isys_acl_groupRepository sys_acl_groupRepository { get { return new sys_acl_groupRepository(); } }

	



	public IDTO.Isys_acl_userRepository sys_acl_userRepository { get { return new sys_acl_userRepository(); } }

	



	public IDTO.Isys_actionRepository sys_actionRepository { get { return new sys_actionRepository(); } }

	



	public IDTO.Isys_configRepository sys_configRepository { get { return new sys_configRepository(); } }

	



	public IDTO.Isys_DepartmentRepository sys_DepartmentRepository { get { return new sys_DepartmentRepository(); } }

	



	public IDTO.Isys_groupRepository sys_groupRepository { get { return new sys_groupRepository(); } }

	



	public IDTO.Isys_group_userRepository sys_group_userRepository { get { return new sys_group_userRepository(); } }

	



	public IDTO.Isys_moduleRepository sys_moduleRepository { get { return new sys_moduleRepository(); } }

	



	public IDTO.Isys_tokenRepository sys_tokenRepository { get { return new sys_tokenRepository(); } }

	



	public IDTO.Isys_userRepository sys_userRepository { get { return new sys_userRepository(); } }

	}
}