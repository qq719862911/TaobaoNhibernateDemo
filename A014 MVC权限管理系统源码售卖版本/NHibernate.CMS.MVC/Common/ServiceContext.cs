using NHibernate.CMS.Business;
using NHibernate.CMS.Framework;
using NHibernate.CMS.Framework.Utility;
using NHibernate.CMS.IBusiness;
using NHibernate.CMS.Model;
using NHibernate.CMS.Model.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace NHibernate.CMS.MVC.Common
{
    public class ServiceContext
    {
        private static readonly Isys_actionService actBll = new sys_actionService();
        private static readonly Isys_acl_groupService aclgroupBll = new sys_acl_groupService();
        private static readonly Isys_acl_userService acluserBll = new sys_acl_userService();
        private static readonly Isys_moduleService moduleBll = new sys_moduleService();
        private static readonly Isys_group_userService groupUserBll = new sys_group_userService();
        private static readonly Isys_groupService groupBll = new sys_groupService();

        /// <summary>
        /// 加载所有启用菜单
        /// </summary>
        /// <returns></returns>
        public static List<sys_module> loadModule()
        {
            Model.adminlogin loginModel = LoginInfo;
            if (loginModel == null)
            {
              string url=  HttpContext.Current.Request.Url.Host + "/Home/Login";
              HttpContext.Current.Response.Redirect(url);
                return null;
            }
            List<sys_module> list = new List<sys_module>();
            ////如果缓存中找到了就直接返回
            object cahingList = Caching.Get("loadModule");
            if (cahingList != null) return cahingList as List<sys_module>;

            ////1.如果是超级管理员直接返回所有
            var groupUser = groupUserBll.GetSingleModel(m => m.userID == loginModel.UserID);
            var adminRoles = groupBll.GetSingleModel(m => m.groupID == groupUser.groupID);

            //2.找出显示的所有菜单
           // var asys_actionList = actBll.LoadEntities(m => m.actionKey == "Show");
            //3.找出当前登陆权限
            if (adminRoles.groupName == "超级管理组")
            {
                 var userModelacl = actBll.LoadEntities(w =>w.actionKey=="Show");//一级菜单
                 foreach (var item in userModelacl)
                 {
                     var sys_modules = moduleBll.GetSingleModel(m => m.moduleKey == item.moduleKey&&m.class_layer==1);
                     if (sys_modules != null)
                         list.Add(sys_modules);
                 } 
                Caching.Set("loadModule", list);
                return list;
            }
            //获取用户自己独立权限
            var sys_acl_userList = acluserBll.LoadEntities(m => m.userID == loginModel.UserID);
            
            foreach (var item in sys_acl_userList)
            {
                var userModelacl = actBll.GetSingleModel(w =>w.actionKey=="Show" && w.actionID == item.actionID);
                var sys_modules = moduleBll.GetSingleModel(m => m.moduleKey == userModelacl.moduleKey && m.class_layer == 1);
                if (sys_modules != null)
                    list.Add(sys_modules);
                 
            }
            ////获取用户角色组权限
            if (groupUser != null)
            {
                var aclGroupList = aclgroupBll.LoadEntities(w => w.groupID == groupUser.groupID);
                foreach (var item in aclGroupList)
                {
                    var GroupModelacl = actBll.GetSingleModel(w => w.actionID == item.actionID);
                    var sys_modules = moduleBll.GetSingleModel(m => m.moduleKey == GroupModelacl.moduleKey && m.class_layer == 1);
                    if (sys_modules != null)
                        list.Add(sys_modules);
                }
            }
            
            Caching.Set("loadModule", list);
            return list;
        }
         /// <summary>
        /// 加载启用菜单节点
        /// </summary>
        /// <returns></returns>
        public static List<sys_module> loadNodeModule(int parentID)
        {
            var list = moduleBll.LoadEntities(m => m.isDisplay == true && m.isMenu == true && m.parentID == parentID).OrderBy(w => w.sort);
            return list.ToList();
        }
        
         

        /// <summary>
        /// 权限
        /// </summary>
        /// <returns></returns>
        public static List<sys_action> CheckBoxList(string key)
        {
            List<sys_action> list = actBll.LoadEntities(m => m.moduleKey == key).ToList();
            return list;
        }
        /// <summary>
        /// 角色
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="actionID"></param>
        /// <returns></returns>
        public static bool CheckChecked(int groupID, int actionID)
        {
           var model= aclgroupBll.GetSingleModel(m => m.actionID == actionID && m.groupID == groupID);
           if (model == null) { return  false; }
           else
           { return model.access; }
        }
        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="actionID"></param>
        /// <returns></returns>
        public static bool CheckUserChecked(int userID, int actionID)
        {
            var model = acluserBll.GetSingleModel(m => m.actionID == actionID && m.userID == userID);
            if (model == null) { return false; }
            else
            { return model.access; }
        }

        /// <summary>
        /// 登陆用户信息
        /// </summary>
        public static adminlogin LoginInfo
        {
            get {
           return HttpContext.Current.Session[CMSKeys.SESSION_ADMIN_INFO] as Model.adminlogin;
                
            }
        }

    }
}