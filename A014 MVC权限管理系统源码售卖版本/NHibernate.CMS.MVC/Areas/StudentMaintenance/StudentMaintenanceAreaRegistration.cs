using System.Web.Mvc;

namespace NHibernate.CMS.MVC.Areas.StudentMaintenance
{
    public class StudentMaintenanceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "StudentMaintenance";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "StudentMaintenance_default",
                "StudentMaintenance/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
