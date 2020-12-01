using System.Web.Mvc;

namespace WebBanHang.Areas.Clience
{
    public class ClienceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Clience";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            
            context.MapRoute(
                "HomeClience",
                "Clience/Home",
                new { action = "Home", Controller = "Clience", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Register",
                "Clience/Register",
                new { action = "Register",Controller="Clience", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Logout",
                "Clience/logout",
                new { action = "logout", Controller = "Clience", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Clience_default",
                "Clience/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}