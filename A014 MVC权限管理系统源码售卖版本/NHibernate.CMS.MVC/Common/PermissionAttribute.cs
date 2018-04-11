using NHibernate.CMS.Business;
using NHibernate.CMS.Framework;
using NHibernate.CMS.Framework.Utility;
using NHibernate.CMS.IBusiness;
using NHibernate.CMS.Model;
using NHibernate.CMS.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHibernate.CMS.MVC.Common
{
    /// <summary>
    /// 权限拦截
    /// </summary>
    public class PermissionAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 登陆页面
        /// </summary>
        public class PageUrl
        {
            public string Controller { get; set; }
            public string Action { get; set; }
            public string Url
            {
                get { return string.Format("{0}/{1}", Controller, Action); }
            }
        }
        private PageUrl url;

        //重写Authorization
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            //获取当前页面地址
            url = new PageUrl();
            url.Controller = filterContext.RouteData.Values["controller"] as string;
            url.Action = filterContext.RouteData.Values["action"] as string;

            //判断用户是否登录
           // string  Token=Caching.Get("adminLogin-key").ToString();//缓存
            Model.adminlogin loginModel = HttpContext.Current.Session[CMSKeys.SESSION_ADMIN_INFO] as Model.adminlogin;
                //NHibernate.CMS.RedisFramework.RedisHelper.Single_Get_Itme<Model.adminlogin>(RedisKeys.REDIS_KEY_ADMINLOGIN + Token);
           
            if (loginModel==null)
            {
                // 未登录，跳转至登录页面
                filterContext.Result = new RedirectResult("/Home/Login");
                return; 
            }
            else
            {
                 
                if (!AuthorizeCore(filterContext.HttpContext))
                {
                    filterContext.Result = new RedirectResult("/Home/Error/premission");
                    //filterContext.HttpContext.Response.Write(""); 
                }
                //redirect to login page
            }
        }

        /// <summary>
        /// 重写AuthorizeAttribute的AuthorizeCore方法
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = false;
            //string Token = Caching.Get("adminLogin-key").ToString();//缓存
            Model.adminlogin loginModel = HttpContext.Current.Session[CMSKeys.SESSION_ADMIN_INFO] as Model.adminlogin;
                //NHibernate.CMS.RedisFramework.RedisHelper.Single_Get_Itme<Model.adminlogin>(RedisKeys.REDIS_KEY_ADMINLOGIN + Token);
           
            //获取登陆标示
            if (loginModel != null) 
            {
                
                //进行权限校验
                //if(。。。。。)
                string action = url.Action;
                string controller = url.Controller;
                //如果是admin 拥有所有权限
                if (loginModel.UserName == "admin") return true;

                Isys_actionService action_bll = new sys_actionService();//模块功能信息表
                Isys_acl_userService acl_user_bll = new sys_acl_userService();//用户权限控制信息表
                Isys_acl_groupService acl_group_bll = new sys_acl_groupService();//分组权限控制信息表
                Isys_group_userService group_user_bll = new sys_group_userService();//用户与用户组信息表
                //1.根据当前 action 、controller查询sys_action 找出actionID
                var actionModel = action_bll.GetSingleModel(o => o.actionKey == action && o.moduleKey == controller);
                if (actionModel == null) return false;//表示没找到 action
                //2.根据当前 ueserid 、actionID查询sys_acl_user 存在数据就返回 access
               
                var acl_userModel = acl_user_bll.GetSingleModel(w => w.actionID == actionModel.actionID && w.userID == loginModel.UserID);
                if (acl_userModel != null) return true;//表示有该权限
                //3.根据当前 groupid 、actionID查询sys_acl_group 存在数据就返回 access 没有就表示没权限
                var group_userModel = group_user_bll.GetSingleModel(k => k.userID == loginModel.UserID);

                var acl_groupModel = acl_group_bll.GetSingleModel(o => o.groupID == group_userModel.groupID && o.actionID == actionModel.actionID);
                if (acl_groupModel != null)
                    result = acl_groupModel.access;
  
            }
            return result;
        }

    }
}