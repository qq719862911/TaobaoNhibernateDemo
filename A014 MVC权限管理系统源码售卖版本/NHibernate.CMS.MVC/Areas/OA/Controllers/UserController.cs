using NHibernate.CMS.Business;
using NHibernate.CMS.Framework;
using NHibernate.CMS.Framework.Utility;
using NHibernate.CMS.IBusiness;
using NHibernate.CMS.Model;
using NHibernate.CMS.MVC.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace NHibernate.CMS.MVC.Areas.OA.Controllers
{
    public class UserController : AdminControllerBase
    {
        private readonly Isys_userService bll = new sys_userService();//用户
        private readonly Isys_DepartmentService depabll = new sys_DepartmentService();//部门
        private readonly Isys_groupService roleBll = new sys_groupService();//角色
        private readonly Isys_group_userService gropbll = new sys_group_userService();//角色与用户
        private readonly Isys_moduleService sysmodulBll = new sys_moduleService();//菜单
        private readonly Isys_acl_userService aclUserBll = new sys_acl_userService();//用户权限
        //
        // GET: /OA/User/

        public ActionResult Show()
        {
            return View("list");
        }

        public string List()
        {
            PageResult result = new PageResult();
            int total = 0;
            result.pageSize = this.GetPageSize;
            result.pageIndex = this.GetPageIndex;

            string query = this.GetQueryStr;//查询条件
            string whereLambda = "";
            if (!string.IsNullOrWhiteSpace(query))
            { whereLambda = " (userAccount like '%" + query + "%' or acctounName like '%" + query + "%')"; }
            string sotrstr = " userID desc";
            if (!string.IsNullOrWhiteSpace(this.GetSotrOrOrderBy)) sotrstr = this.GetSotrOrOrderBy;
             DataTable dt = bll.LoadPagerEntities(result, out total, whereLambda, sotrstr);

            return JsonHelper.FlexiGridToJson<sys_user>(dt, result.pageIndex, total);
            //IList<sys_user> list= bll.LoadPagerEntities<sys_user>(result, out total, k => string.IsNullOrWhiteSpace(query) ? k.userID > 0 : (k.userAccount.Contains(query) || k.acctounName.Contains(query))
            //, this.GetOrderBy, m => (string.IsNullOrWhiteSpace(this.GetSort) == true ? m.userID.ToString() : this.GetSort)).ToList();
            //return JsonHelper.FlexiGridJson(list, result.pageIndex, total);
        }

        public ActionResult Add()
        {
            var deparList=depabll.LoadEntities(m=>m.ID>0);
            deparList.Insert(0, new sys_Department { DeparName="选择部门"  });
            ViewBag.departIDs = new SelectList(deparList, "ID", "DeparName");  //部门

            var roleList = roleBll.LoadEntities(m=>m.groupID>0);
            roleList.Insert(0, new sys_group { groupName="选择角色" });
            ViewBag.roleIDs = new SelectList(roleList, "groupID", "groupName");//角色
 
            return View("Edit");
        }
        public ActionResult Edit(int id)
        {
            var model = bll.GetSingleModel(m => m.userID == id);

            var deparList = depabll.LoadEntities(m => m.ID > 0);
            deparList.Insert(0, new sys_Department { DeparName = "选择部门" });
            ViewBag.departIDs = new SelectList(deparList, "ID", "DeparName",model.departID);  //部门


            var rolegoupModel = gropbll.GetSingleModel(m => m.userID == model.userID);//获取用户与角色

            var roleList = roleBll.LoadEntities(m => m.groupID > 0);
            roleList.Insert(0, new sys_group { groupName = "选择角色" });
            ViewBag.roleIDs = new SelectList(roleList, "groupID", "groupName", rolegoupModel.groupID);//角色

            return View("Edit", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FormCollection collection)
        { 
            var model = new sys_user();
            this.TryUpdateModel<sys_user>(model);
            string pwd = Encrypt.MD5(Encrypt.Encode(model.userPasswd));
            model.userPasswd = pwd;
            string roleID = collection.Get("roleID");
            if (model.departID == 0 || roleID == "0")
            {
                var deparList = depabll.LoadEntities(m => m.ID > 0);
                deparList.Insert(0, new sys_Department { DeparName = "选择部门" });
                ViewBag.departIDs = new SelectList(deparList, "ID", "DeparName");  //部门

                var roleList = roleBll.LoadEntities(m => m.groupID > 0);
                roleList.Insert(0, new sys_group { groupName = "选择角色" });
                ViewBag.roleIDs = new SelectList(roleList, "groupID", "groupName");//角色
                ViewBag.departErro = "请选择部门";
                
                ViewBag.roleIDsErro = "请选择角色";
                return View("Edit");
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    object ids= bll.AddEntities(model);
                    sys_group_user roles = new sys_group_user();
                    roles.groupID = int.Parse(roleID);
                    roles.userID = int.Parse(ids.ToString());
                    gropbll.AddEntities(roles);
                    scope.Complete();
                }
                return this.JscriptMsg("信息已添加成功", false, "Success");
            }
            catch {
                return this.JscriptMsg("信息已添加失败", false, "Error");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
           var model = bll.GetSingleModel(m => m.userID == id);
           string pwd = model.userPasswd;
            this.TryUpdateModel<sys_user>(model);
            string ckpwd = collection.Get("ckpwd");
            if (!string.IsNullOrWhiteSpace(ckpwd) && !string.IsNullOrWhiteSpace(model.userPasswd))
            {
                 pwd= Encrypt.MD5(Encrypt.Encode(model.userPasswd));
               
            }
             model.userPasswd = pwd;
             //model.acctounName = collection.Get("acctounName");
            string roleID = collection.Get("roleID");
            

            if (model.departID == 0 || roleID == "0")
            {
                var deparList = depabll.LoadEntities(m => m.ID > 0);
                deparList.Insert(0, new sys_Department { DeparName = "选择部门" });
                ViewBag.departIDs = new SelectList(deparList, "ID", "DeparName");  //部门

                var roleList = roleBll.LoadEntities(m => m.groupID > 0);
                roleList.Insert(0, new sys_group { groupName = "选择角色" });
                ViewBag.roleIDs = new SelectList(roleList, "groupID", "groupName");//角色
                ViewBag.departErro = "请选择部门";

                ViewBag.roleIDsErro = "请选择角色";
                return View("Edit", model);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                     bll.UpdateEntities(model);
                    sys_group_user roles = gropbll.GetSingleModel(m => m.userID == model.userID);
                    roles.groupID = int.Parse(roleID);
                    gropbll.UpdateEntities(roles);
                    scope.Complete();
                }
                return this.JscriptMsg("信息已添加成功", false, "Success");
            }
            catch
            {
                return this.JscriptMsg("信息已添加失败", false, "Error");
            }
        }


        //授权
        public ActionResult Authorize(int id)
        {
            var model = bll.GetSingleModel(m => m.userID == id);

            ViewBag.modulelist = sysmodulBll.LoadEntities(m => m.isDisplay == true).ToList();
            return View("Authorize", model);
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Authorize(int id,FormCollection collection)
        {
            var model = bll.GetSingleModel(m => m.userID == id);
            string checkAll=collection.Get("checkAll");
            if (string.IsNullOrWhiteSpace(checkAll))
            {
                ViewBag.modulelist = sysmodulBll.LoadEntities(m => m.isDisplay == true).ToList();
                ViewBag.UserDsErro = "请选择授权信息";
                return View("Authorize", model);
            }


            var seleList = aclUserBll.LoadEntities(m => m.userID == id);//上次选中的
            sys_acl_user acluser;
            foreach (var item in checkAll.Split(','))
            {
                int actionID=int.Parse(item);
                acluser = seleList.Where(w => w.actionID == actionID).FirstOrDefault();//查询这个权限是否已经选中过
                if (acluser == null)
                {
                     acluser = new sys_acl_user();
                     acluser.userID = id;
                     acluser.access = true;
                     acluser.actionID = actionID;
                     aclUserBll.AddEntities(acluser);
                }
                seleList.Remove(acluser);

            }
            if (seleList.Count > 0)
            {
                foreach (var item in seleList)
                {
                    aclUserBll.DeleteEntities(item);//删除多余权限
                }
            } 
            return this.JscriptMsg("信息已添加成功", false, "Success");
        }
    }
}
