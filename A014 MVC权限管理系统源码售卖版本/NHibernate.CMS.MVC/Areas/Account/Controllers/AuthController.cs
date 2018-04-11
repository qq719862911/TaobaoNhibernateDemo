using NHibernate.CMS.Framework;
using NHibernate.CMS.Model.Entity;
using NHibernate.CMS.MVC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHibernate.CMS.MVC.Areas.Account.Controllers
{

    public class AuthController : Controller
    {
        //
        // GET: /Account/Auth/
        public ActionResult Index()
        {
            return View();
        }
        public void Logout()
        {

            Session.Remove(CMSKeys.SESSION_ADMIN_INFO);
             HttpContext.Response.Redirect("/Home/Login");
             return;// RedirectToAction("Login", "Home");

        }

         


    }
}
