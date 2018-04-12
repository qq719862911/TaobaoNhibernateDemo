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

namespace NHibernate.CMS.MVC.Areas.StudentMaintenance.Controllers
{
    public class StudentCourseController : AdminControllerBase
    {
         private readonly Isys_Oa_student_courseService bll = new sys_Oa_student_courseService();

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
            string query = this.GetQueryStr.Trim();//查询条件
            string whereLambda = "";
            if (!string.IsNullOrWhiteSpace(query))
            { whereLambda = " (Name like '%" + query + "%')"; }

            DataTable dt = bll.LoadPagerEntities(result, out total, whereLambda, dws);
            return JsonHelper.FlexiGridToJson<sys_Department>(dt, result.pageIndex, total);
        }
        [HttpGet]
        public ActionResult Add()
        {
            var list = bll.LoadEntities(m => m.ID > 0);
            list.Insert(0, new oa_student_course() { ID = 0,Name="C#基础",Price=200});
            return View("Edit");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FormCollection collection)
        {
            var model = new oa_student_course();
            this.TryUpdateModel<oa_student_course>(model);//使用来自控制器的当前值提供程序的值更新指定的模型实例。
                                                          // model.ID =int.Parse(collection.Get("ID"));
            if (string.IsNullOrEmpty(model.Name))
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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = bll.GetSingleModel(m => m.ID == id);
            var studentsList = bll.LoadEntities(m => m.ID > 0);
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var model = bll.GetSingleModel(m => m.ID == id);
            this.TryUpdateModel<oa_student_course>(model);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    bll.UpdateEntities(model);
                    scope.Complete();
                }
                return this.JscriptMsg("信息已添加成功", false, "Success");
            }
            catch
            {
                return this.JscriptMsg("信息已添加失败", false, "Error");
            }
        }


        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                bll.DeleteEntities("from oa_student_course Where id in(" + id+")");
            }
            catch (Exception)
            {
                return Json("信息已删除失败");
            }
            return Json("信息已删除");
        }

    }
}
