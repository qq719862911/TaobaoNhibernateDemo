using NHibernate.CMS.Business;
using NHibernate.CMS.Framework.Utility;
using NHibernate.CMS.IBusiness;
using NHibernate.CMS.Model;
using NHibernate.CMS.MVC.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHibernate.CMS.MVC.Areas.OA.Controllers
{
    public class DepartmentController : AdminControllerBase
    {
        private readonly Isys_DepartmentService bll = new sys_DepartmentService();
        //
        // GET: /OA/Department/
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

            //    IList<sys_Department> list = bll.LoadPagerEntities<sys_Department>(result, out total, k => string.IsNullOrWhiteSpace(query) ? k.ID > 0 : (k.DeparName.Contains(query) )
            //    , this.GetOrderBy, m => (string.IsNullOrWhiteSpace(this.GetSort) == true ? m.sort.ToString() : this.GetSort));

            //return JsonHelper.FlexiGridJson(list, result.pageIndex, total);
            string whereLambda = "";
            if (!string.IsNullOrWhiteSpace(query))
            { whereLambda = " (DeparName like '%" + query + "%')"; }

             DataTable dt = bll.LoadPagerEntities(result, out total, whereLambda, this.GetSotrOrOrderBy);
            return JsonHelper.FlexiGridToJson<sys_Department>(dt, result.pageIndex, total);
        }

        public ActionResult Add()
        {
            var list = bll.LoadEntities(m => m.ID > 0);
            list.Insert(0, new sys_Department { ID = 0, DeparName = "选择上级" });
            this.ViewBag.ParentIds = new SelectList(list, "ID", "DeparName");

            return View("Edit");
        }
        public ActionResult Edit(int? id)
        {
            var model = bll.GetSingleModel(m => m.ID == id);
            var list = bll.LoadEntities(m => m.ID > 0);
            list.Insert(0, new sys_Department { ID = 0, DeparName = "选择上级" });
            this.ViewBag.ParentIds = new SelectList(list, "ID", "DeparName", model.parentID);

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var model = bll.GetSingleModel(m => m.ID == id);
            TryUpdateModel<sys_Department>(model);

            model.parentID=int.Parse(collection.Get("ParentIds"));
            if (string.IsNullOrEmpty(model.DeparName))
            {
                ModelState.AddModelError("error", "名称错误");
                return View("Edit");
            }


            try
            {
                

                bll.UpdateEntities(model);
            }
            catch
            {
                var list = bll.LoadEntities(m => m.ID > 0);
                list.Insert(0, new sys_Department { ID = 0, DeparName = "选择上级" });
                this.ViewBag.ParentIds = new SelectList(list, "ID", "DeparName", model.parentID);
                return this.JscriptMsg("信息已修改失败", false, "Success");
            }

            return this.JscriptMsg("信息已修改成功", false, "Success");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FormCollection collection)
        {
            var model = new sys_Department();
            this.TryUpdateModel<sys_Department>(model);
            model.parentID = int.Parse(collection.Get("ParentIds"));
            if (string.IsNullOrEmpty(model.DeparName))
            {
                ModelState.AddModelError("error", "名称错误");
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

            return this.JscriptMsg("信息已添加成功", false, "Success");
        }
        [HttpPost]
        public JsonResult Delete(string id)
        {

            try
            {
                 
                bll.DeleteEntities("from sys_Department Where id in("+id+")");

            }
            catch
            {

                return Json("信息已删除失败");
            }

            return Json("信息已删除");
        }

    }
}
