using NHibernate.CMS.Business;
using NHibernate.CMS.Framework.Utility;
using NHibernate.CMS.IBusiness;
using NHibernate.CMS.Model;
using NHibernate.CMS.MVC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHibernate.CMS.MVC.Areas.Systems.Controllers
{
    public class SettingsController : AdminControllerBase
    {
        private readonly Isys_configService bll = new sys_configService(); 

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
 
            string whereLambda = "";
            if (!string.IsNullOrWhiteSpace(query))
            { whereLambda = " (type like '%" + query + "%' or name like '%" + query + "%')"; }

            System.Data.DataTable dt = bll.LoadPagerEntities(result, out total, whereLambda, this.GetSotrOrOrderBy);
            return JsonHelper.FlexiGridToJson<sys_config>(dt, result.pageIndex, total);
        }

        public ActionResult Add()
        {
            
            return View("Edit");
        }
        public ActionResult Edit(int? id)
        {
            var model = bll.GetSingleModel(m => m.id == id); 
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var model = bll.GetSingleModel(m => m.id == id);
            TryUpdateModel<sys_config>(model);
             
            try
            {
                bll.UpdateEntities(model);

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
            var model = new sys_config();
            this.TryUpdateModel<sys_config>(model);
               
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
                bll.DeleteEntities("from sys_config Where id in(" + id + ")");
            }
            catch
            {

                return Json("信息已删除失败");
            }

            return Json("信息已删除");
        }



    }
}
