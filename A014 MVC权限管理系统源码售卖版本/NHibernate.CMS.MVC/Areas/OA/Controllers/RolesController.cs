using NHibernate.CMS.Business;
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
    public class RolesController : AdminControllerBase
    {
        private readonly Isys_groupService bll = new sys_groupService();
        private readonly Isys_actionService actBll = new sys_actionService();
        private readonly Isys_moduleService modulebll = new sys_moduleService();
        private readonly Isys_acl_groupService aclgroupBll = new sys_acl_groupService();

        //
        // GET: /OA/Roles/Show
        public ActionResult Show()
        {
            return View("List");
        }
        public string List()
        {
            PageResult result = new PageResult();
            int total = 0;
            result.pageSize = this.GetPageSize;
            result.pageIndex = this.GetPageIndex;
             
            string query = this.GetQueryStr;//查询条件

            //IList<sys_group> list = bll.LoadPagerEntities<sys_group>(result, out total, k => string.IsNullOrWhiteSpace(query) ? k.groupID > 0 : (k.groupName.Contains(query))
            //    , this.GetOrderBy, m => (string.IsNullOrWhiteSpace(this.GetSort) == true ? m.groupID.ToString() :this.GetSort));

            //return JsonHelper.FlexiGridJson(list, result.pageIndex, total, "2");
            string whereLambda = "";
            if (!string.IsNullOrWhiteSpace(query))
            { whereLambda = " (groupName like '%" + query + "%')"; }

              DataTable dt = bll.LoadPagerEntities(result, out total, whereLambda, this.GetSotrOrOrderBy);
            return JsonHelper.FlexiGridToJson<sys_group>(dt, result.pageIndex, total);
        }

        public ActionResult Add()
        {
            //var list = actBll.LoadEntities(m => m.actionID > 0);
            //ViewBag.actionRoles = new SelectList(list, "actionID", "actionName");
             ViewBag.modulelist= modulebll.LoadEntities(m=>m.isDisplay==true).ToList();
            
            return View("Edit");
        }
        public ActionResult Edit(int? id)
        {
            var model = bll.GetSingleModel(m => m.groupID == id);
            //var list = actBll.LoadEntities(m => m.actionID > 0);
            //ViewBag.actionRoles = new SelectList(list, "actionID", "actionName");
            ViewBag.modulelist = modulebll.LoadEntities(m => m.isDisplay == true).ToList();
            
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var model = bll.GetSingleModel(m => m.groupID == id);
            TryUpdateModel<sys_group>(model);

             
            if (string.IsNullOrEmpty(model.groupName))
            {
                ModelState.AddModelError("error", "名称错误");
                return View("Edit");
            }

            string checkAll = collection.Get("checkAll");
            try
            {
                using (TransactionScope scope = new TransactionScope())
                { 
                    var seleList = aclgroupBll.LoadEntities(m => m.groupID==model.groupID);//上次选中的
                    sys_acl_group models;
                    foreach (var item in checkAll.Split(','))
                    {
                        models = seleList.Where(w => w.actionID == int.Parse(item)).FirstOrDefault();
                        if (models == null)
                        {
                            models = new sys_acl_group();
                            models.access = true;
                            models.groupID = int.Parse(id.ToString());
                            models.actionID = int.Parse(item);
                            aclgroupBll.AddEntities(models);
                        }
                        seleList.Remove(models);
                        
                    }
                    if (seleList.Count > 0)
                    {
                        foreach (var item in seleList)
                        {
                            aclgroupBll.DeleteEntities(item);
                        }
                    } 
                    bll.UpdateEntities(model);
                    scope.Complete();
                }
               
            }
            catch
            {
                
                return this.JscriptMsg("信息已修改失败", false, "Success");
            }

            return this.JscriptMsg("信息已修改成功", false, "Success");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FormCollection collection)
        {
            var model = new sys_group();
            this.TryUpdateModel<sys_group>(model); 
            if (string.IsNullOrEmpty(model.groupName))
            {
                ModelState.AddModelError("error", "名称错误");
                return View("Edit");
            }

            string checkAll = collection.Get("checkAll");

            try
            {
                
                using (TransactionScope scope = new TransactionScope())
                {
                    var id = bll.AddEntities(model); 
                    foreach (var item in checkAll.Split(','))
                    {
                        sys_acl_group models = new sys_acl_group();
                        models.access = true;
                        models.groupID = int.Parse(id.ToString());
                        models.actionID = int.Parse(item);
                        aclgroupBll.AddEntities(models);
                    } 
                    scope.Complete();
                }
             
            }
            catch
            {
                 
                return View("Edit", model);
            }

            return this.JscriptMsg("信息已添加成功", false, "Success");
        }
        [HttpPost]
        public JsonResult Delete(string id)
        {

            try
            {

                
                using (TransactionScope scope = new TransactionScope())
                {
                    bll.DeleteEntities("from sys_group Where groupID in(" + id + ")");
                    foreach (var item in id.Split(','))
                    {
                        var smodel = bll.GetSingleModel(m => m.groupID == int.Parse(item));
                        if (smodel != null)
                        {
                            aclgroupBll.DeleteEntities(string.Format("from sys_acl_group Where groupID={0}", smodel.groupID));
                        }
                    }
                    scope.Complete();
                }

            }
            catch
            {

                return Json("信息已删除失败");
            }

            return Json("信息已删除");
        }


    }
}
