using NHibernate.CMS.MVC.Common;
using NHibernate.CMS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.CMS.IBusiness;
using NHibernate.CMS.Model;
using NHibernate.CMS.Model.Entity;
using System.Data;
using System.Linq.Expressions;
using System.Collections;

namespace NHibernate.CMS.MVC.Areas.Account.Controllers
{
    public class ActionController : AdminControllerBase
    {
        private readonly Isys_actionService bll = new sys_actionService();
        //
        // GET: /Account/Action/List
        public ActionResult Show()
        {
           
            return View("List");
        }
        [HttpPost]
        public JsonResult List()
        {
            PageResult result=new PageResult();
            int total = 0;
            result.pageSize = this.GetPageSize;
             result.pageIndex= this.GetPageIndex;
            string dws = this.GetSort;
            List<sys_action> list = bll.LoadPagerEntities<sys_action>(result, out total,k=>k.actionID>0,this.GetOrderBy,m=>m.actionKey).ToList();
           // string josn = NHibernate.CMS.Framework.Utility.JsonHelper.DataTable2Table(list, result.pageIndex, total);
             Hashtable ht = new Hashtable();
            ht["page"]=result.pageIndex;
            ht["total"] = total;
            ht["rows"] = list;
            return Json(ht);
        }
        public ActionResult Add()
        {
           
            return View("Edit");
        }
        public ActionResult Edit(int ?id)
        {
            var model = bll.GetSingleModel(m => m.actionID == id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var  model = bll.GetSingleModel(m=>m.actionID== id);
            return View("Edit",model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FormCollection collection)
        {
            var model = new sys_action();
            this.TryUpdateModel<sys_action>(model);
            if (string.IsNullOrEmpty(model.actionKey))
            {
                ModelState.AddModelError("error", "action错误");
                return View("Edit");
            }
            if (string.IsNullOrEmpty(model.moduleKey))
            {
                ModelState.AddModelError("error", "Controller错误");
                return View("Edit");
            }
            try
            {
                bll.AddEntities(model);
            }
            catch  
            {
               
                return View("Edit", model);
            }

            return View("List");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
              
            try
            {
                bll.DeleteEntities("sys_action", id);
            }
            catch  
            {

                return View("List");
            }

            return View();
        }

    }
}
