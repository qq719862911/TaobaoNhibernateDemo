using System.Web.Mvc;

namespace NHibernate.CMS.MVC.Areas.CMS
{
    public class CMSAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CMS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CMS_default",
                "CMS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                 , new[] { "NHibernate.CMS.MVC.Areas.OA" }//控制器的命名
            );
        }
    }
}
