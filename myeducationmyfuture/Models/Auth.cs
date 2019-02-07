using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myeducationmyfuture.Models
{
    public class Auth : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (HttpContext.Current.Session["LogedIn"] == null)
            {
                filterContext.Result = new RedirectResult("~/AdminPanel/LogPage");
            }
            base.OnActionExecuted(filterContext);
        }
    }
}