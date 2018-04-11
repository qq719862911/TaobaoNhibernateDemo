using System.Web.Mvc;

namespace NHibernate.CMS.MVC.Areas.Account
{
    public class AccountAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Account_default",
                "Account/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                 , new[] { "NHibernate.CMS.MVC.Areas.Account.Controllers" }//控制器的命名
            );
        }
    }
}
