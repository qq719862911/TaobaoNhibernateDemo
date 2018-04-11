using NHibernate.CMS.Business;
using NHibernate.CMS.Framework.Utility;
using NHibernate.CMS.IBusiness;
using NHibernate.CMS.Model;
using NHibernate.CMS.MVC.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace NHibernate.CMS.MVC.Areas.Account.Controllers
{
    public class ModelController : AdminControllerBase
    {
        private readonly Isys_moduleService bll = new sys_moduleService();
        private readonly Isys_actionService atcbll = new sys_actionService();
        //
        // GET: /Account/Model/List
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
            string dws = this.GetSort;
            string query = this.GetQueryStr;//查询条件

            //IList<sys_module> list = bll.LoadPagerEntities<sys_module>(result, out total, k => string.IsNullOrWhiteSpace(query) ? k.moduleID > 0 :  ( k.moduleKey.Contains(query)||k.moduleName.Contains(query))
            //    , this.GetOrderBy, m => (string.IsNullOrWhiteSpace(this.GetSort) == true ? m.moduleKey :this.GetSort)).ToList();
      
            string whereLambda = "";
            if (!string.IsNullOrWhiteSpace(query))
            { whereLambda = " (moduleKey like '%" + query + "%' or moduleName like '%" + query + "%')"; }

             DataTable dt = bll.LoadPagerEntities(result, out total, whereLambda, this.GetSotrOrOrderBy);
            return JsonHelper.FlexiGridToJson<sys_module>(dt, result.pageIndex, total);
        }
       
        public ActionResult Add()
        {
            var list = bll.LoadEntities(m=>m.moduleID>0);
            list.Insert(0,new sys_module { moduleID=0, moduleName="选择目录" });
            this.ViewBag.ParentIds = new SelectList(list, "moduleID", "moduleName");//上级目录
            List<actionPower> actlist = new List<actionPower>();
            foreach (KeyValuePair<string, string> kvp in Utils.ActionType())
            {
                actlist.Add(new actionPower { actionName = kvp.Value, actionkey = kvp.Key });
                 
            }
            this.ViewBag.actionList = new SelectList(actlist, "actionkey", "actionName");//权限列表
            return View("Edit");
        }
        public ActionResult Edit(int? id)
        {
            var model = bll.GetSingleModel(m => m.moduleID == id);
            var list = bll.LoadEntities(m => m.moduleID > 0);
            list.Insert(0, new sys_module { moduleID = 0, moduleName = "选择目录" });
            this.ViewBag.ParentIds = new SelectList(list, "moduleID", "moduleName", model.parentID);//上级目录

            List<actionPower> actlist = new List<actionPower>();
            foreach (KeyValuePair<string, string> kvp in Utils.ActionType())
            {
                actlist.Add(new actionPower { actionName = kvp.Value, actionkey = kvp.Key });

            }
            var seleList = atcbll.LoadEntities(m => m.moduleKey == model.moduleKey);
            string ids = "";
            foreach (var item in seleList)
            {
                ids += item.actionKey + ",";
            }
            this.ViewBag.actionList = new SelectList(actlist, "actionkey", "actionName", ids.TrimEnd(','));//权限列表
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var model = bll.GetSingleModel(m => m.moduleID == id);
            TryUpdateModel<sys_module>(model);
            model.parentID = int.Parse(collection.Get("ParentIds"));//上级目录
            if (string.IsNullOrEmpty(model.moduleKey))
            {
                ModelState.AddModelError("error", "action错误");
                return View("Edit");
            }
            if (string.IsNullOrEmpty(model.moduleName))
            {
                ModelState.AddModelError("error", "名称错误");
                return View("Edit");
            }
            //获取权限
            string actionList = collection.Get("actionList");
            

            try
            {
                if (model.parentID == 0)
                {
                    model.class_layer = 1;

                }
                else
                {
                    var parModel = bll.GetSingleModel(m => m.moduleID == model.parentID);
                    model.class_layer = parModel.class_layer + 1;
                }

                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
              
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                {
                    if (!string.IsNullOrWhiteSpace(actionList))
                    {
                          var seleList = atcbll.LoadEntities(m => m.moduleKey == model.moduleKey);//上次选中的
                          sys_action models2;
                            
                            foreach (string item in actionList.Split(','))//选中了的
                            {
                                models2 = seleList.Where(w => w.actionKey == item).FirstOrDefault();
                                if (models2 == null)
                                {
                                    models2 = new sys_action();
                                    models2.actionKey = item;
                                    models2.moduleKey = model.moduleKey;
                                    KeyValuePair<string, string> kvp = Utils.ActionType().Where(m => m.Key == item).FirstOrDefault();
                                    models2.actionName = kvp.Value;
                                    atcbll.AddEntities(models2);
                                }
                                seleList.Remove(models2);
                            }
                            if (seleList.Count > 0)
                            {
                                foreach (var item in seleList)
                                { 
                                    atcbll.DeleteEntities(item);
                                }
                            } 
                    }
                    bll.UpdateEntities(model);
                    scope.Complete();
                }

            }
            catch
            {
                var list = bll.LoadEntities(m => m.moduleID > 0);
                list.Insert(0, new sys_module { moduleID = 0, moduleName = "选择目录" });
                this.ViewBag.ParentIds = new SelectList(list, "moduleID", "moduleName");//上级目录
                List<actionPower> actlist = new List<actionPower>();
                foreach (KeyValuePair<string, string> kvp in Utils.ActionType())
                {
                    actlist.Add(new actionPower { actionName = kvp.Value, actionkey = kvp.Key });

                }
                this.ViewBag.actionList = new SelectList(actlist, "actionkey", "actionName");//权限列表
                return this.JscriptMsg("信息已修改失败", false, "Success");
            }

            return this.JscriptMsg("信息已修改成功", false, "Success");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FormCollection collection)
        {
            var model = new sys_module();
            this.TryUpdateModel<sys_module>(model);
            model.parentID = int.Parse(collection.Get("ParentIds"));
            if (string.IsNullOrEmpty(model.moduleKey))
            {
                ModelState.AddModelError("error", "action错误");
                return View("Edit");
            }
            if (string.IsNullOrEmpty(model.moduleName))
            {
                ModelState.AddModelError("error", "名称错误");
                return View("Edit");
            }
            
            if (model.parentID == 0)
            {
                model.class_layer = 1;

            }
            else
            {
                var parModel = bll.GetSingleModel(m => m.moduleID == model.parentID);
                model.class_layer = parModel.class_layer + 1;
            }
             

            string actionList = collection.Get("actionList");
            
            try
            {
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                {
                    if (!string.IsNullOrWhiteSpace(actionList))
                    {

                        foreach (string item in actionList.Split(','))
                        {
                            sys_action models2 = atcbll.GetSingleModel(w => w.actionKey == item && w.moduleKey == model.moduleKey);
                            if (models2 == null)
                            {
                                models2 = new sys_action();
                            }
                            models2.actionKey = item;
                            models2.moduleKey = model.moduleKey;

                            KeyValuePair<string, string> kvp = Utils.ActionType().Where(m => m.Key == item).FirstOrDefault();
                            models2.actionName = kvp.Value;
                            atcbll.AddEntities(models2);
                        }
                    }
                  
                    bll.AddEntities(model);
                    scope.Complete();
                }

            }
            catch
            {
                var list = bll.LoadEntities(m => m.moduleID > 0);
                list.Insert(0, new sys_module { moduleID = 0, moduleName = "选择目录" });
                this.ViewBag.ParentIds = new SelectList(list, "moduleID", "moduleName");//上级目录
                List<actionPower> actlist = new List<actionPower>();
                foreach (KeyValuePair<string, string> kvp in Utils.ActionType())
                {
                    actlist.Add(new actionPower { actionName = kvp.Value, actionkey = kvp.Key });

                }
                this.ViewBag.actionList = new SelectList(actlist, "actionkey", "actionName");//权限列表
                return View("Edit", model);
            }

            return this.JscriptMsg("信息已添加成功",false, "Success");
        }
        [HttpPost]
        public JsonResult Delete(string id)
        {
            
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    bll.DeleteEntities("from sys_module Where moduleID in(" + id + ")");
                    foreach (var item in id.Split(','))
                    {
                        var smodel=bll.GetSingleModel(m=>m.moduleID==int.Parse(item));
                        if (smodel != null)
                        {
                            atcbll.DeleteEntities(string.Format("from sys_action Where moduleKey='{0}'", smodel.moduleKey));
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
