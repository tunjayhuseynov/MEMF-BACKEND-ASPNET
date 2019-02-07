using System.Web.Mvc;

namespace myeducationmyfuture.Areas.AdminPanel
{
    public class AdminPanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminPanel_default",
                "AdminPanel/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "myeducationmyfuture.Areas.AdminPanel.Controllers" }
            );
        }
    }
}