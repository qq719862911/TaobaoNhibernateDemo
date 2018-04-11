using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.CMS.Framework;
using NHibernate.CMS.Framework.Utility;
using System.Web.Security;
using NHibernate.CMS.Model.Entity;
using NHibernate.CMS.MVC.Common;


namespace NHibernate.CMS.MVC.Controllers
{
    public class HomeController : Controller
    {



        //
        // GET: /Home/

        public ActionResult Index()
        {
            return    View();
        }
        public ActionResult Login()
        {
            //////加密
            //DesOrRsaEncrypt des = new DesOrRsaEncrypt();
            //string path = AppSettingsHelper.GetString("RsaPublicKey");
            //System.IO.StreamReader reader = new System.IO.StreamReader(Server.MapPath(path));
            //string PublicKey = reader.ReadToEnd();
            //Param parsm = des.Encrypt("admin", PublicKey);
            ////解密
            //string path2 = AppSettingsHelper.GetString("RsaPrivateKey");
            //System.IO.StreamReader reader2 = new System.IO.StreamReader(Server.MapPath(path2));
            //string PublicKey2 = reader2.ReadToEnd();
            //string mw = des.Decrypt(parsm.Data, parsm.DESKey, PublicKey2);
            

            return View();
        }
        [HttpPost]
        //[Authorize(Roles="")]
        public ActionResult Login(string username, string password, string verifycode)
        {

            if (!isCheckVerifyCode(verifycode))
            {
                ModelState.AddModelError("error", "验证码错误");
                return View();
            }
            
            if (string.IsNullOrWhiteSpace(username))
            {
                ModelState.AddModelError("error", "请输入用户名");
                return View();
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("error", "请输入密码");
                return View();
            }
            password = Encrypt.MD5(Encrypt.Encode(password));
            NHibernate.CMS.IBusiness.Isys_userService bll = new NHibernate.CMS.Business.sys_userService();
            var loginInfo = bll.GetSingleModel(o => o.userAccount == username && o.userPasswd == password && o.status == true);
            
            if (loginInfo != null)
            {
                Model.adminlogin model = new Model.adminlogin();
                model.UserID = loginInfo.userID;
                model.UserName = loginInfo.userAccount;
                model.Token = Guid.NewGuid();
                model.LoginDate = DateTime.Now;
                Session[CMSKeys.SESSION_ADMIN_INFO] = model;
                //Caching.Set("adminLogin-key", model.Token,10);
                //NHibernate.CMS.RedisFramework.RedisHelper.Single_Set_Itme<Model.adminlogin>(RedisKeys.REDIS_KEY_ADMINLOGIN + model.Token, model);
                return RedirectToAction("Index", "Auth", new { Area = "Account" });
            }
            else
            {
                ModelState.AddModelError("error", "用户名或密码错误");
                return View();
            }
        }

        private bool isCheckVerifyCode( string code)
        {
            //Verification model = new Verification();
            //model = NHibernate.CMS.RedisFramework.RedisHelper.Single_Get_Itme<Verification>(RedisKeys.REDIS_VERIFICATION + code);
            //if (model == null) return false;
            //return model.VerCode == code ? true : false;
            if (Session[CMSKeys.SESSION_VERIFYCODE_INFO] != null)
            {
                string vcode = Session[CMSKeys.SESSION_VERIFYCODE_INFO].ToString();
                return vcode == code ? true : false;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Error(string id)
        {
            string msg="";
            switch (id)
            { 
                case "error":
                    msg = "对不起，系统错误提示！";
                    break;
                case "premission":
                    msg = "对不起，您无权访问该页面，请联系管理员！";
                     
                    break;
            }
            ViewData["msg"] = msg;
            return View();
        }
    }
}
