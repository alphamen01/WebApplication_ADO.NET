using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication_ADO.NET.Permisos
{
    public class NoAccesoCuandoSesionActivaAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["usuario"] != null)
            {
                filterContext.Result = new RedirectResult("~/");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}