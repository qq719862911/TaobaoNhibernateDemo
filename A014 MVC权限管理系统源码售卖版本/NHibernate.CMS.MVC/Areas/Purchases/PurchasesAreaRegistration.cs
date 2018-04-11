using System.Web.Mvc;

namespace NHibernate.CMS.MVC.Areas.Purchases
{
    public class PurchasesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Purchases";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Purchases_default",
                "Purchases/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "NHibernate.CMS.MVC.Areas.Purchases.Controllers" }
            );
        }
    }
}
