using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.CMS.Framework;
using NHibernate.CMS.Model.Entity;
 

namespace NHibernate.CMS.MVC.Controllers
{
    public class CommController : Controller
    {
        //
        // GET: /Comm/
        //验证码
        public  ActionResult VerifyImage()
        {
            var s1 = new ValidateCode_Style4();
            string code = "6666";
            byte[] bytes = s1.CreateImage(out code);

            //this.CookieContext.VerifyCode = code;
            //Verification model = new Verification();
            //model.VerID = Guid.NewGuid();
            //model.VerCode = code;
            //NHibernate.CMS.RedisFramework.RedisHelper.Single_Set_Itme<Verification>(RedisKeys.REDIS_VERIFICATION + code, model);
            Session[CMSKeys.SESSION_VERIFYCODE_INFO] = code;
            return File(bytes, @"image/png");
            
        }
    }

    

}
